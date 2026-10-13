using UnityEngine;
using System.Collections;

public class BossBullet : MonoBehaviour
{
    [SerializeField] ProjectileData data;

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
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCollision>().TakeDamage(data.damage);
        }
    }
}
