using UnityEngine;

public class EnemyContacts : MonoBehaviour, IDamageable
{
    [SerializeField] protected EnemyData enemyData;

    public EnemyData Data => enemyData;

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

    public void Death()
    {
        Destroy(this.gameObject);
    }
}
