using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyBossLogic : MonoBehaviour
{

    BossStates states = new BossStates();

    [SerializeField] NavMeshAgent agent;

    [SerializeField] float range = 9f;
    [SerializeField] float bulletSpeed = 11f;
    [SerializeField] float shootCoolDown = 1f;
    float cooldownTillShootAgain = 0;
  
    [SerializeField] int maxShotTimes = 5;


    [SerializeField] GameObject bulletPrefab;

    GameObject player;

    int numberOfBullets = 12;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        states = BossStates.Chasing;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTillShootAgain > 0)
        {
            cooldownTillShootAgain -= Time.deltaTime;
        }

        switch (states)
        {
            case BossStates.Chasing:
                Chase();
                break;

            case BossStates.Shooting:
                break;
        }
    }

    void Chase()
    {
        agent.enabled = true;

        float distance = Vector2.Distance(transform.position, player.transform.position);



        if (distance < range && cooldownTillShootAgain <= 0)
        {
            states = BossStates.Shooting;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        int shotTimes = 0;
        while (shotTimes < maxShotTimes)
        {
            agent.enabled = false;
            float angleStep = 360f / numberOfBullets;
            float angle = 0f;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float bulletDirectionX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float bulletDirectionY = Mathf.Sin(angle * Mathf.Deg2Rad);

                Vector3 bulletDirection = new Vector3(bulletDirectionX, bulletDirectionY, 0f);

                float rotationZ = Mathf.Atan2(-bulletDirection.x, bulletDirection.y) * Mathf.Rad2Deg;
                Quaternion bulletRotation = Quaternion.Euler(0f, 0f, rotationZ);

                GameObject _bulletPrefab = Instantiate(bulletPrefab, transform.position, bulletRotation);

                Rigidbody2D rb = _bulletPrefab.GetComponent<Rigidbody2D>();
                if (rb != null) rb.linearVelocity = bulletDirection * bulletSpeed;

                angle += angleStep;

            }

            yield return new WaitForSeconds(shootCoolDown);
            shotTimes++;
        }
        
        states = BossStates.Chasing;
        cooldownTillShootAgain = 5f;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}




enum BossStates
{
    Chasing, 
    Shooting
}
