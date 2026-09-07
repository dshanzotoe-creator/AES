using UnityEngine;

public class PlayerCollision : PlayerStats, IDamageable
{
    float _currentHealth;

    float _maxHealth; 

    private void Awake()
    {
        _maxHealth = health;
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void TakeDamage(float damage)
    {
        int _roundedDamage = Mathf.RoundToInt(damage);

        _currentHealth -= _roundedDamage / defenseModifier;
    }

    public void Death()
    {
        if(_currentHealth <= 0)
        {
            //End Game Logic Since It Is The Player
        }
    }
}
