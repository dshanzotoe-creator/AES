using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{

    public int totalEnemiesKilled = 0;

    int highestEnemiesKilled;

    int highestScore;


    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {
        highestEnemiesKilled = PlayerPrefs.GetInt("HighestEnemiesKilled", 0);
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
    }

    private void Update()
    {
        scoreText.text = $"Score: {CalculateScore()}";
    }

    int CalculateScore()
    {
        return totalEnemiesKilled * 1000; 
    }

    public void CheckAndSetEnemiesKilled()
    {
        int _score = CalculateScore();
        if (totalEnemiesKilled > highestEnemiesKilled) PlayerPrefs.SetInt("HighestEnemiesKilled", totalEnemiesKilled);
        if (_score > highestScore) PlayerPrefs.SetInt("HighestScore", _score);
    }
}
