using UnityEngine;

public class FireWallLogic : ProjectileClass
{

    [SerializeField] AbilityData data;

    SpriteRenderer _sprite;

    Rigidbody2D rb; 

    PlayerShooting playerShooting;


    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = Data.icon;
        rb = GetComponent<Rigidbody2D>();
        playerShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
        gameObject.SetActive(false);

    }

    void SpawnFirelWall()
    {
        MoveTowardsTarget(playerShooting.enemyToShootAt, Data.speed, gameObject.GetComponent<Rigidbody2D>());
        StartCoroutine(DestroyBullet(Data.lifeTime));
    }
}
