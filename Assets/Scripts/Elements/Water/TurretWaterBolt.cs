using System.Collections;
using UnityEngine;

public class TurretWaterBolt : MonoBehaviour
{
    [SerializeField]  ProjectileData data; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(data.lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyContacts>().TakeDamage(data.damage);
        }
    }
}
