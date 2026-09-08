
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] BoxCollider2D boxCollider; 

    float boxMinX, boxMinY, boxMaxX, boxMaxY, cameraMinX, cameraMinY, cameraMaxX, cameraMaxY;

    [SerializeField] GameObject[] enemies;

    [SerializeField] float _spawnPadding = 2f;

    float _currentSpawnRateTimer;

    float _spawnRateTimer = 20;

    int _maxAmountOfEnemies = 8; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetBoxColliderCoordinates();
        _currentSpawnRateTimer = _spawnRateTimer; 

       
    }

    // Update is called once per frame
    void Update()
    {

        if (_currentSpawnRateTimer > 0)
        {
            _currentSpawnRateTimer -= Time.deltaTime;
        }
        else if (_currentSpawnRateTimer <= 0)
        {
            StartCoroutine(SpawnEnemy());
            _currentSpawnRateTimer = _spawnRateTimer; 
        }
    }


    IEnumerator SpawnEnemy() 
    {
       
        Vector2 spawnPoint = new Vector2();
        int enemiesSpawned = 0;

        while (enemiesSpawned != _maxAmountOfEnemies)
        {
            int spawnPlace = Random.Range(1, 5);
            GetCameraLocation();

            switch (spawnPlace)
            {
                case 1:
                    spawnPoint = LeftSide();
                    break;

                case 2:
                    spawnPoint = RightSide();
                    break;

                case 3:
                    spawnPoint = TopSide();
                    break;

                case 4:
                    spawnPoint = BottomSide();
                    break;
       
            }

            if (IsInPlayerArea(spawnPoint))
            {
                Instantiate(enemies[0], spawnPoint, Quaternion.identity);
                enemiesSpawned++;
            }

            Debug.Log(enemiesSpawned);
            yield return null;
        }


    }

    void GetBoxColliderCoordinates()
    {
        boxCollider = GetComponentInChildren<BoxCollider2D>();

        if (boxCollider != null)
        {
            boxMinX = boxCollider.bounds.min.x;
            boxMinY = boxCollider.bounds.min.y;
            boxMaxX = boxCollider.bounds.max.x;
            boxMaxY = boxCollider.bounds.max.y;
        }
    }

    void GetCameraLocation()
    {
        playerCamera = Camera.main;
        cameraMinY = playerCamera.transform.position.y - playerCamera.orthographicSize;
        cameraMaxY = playerCamera.transform.position.y + playerCamera.orthographicSize;

        float halfSize = playerCamera.orthographicSize * playerCamera.aspect; 

        cameraMinX = playerCamera.transform.position.x - halfSize;
        cameraMaxX = playerCamera.transform.position.x + halfSize;

    }

    bool IsInPlayerArea(Vector2 point)
    {
        return point.x >= boxMinX && point.x <= boxMaxX && point.y >= boxMinY && point.y <= boxMaxY;
    }

    Vector2 LeftSide()
    {
        float leftX = cameraMinX - _spawnPadding; 
        float randomY = Random.Range(cameraMinY, cameraMaxY);

        return new Vector2(leftX, randomY);
    }

    Vector2 RightSide()
    {
        float rightX = cameraMaxX + _spawnPadding;
        float randomY = Random.Range(cameraMinY, cameraMaxY);

        return new Vector2(rightX, randomY);
    }

    Vector2 TopSide()
    {
        float topY = cameraMaxY + _spawnPadding;
        float randomX = Random.Range(cameraMinX, cameraMaxX);
        return new Vector2(randomX, topY);
    }

    Vector2 BottomSide()
    {
        float bottomY = cameraMinY - _spawnPadding;
        float randomX = Random.Range(cameraMinX, cameraMaxX);

        return new Vector2(randomX, bottomY);
    }
}
