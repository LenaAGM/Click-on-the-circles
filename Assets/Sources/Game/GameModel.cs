using UnityEngine;

public class GameModel
{
    private int score;
    public int Score => score;
    
    private int totalScore;
    public int TotalScore => totalScore;

    private const int TOTAL_TIME_MILLISECONDS = 60 * 1000;
    public int LeftTimeMilliseconds;

    public GameModel()
    {
        Reset();
    }

    public void Reset()
    {
        score = 0;
        totalScore = 0;
        LeftTimeMilliseconds = TOTAL_TIME_MILLISECONDS;
    }

    public void IncreaseScore(int score)
    {
        totalScore += score;
    }

    public void IncreaseScoreToTotalScore()
    {
        score = Mathf.Min(TotalScore, ++score);
    }
}