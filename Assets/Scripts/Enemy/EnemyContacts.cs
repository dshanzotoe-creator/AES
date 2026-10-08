using System.Collections;
using UnityEngine;

public class EnemyContacts : MonoBehaviour, IDamageable
{
    [SerializeField] protected EnemyData enemyData;

    public EnemyData Data => enemyData;

    [SerializeField] GameObject XPGem;

    [SerializeField] private float currentHealth;


    public void SetData(EnemyData newData)
    {
        enemyData = newData;
    }

    private void Start()
    {
        currentHealth = Data.Health;    
    }


    private void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage)
    {
 
        currentHealth -= damage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(Data.Damage);
            }
        }
    }



    public void Death()
    {
        GameObject xpGem = Instantiate(XPGem, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
