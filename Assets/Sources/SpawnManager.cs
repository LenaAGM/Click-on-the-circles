using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform CircleParent;

    [SerializeField] private CircleComponent CircleComponentFactory;

    [SerializeField] private Material[] CircleMaterials;

    private Vector3[] Positions;

    private int spawnCirclesIndex;

    private float circleSpawnPeriod, circleSpawnPeriodTemp;

    private float TopFrontierY;
    private float RightFrontierX;

    [SerializeField] private BaseCircleParameters BaseCircleParameters;

    public void Init()
    {
        Positions = new Vector3[15];

        spawnCirclesIndex = 1;
        circleSpawnPeriod = circleSpawnPeriodTemp = 0.5f;
        
        FillPositions();
        ShufflePositionsArray();
    }

    public void GenerationCircles()
    {
        if (circleSpawnPeriodTemp <= 0f && CircleParent.childCount < 15)
        {
            circleSpawnPeriodTemp = circleSpawnPeriod;
            CreateCircle();
        }
        else
        {
            circleSpawnPeriodTemp -= Time.deltaTime;
        }
    }

    private void CreateCircle()
    {
        var circle = CircleComponentFactory;

        var circleComponent = Instantiate(circle, CircleParent);

        var scale = Random.Range(70, 100) / 100f;
        var materialIndex = Random.Range(0, 4);

        circleComponent.transform.position = GetNextPosition();
        circleComponent.transform.localScale = new Vector3(scale, scale, 1.0f);

        circleComponent.Init(CircleMaterials[materialIndex], (int)((materialIndex + 1.0f) * BaseCircleParameters.ScoreColorCoef * scale * BaseCircleParameters.ScoreSizeCoef));
        
        spawnCirclesIndex = spawnCirclesIndex == Positions.Length - 1 ? 0 : ++spawnCirclesIndex;
    }

    private Vector3 GetNextPosition()
    {
        foreach (Transform transform in CircleParent)
        {
            if (transform.position == Positions[spawnCirclesIndex])
            {
                spawnCirclesIndex = spawnCirclesIndex == Positions.Length - 1 ? 0 : ++spawnCirclesIndex;
                return GetNextPosition();
            }
        }

        return Positions[spawnCirclesIndex];
    }

    private void FillPositions()
    {
        TopFrontierY = -Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelHeight, 0, 0)).y;
        RightFrontierX = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0)).x;
        
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                //x
                var widthCell = RightFrontierX / 1.5f;
                var x = -widthCell + j * widthCell;
                var spaceX = (widthCell - 1.2f) / 2.0f;
                x = Random.Range(x - spaceX, x + spaceX);

                //y
                var heightCell = (TopFrontierY * 2.0f - 0.6f) / 5.0f;
                var y = (TopFrontierY - heightCell / 2.0f - 0.6f) - i * heightCell;
                var spaceY = (heightCell - 1.2f) / 2.0f;
                y = Random.Range(y - spaceY, y + spaceY);

                Positions[i * 3 + j] = new Vector3(x, y, 0);
            }
        }
    }
    
    private void ShufflePositionsArray()
    {
        var random = new System.Random();
        Positions = Positions.OrderBy(x => random.Next()).ToArray();
    }

    public void DestroyCircle(CircleComponent circleComponent)
    {
        circleComponent.Destroy();
    }

    public void ClearAll()
    {
        spawnCirclesIndex = 0;
        foreach (Transform circle in CircleParent)
        {
            Destroy(circle.gameObject);
        }
        FillPositions();
        ShufflePositionsArray();
    }
}