using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    [SerializeField] private GameView GameView;
    
    public SpawnManager SpawnManager;
    public InputController InputController;

    private bool IsGameOver = false;

    private void Start()
    {
        GameView.GameModel = new GameModel();
        
        SpawnManager.Init();
        
        SubscribeActions();
    }

    private void FixedUpdate()
    {
        GameView.UpdateScore();
    }

    private void Update()
    {
        if (!IsGameOver)
        {
            SpawnManager.GenerationCircles();
            
            if (GameView.GameModel.LeftTimeMilliseconds >= 200)
            {
                GameView.GameModel.LeftTimeMilliseconds -= (int)(Time.deltaTime * 1000);
                GameView.UpdateTimer();
            }
            else
            {
                IsGameOver = true;
                SpawnManager.ClearAll();
                GameView.ShowPopUp(PopUpType.GameOverPopUp);
            }
        }
    }

    private void SubscribeActions()
    {
        InputController.OnTapAction += Tap;
    }

    private void Tap(CircleComponent circleComponent)
    {
        AudioController.Instance.PlaySound("General_Tap");
        GameView.GameModel.IncreaseScore(circleComponent.Score);
        SpawnManager.DestroyCircle(circleComponent);
    }

    public void Restart()
    {
        IsGameOver = false;
        GameView.GameModel.Reset();
        GameView.ResetUI();
        GameView.ShowPopUp(PopUpType.None);
    }
}