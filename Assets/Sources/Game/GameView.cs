using TMPro;
using UnityEngine;

public enum PopUpType
{
    None,
    GameOverPopUp
}
public class GameView : MonoBehaviour
{
    public GameModel GameModel;
    
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI TimerText;

    [SerializeField] private GameOverPopUp GameOverPopUp;

    public void ResetUI()
    {
        ScoreText.text = "0";
        TimerText.text = GameModel.LeftTimeMilliseconds / 1000 + "";
    }
    
    public void UpdateScore()
    {
        if (GameModel.Score != GameModel.TotalScore)
        {
            GameModel.IncreaseScoreToTotalScore();
            ScoreText.text = GameModel.Score + "";
        }
    }

    public void UpdateTimer()
    {
        TimerText.text = GameModel.LeftTimeMilliseconds / 1000 + "";
    }

    public void ShowPopUp(PopUpType popUpType)
    {
        GameOverPopUp.gameObject.SetActive(popUpType == PopUpType.GameOverPopUp);

        switch (popUpType)
        {
            case PopUpType.GameOverPopUp:
                AudioController.Instance.PlaySound("General_Game_Over");
                GameOverPopUp.Init(GameModel.TotalScore);
                break;
        }
    }
}