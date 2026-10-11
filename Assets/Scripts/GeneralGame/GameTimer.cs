using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    EnemySpawner enemySpawner;
    
    float timer = 0;


    int last40Seconds = 0;
    int lastOneHalfMinute = 0;
    int lastStage = 0;


    private void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int current40Seconds = Mathf.FloorToInt(timer / 40f);
        int currentOneHalfMinutes = Mathf.FloorToInt(timer / 90f);


        HandleTimerText();
        IncreaseSpawnIndex();

        if (current40Seconds > last40Seconds)
        {
            IncreaseMaximumSpawns();
            last40Seconds = current40Seconds;
        }

        if (currentOneHalfMinutes > lastOneHalfMinute)
        {
            IncreaseSpawnRate();
            lastOneHalfMinute = currentOneHalfMinutes;
        }
    }

    void HandleTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int miliSeconds = Mathf.FloorToInt((timer % 1) * 100);

        timerText.text = $"{minutes:00}:{seconds:00}:{miliSeconds:00}";
    }

    void IncreaseMaximumSpawns()
    {
        if (enemySpawner.MaxAmountOfEnemies < 18) enemySpawner.MaxAmountOfEnemies += 2;
    }

    void IncreaseSpawnRate()
    {
        if (enemySpawner.SpawnRateTimer > 5) enemySpawner.SpawnRateTimer -= 3;
    }

    void IncreaseSpawnIndex()
    {
        int stage = Mathf.Min(Mathf.FloorToInt(timer / 80f), 2);


        if (stage > lastStage)
        {
            enemySpawner.maxSpawnIndex++;
            lastStage = stage;
        }
    }
}
