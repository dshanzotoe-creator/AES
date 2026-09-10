using UnityEngine;
using System.Collections;

public class ProjectileClass : MonoBehaviour
{
    public void MoveTowardsTarget(GameObject target, float projectileSpeed, Rigidbody2D rb)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * projectileSpeed;
    }


    public IEnumerator DestroyBullet(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
