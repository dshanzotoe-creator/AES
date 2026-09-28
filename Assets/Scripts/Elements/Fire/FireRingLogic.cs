using System.Collections.Generic;
using UnityEngine;

public class FireRingLogic : AbilityClass
{
    [SerializeField] AbilityData data;

    [SerializeField] float rotationSpeed = 180.0f;

    GameObject player; 

    SpriteRenderer sprite;

    Dictionary<GameObject, float> nextHitTime = new Dictionary<GameObject, float>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player"); 
        sprite.sprite = data.icon;
    }

    // Update is called once per frame
    public override void Update()
    {
        Rotate();
        transform.position = player.transform.position;
    }

    void Rotate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + rotationSpeed * Time.deltaTime);
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;

            if (!nextHitTime.ContainsKey(enemy))
            {
                nextHitTime[enemy] = 0f; 
            }

            if(Time.time >= nextHitTime[enemy])
            {
                Debug.Log("Enemy hit by fire ring");
                //collision.GetComponent<EnemyHealth>().TakeDamage(data.damage);
                nextHitTime[enemy] = Time.time + data.cooldown;
            }

            Debug.Log("Enemy hit by fire ring");
            //collision.GetComponent<EnemyHealth>().TakeDamage(data.damage);
        }
    }
}
