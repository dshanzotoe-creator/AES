using System.Collections;
using UnityEngine;

public class MeteorLogic : MonoBehaviour
{

    [SerializeField] AbilityData abData;

    Rigidbody2D rb;

    


    [SerializeField] float lifeTime = 2f;
    [SerializeField] float speed = 30f;

    GameObject target;

    SpriteRenderer _sprite;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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

    private void OnEnable()
    {
        target = GameObject.Find("MeteorTarget");
        rb = GetComponent<Rigidbody2D>();
        MoveTowardsTarget(target, speed, rb);
        StartCoroutine(StartLifeTime());
    }

    


    void MoveTowardsTarget(GameObject target, float projectileSpeed, Rigidbody2D rb)
     {
        if (target == null)
        {
            Destroy(this.gameObject); return;
        }

        Vector2 direction = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + -90);
        rb.linearVelocity = direction * projectileSpeed;
     }

    void DisableMeteor()
    {
        gameObject.SetActive(false);
    }

   

    

    IEnumerator StartLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        DisableMeteor();
        yield return null;
    }
}
