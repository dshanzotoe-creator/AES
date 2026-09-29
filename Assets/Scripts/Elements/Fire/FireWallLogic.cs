using UnityEngine;

public class FireWallLogic : ProjectileClass
{

    [SerializeField] AbilityData abData;

    SpriteRenderer _sprite;



    protected override void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = abData.icon;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyContacts>().TakeDamage(abData.damage);
        }
    }
}
