using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TidalBurstLogic : MonoBehaviour
{
    [SerializeField] AbilityData abData;

    [SerializeField] float knockbackDistance = 1.5f;
    [SerializeField] float knockbackDuration = 0.15f;

    float lifeTime;

    private void Start()
    {
        lifeTime = abData.cooldown;
    }

    private void Update()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        collision.GetComponent<EnemyContacts>()
            .TakeDamage(abData.damage);

        StartCoroutine(KnockbackEnemy(collision.gameObject));
    }

    IEnumerator KnockbackEnemy(GameObject enemy)
    {
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.ResetPath();
            agent.enabled = false;
        }

        Vector2 startPosition = enemy.transform.position;

        Vector2 direction =
            ((Vector2)enemy.transform.position -
             (Vector2)transform.position).normalized;

        Vector2 targetPosition =
            startPosition + direction * knockbackDistance;

        float timer = 0f;

        while (timer < knockbackDuration)
        {
            timer += Time.deltaTime;

            float t = timer / knockbackDuration;

            enemy.transform.position = Vector2.Lerp(
                startPosition,
                targetPosition,
                t
            );

            yield return null;
        }

        if (agent != null)
        {
            agent.enabled = true;
        }
    }
}