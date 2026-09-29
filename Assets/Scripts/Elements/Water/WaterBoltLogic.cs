
using UnityEngine;

public class WaterBoltLogic : ProjectileClass
{

    Rigidbody2D rb;
    SpriteRenderer _sprite;

    PlayerShooting playerShooting;

    int collideAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = Data.icon;
        rb = GetComponent<Rigidbody2D>();
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
        MoveTowardsTarget(playerShooting.enemyToShootAt, Data.speed, gameObject.GetComponent<Rigidbody2D>());

    }

      private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyContacts>().TakeDamage(Data.damage);
            collideAmount++;

            if (collideAmount >= Data.collideAmount) Destroy(this.gameObject);
        }
    }
}

