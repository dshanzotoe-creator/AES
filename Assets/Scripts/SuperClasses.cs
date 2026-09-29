using UnityEngine;
using System.Collections;

public class ProjectileClass : MonoBehaviour
{
    [SerializeField] protected ProjectileData projectileData;

    public ProjectileData Data => projectileData;


    protected virtual void Start()
    {

    }

    public virtual void MoveTowardsTarget(GameObject target, float projectileSpeed, Rigidbody2D rb)
    {
        if(target == null)
        {
            Destroy(this.gameObject); return;
        }
        
            

        Vector2 direction = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + -90);
        rb.linearVelocity = direction * projectileSpeed;
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(EndProjectileLifetime());
    }

    public IEnumerator EndProjectileLifetime()
    {
        yield return new WaitForSeconds(Data.lifeTime);

        if (CompareTag("AbilityProjectile")) gameObject.SetActive(false);
        else Destroy(gameObject); 
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null) rb.linearVelocity = Vector2.zero; 
    }
}


public class AbilityClass : MonoBehaviour
{
    GameObject player;

    public virtual void Update()
    {
        transform.position = player.transform.position;
    }
}