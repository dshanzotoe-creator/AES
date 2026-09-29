using UnityEngine;

public class MeteorLogic : ProjectileClass
{

    [SerializeField] AbilityData abData;

    SpriteRenderer _sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = abData.icon;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit");
            collision.GetComponent<EnemyContacts>().TakeDamage(abData.damage);
        }
    }
}
