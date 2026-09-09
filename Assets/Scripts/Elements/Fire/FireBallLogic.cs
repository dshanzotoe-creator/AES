using System.Collections;
using UnityEngine;

public class FireBallLogic : MonoBehaviour, IProjectile
{
    [SerializeField] private ProjectileData _projectileData;

    Rigidbody2D rb;
    SpriteRenderer _sprite; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = _projectileData.icon;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MoveTowardsTarget(GameObject target)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * _projectileData.speed;
    }


    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_projectileData.lifeTime);
        Destroy(gameObject);
    }

}
