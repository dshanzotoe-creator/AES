using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    EnemySpawner enemySpawner;
    
    float timer = 0;


    int last20Seconds = 0;
    int last30Seconds = 0;
    int lastStage = 0;
    int lastBossTime = 0;

    List<GameObject> enemiesInScene = new List<GameObject>();
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
        if (enemySpawner.isBossSpawned) return;
    
        timer += Time.deltaTime;

        AddEnemyToList();
        RemoveEnemyFromList();
        
        int current20Seconds = Mathf.FloorToInt(timer / 20f);
        int current30Seconds = Mathf.FloorToInt(timer / 30f);
        int currentBossTime = Mathf.FloorToInt(timer / 300);

        HandleTimerText();
        IncreaseSpawnIndex();

        if (current20Seconds > last20Seconds)
        {
            IncreaseMaximumSpawns();
            last20Seconds = current20Seconds;
        }

        if (current30Seconds > last30Seconds)
        {
            IncreaseSpawnRate();
            last30Seconds = current30Seconds;
        }

        if (currentBossTime > lastBossTime)
        {
            SpawnBoss();
        }

        if (enemiesInScene.Count == 0 && !enemySpawner.isBossSpawned)
        {
            StartCoroutine(enemySpawner.SpawnEnemy());
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
        if (enemySpawner.MaxAmountOfEnemies < 30) enemySpawner.MaxAmountOfEnemies += 3;
    }

    void IncreaseSpawnRate()
    {
        if (enemySpawner.SpawnRateTimer > 3) enemySpawner.SpawnRateTimer -= 3;
        
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

    void SpawnBoss()
    {
        enemySpawner.isBossSpawned = true;
    }

    void AddEnemyToList()
    {
        GameObject[] foundAllEnemies = GameObject.FindGameObjectsWithTag("Enemy");


        foreach (GameObject enemy in foundAllEnemies)
        {
            if (enemy != null && !enemiesInScene.Contains(enemy))
            {
                enemiesInScene.Add(enemy);
            }
        }
    }

    void RemoveEnemyFromList()
    {
        for (int i = enemiesInScene.Count - 1; i >= 0; i--)
        {
            if (enemiesInScene[i] == null)
            {
                enemiesInScene.RemoveAt(i);
            }
        }
    }
}
