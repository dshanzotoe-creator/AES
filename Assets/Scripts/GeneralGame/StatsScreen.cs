using TMPro;
using UnityEngine;

public class StatsScreen : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject statsMenu;
    [SerializeField] TextMeshProUGUI  highEnemyText;
    [SerializeField] TextMeshProUGUI highScoreText;


    private void Awake()
    {
        highEnemyText = GameObject.Find("MostEnemiesKilled").GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.Find("HighestScore").GetComponent<TextMeshProUGUI>();

        highEnemyText.text = $"Most Enemies Killed: {PlayerPrefs.GetInt("HighestEnemiesKilled").ToString()}";
        highScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighestScore").ToString()}";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statsMenu.SetActive(false);
    }
    

    public void Stats()
    {
        statsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Back()
    {
        statsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
