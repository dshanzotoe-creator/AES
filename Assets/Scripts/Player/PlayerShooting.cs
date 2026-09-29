using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : PlayerStats
{
    //Add logic where the bullets that are spawned changes with the element(s) picked. 
    public GameObject projectile;

  //  [SerializeField] ElemntData[] elements = new ElemntData[2];

    bool originalElementActive = true;

    [Header("Variables")]
    [SerializeField] float range = 10.0f;
    [SerializeField] float shotSpeedCooldown = 2.0f;

    public ProjectileData projectileData;

    float originalShotSpeedCooldown;

    bool shooting = false;
    
    [SerializeField] List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private List<GameObject> abilityProjectilePool = new List<GameObject>();

    public GameObject enemyToShootAt;

    private Dictionary<GameObject, float> abilityCooldownTimers = new Dictionary<GameObject, float>();
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalShotSpeedCooldown = shotSpeedCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        AddEnemyToList(); 
        FindClosestEnemy();
        HandleAbilityProjectiles();

        if (enemyToShootAt != null)
        {
            shotSpeedCooldown -= Time.deltaTime; 

            if(shotSpeedCooldown <= 0 && !shooting)
            {
                StartCoroutine(ShootProjectile());
            }
            else
            {
                StopCoroutine(ShootProjectile());
            }

        }
        
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
        enemyToShootAt = null;
        float closestDistance = range; 

        
        for(int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null) enemies.RemoveAt(i);
        }

        foreach (GameObject enemy in enemies)
        {

            float distanceFromEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if(distanceFromEnemy < closestDistance)
            {
                closestDistance = distanceFromEnemy;
                closestEnemy = enemy;
                enemyToShootAt = closestEnemy;
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
        shooting = true;
        int projectilesSpawned = 0; 
        projectileData = projectile.GetComponent<ProjectileClass>().Data;

        //Add projectile logic once elements are completed.
        while (projectilesSpawned < projectileData.maxSpawns)
        {
            if (enemyToShootAt == null)
            {
                break; 
            }

            Instantiate(projectile, transform.position, Quaternion.identity);

            projectilesSpawned++;
            yield return new WaitForSeconds(projectileData.projectileSpawnRate);
        }

        shotSpeedCooldown = originalShotSpeedCooldown / projectileSpawnRateModifier;
        shooting = false;

        yield return null;
    }

    public void CheckForAbilityProjectile()
    {
        GameObject[] foundAbilityProjectiles = GameObject.FindGameObjectsWithTag("AbilityProjectile");

        foreach(GameObject abilityProjectile in foundAbilityProjectiles)
        {
            if (abilityProjectilePool.Contains(abilityProjectile)) continue;

            AbilityProjectile _abilityScript = abilityProjectile.GetComponent<AbilityProjectile>();

            if (_abilityScript == null || _abilityScript.Data == null) continue; 


            abilityProjectilePool.Add(abilityProjectile);

            abilityCooldownTimers.Add(abilityProjectile, _abilityScript.Data.cooldown);

            abilityProjectile.SetActive(false);
        }
    }

    void HandleAbilityProjectiles()
    {
        foreach(GameObject abilityProjectile in abilityProjectilePool)
        {
            if (abilityCooldownTimers[abilityProjectile] <= 0f)
            {
                if (enemyToShootAt == null) continue;

                if (abilityProjectile.activeSelf) continue;

                AbilityProjectile _abilityScript = abilityProjectile.GetComponent<AbilityProjectile>();

                abilityProjectile.transform.position = transform.position;

                abilityProjectile.SetActive(true);

                abilityCooldownTimers[abilityProjectile] = _abilityScript.Data.cooldown; 
            }
        }
    }
}
