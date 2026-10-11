using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    EnemySpawner enemySpawner;
    
    float timer = 0;


    int last30Seconds = 0;
    int last50Seconds = 0;
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

        timer += Time.deltaTime;

        if (enemySpawner.isBossSpawned) return;

        AddEnemyToList();
        RemoveEnemyFromList();
        
        int current30Seconds = Mathf.FloorToInt(timer / 30f);
        int current50Seconds = Mathf.FloorToInt(timer / 50f);
        int currentBossTime = Mathf.FloorToInt(timer / 480);

        HandleTimerText();
        IncreaseSpawnIndex();

        if (current30Seconds > last30Seconds)
        {
            IncreaseMaximumSpawns();
            last30Seconds = current30Seconds;
        }

        if (current50Seconds > last50Seconds)
        {
            IncreaseSpawnRate();
            last50Seconds = current50Seconds;
        }

        if (currentBossTime > lastBossTime)
        {
            SpawnBoss();
        }

        if (enemiesInScene.Count == 0 && timer > 75)
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
