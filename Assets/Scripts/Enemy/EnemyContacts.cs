using UnityEngine;

public class EnemyContacts : MonoBehaviour, IDamageable
{
    [SerializeField] protected EnemyData enemyData;

    public EnemyData Data => enemyData;

    private float currentHealth;

    private void Start()
    {
        currentHealth = Data.Health;
    }




    public void TakeDamage(float damage)
    {
        if(currentHealth <= 0)
        {
            Death();
        }
        else
        {
            currentHealth -= damage;
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}
