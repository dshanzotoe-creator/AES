using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : PlayerStats
{
    //Add logic where the bullets that are spawned changes with the element(s) picked. 
    [SerializeField] GameObject projectile;

    [Header("Variables")]
    [SerializeField] float range = 10.0f;
    [SerializeField] float shotSpeedCooldown = 2.0f;
    
    
    [SerializeField] List<GameObject> enemies = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AddEnemyToList(); 
        FindClosestEnemy();
    }

    void AddEnemyToList()
    {
        GameObject[] foundAllEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in foundAllEnemies)
        {
            if(enemy != null && !enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
        }
    }

    void FindClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = range; 

        foreach (GameObject enemy in enemies)
        {
            float distanceFromEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if(distanceFromEnemy < closestDistance)
            {
                closestDistance = distanceFromEnemy;
                closestEnemy = enemy;
            }
        }
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range); 
    }


    IEnumerator ShootProjectile()
    {
        //Add projectile logic once elements are completed.
        yield return null;
    }

    
}
