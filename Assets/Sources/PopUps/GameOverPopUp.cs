using TMPro;
using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;

    public void Init(int score)
    {
        ScoreText.text = "Score: " + score;
    }
}