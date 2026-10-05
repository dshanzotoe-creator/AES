using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : PlayerStats
{

    [Header("Normal Projectile")]

    public GameObject projectile;

    public ProjectileData projectileData;



    [Header("Variables")]

    [SerializeField] private float range = 10f;

    [SerializeField] private float shotSpeedCooldown = 2f;


    private float originalShotSpeedCooldown;

    private bool shooting = false;



    [Header("Enemies")]

    [SerializeField]
    private List<GameObject> enemies =
        new List<GameObject>();


    public GameObject enemyToShootAt;



    [Header("Ability Projectile Pool")]

    [SerializeField]
    private List<GameObject> abilityProjectilePool =
        new List<GameObject>();



    private Dictionary<GameObject, float> abilityCooldownTimers
        = new Dictionary<GameObject, float>();


    private Dictionary<GameObject, ElemntData> abilityElementOwners
        = new Dictionary<GameObject, ElemntData>();



    private ElemntData activeElement;



    private void Start()
    {
        originalShotSpeedCooldown =
            shotSpeedCooldown;
    }



    private void Update()
    {
        AddEnemyToList();

        FindClosestEnemy();

        HandleAbilityProjectiles();



        if (projectile != null)
        {
            ProjectileClass projectileClass =
                projectile.GetComponent<ProjectileClass>();


            if (projectileClass != null)
            {
                projectileData =
                    projectileClass.Data;
            }
        }



        if (enemyToShootAt != null
            && projectileData != null)
        {
            shotSpeedCooldown -=
                Time.deltaTime;


            if (shotSpeedCooldown <= 0f
                && !shooting)
            {
                StartCoroutine(
                    ShootProjectile()
                );
            }
        }
    }



    IEnumerator ShootProjectile()
    {
        shooting = true;


        int projectilesSpawned = 0;


        while (projectilesSpawned
               < projectileData.maxSpawns)
        {
            if (enemyToShootAt == null)
            {
                break;
            }


            Instantiate(
                projectile,
                transform.position,
                Quaternion.identity
            );


            projectilesSpawned++;


            yield return new WaitForSeconds(
                projectileData.projectileSpawnRate
            );
        }


        shotSpeedCooldown =
            originalShotSpeedCooldown
            / projectileSpawnRateModifier;


        shooting = false;
    }


    void AddEnemyToList()
    {
        GameObject[] foundAllEnemies =
            GameObject.FindGameObjectsWithTag(
                "Enemy"
            );


        foreach (GameObject enemy
                 in foundAllEnemies)
        {
            if (enemy != null
                && !enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
        }
    }




    void FindClosestEnemy()
    {
        enemyToShootAt = null;


        float closestDistance =
            range;


       
        for (int i = enemies.Count - 1;
             i >= 0;
             i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }


        foreach (GameObject enemy
                 in enemies)
        {
            float distanceFromEnemy =
                Vector2.Distance(
                    transform.position,
                    enemy.transform.position
                );


            if (distanceFromEnemy
                < closestDistance)
            {
                closestDistance =
                    distanceFromEnemy;


                enemyToShootAt =
                    enemy;
            }
        }
    }



    public void RegisterAbilityProjectile(
        GameObject abilityProjectile,
        ElemntData ownerElement)
    {
        if (abilityProjectile == null)
            return;


        if (abilityProjectilePool.Contains(
                abilityProjectile))
        {
            return;
        }


        AbilityProjectile abilityScript =
            abilityProjectile
                .GetComponent<AbilityProjectile>();


        if (abilityScript == null
            || abilityScript.Data == null)
        {
            return;
        }


        abilityProjectilePool.Add(
            abilityProjectile
        );


        abilityElementOwners.Add(
            abilityProjectile,
            ownerElement
        );


        abilityCooldownTimers.Add(
            abilityProjectile,
            abilityScript.Data.cooldown
        );


        abilityProjectile.SetActive(false);
    }



    public void SetActiveElement(
        ElemntData element)
    {


        activeElement = element;
    }


    void HandleAbilityProjectiles()
    {
        foreach (GameObject abilityProjectile
                 in abilityProjectilePool)
        {
            if (abilityProjectile == null)
                continue;


            if (!abilityCooldownTimers
                    .ContainsKey(abilityProjectile))
            {
                continue;
            }


            if (!abilityElementOwners
                    .ContainsKey(abilityProjectile))
            {
                continue;
            }


            ElemntData ownerElement =
                abilityElementOwners[
                    abilityProjectile
                ];




            if (ownerElement != activeElement)
            {
                continue;
            }


            abilityCooldownTimers[
                abilityProjectile
            ] -= Time.deltaTime;


            if (abilityCooldownTimers[
                    abilityProjectile
                ] > 0f)
            {
                continue;
            }


            if (enemyToShootAt == null)
                continue;


            if (abilityProjectile.activeSelf)
                continue;



            AbilityProjectile abilityScript =
                abilityProjectile
                    .GetComponent<AbilityProjectile>();


            ProjectileClass projectileClass =
                abilityProjectile
                    .GetComponent<ProjectileClass>();


            Rigidbody2D rb =
                abilityProjectile
                    .GetComponent<Rigidbody2D>();


            if (abilityScript == null)
                continue;


            if (abilityScript.Data == null)
                continue;


            if (projectileClass == null)
                continue;


            if (projectileClass.Data == null)
                continue;


            if (rb == null)
                continue;



            abilityProjectile.transform.position =
                transform.position;



            rb.linearVelocity =
                Vector2.zero;

            rb.angularVelocity =
                0f;


            abilityProjectile.SetActive(true);


            projectileClass.MoveTowardsTarget(
                enemyToShootAt,
                projectileClass.Data.speed,
                rb
            );



            abilityCooldownTimers[
                abilityProjectile
            ] = abilityScript.Data.cooldown;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color =
            Color.yellow;


        Gizmos.DrawWireSphere(
            transform.position,
            range
        );
    }
}