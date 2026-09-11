using UnityEngine;
using System.Collections;

public class ProjectileClass : MonoBehaviour
{
    [SerializeField] protected ProjectileData projectileData;

    public ProjectileData Data => projectileData;


    private void Start()
    {

        StartCoroutine(DestroyBullet(Data.lifeTime));
    }

    public void MoveTowardsTarget(GameObject target, float projectileSpeed, Rigidbody2D rb)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + -90);
        rb.linearVelocity = direction * projectileSpeed;
    }


    public IEnumerator DestroyBullet(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
