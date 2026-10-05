using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhirlPoolLogic : ProjectileClass
{
   [SerializeField] private AbilityData abilityData;

    [SerializeField] private float pullSpeed = 4f;

    [SerializeField] private float pullRadius = 3f;

    SpriteRenderer  _sprite;

    Dictionary<GameObject, float> nextHitTime = new Dictionary<GameObject, float>();

    [SerializeField] float timeBetweenHits = 1f;

    Rigidbody2D rb; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = abilityData.icon;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        // Pull enemy directly toward whirlpool
        collision.transform.position = Vector2.MoveTowards(
            collision.transform.position,
            transform.position,
            pullSpeed * Time.deltaTime);

        if (collision.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;

            if (!nextHitTime.ContainsKey(enemy))
            {
                nextHitTime[enemy] = 0f;
            }

            if (Time.time >= nextHitTime[enemy])
            {
                Debug.Log("Enemy hit by fire ring");
                collision.GetComponent<EnemyContacts>().TakeDamage(abilityData.damage);
                nextHitTime[enemy] = Time.time + timeBetweenHits;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        NavMeshAgent agent = collision.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.enabled = true;
        }
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
