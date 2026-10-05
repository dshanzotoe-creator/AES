using UnityEngine;

public class EnemyBullet : ProjectileClass
{


    GameObject player;

    Rigidbody2D rb; 

    float speed; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody2D>();

        speed = Data.speed;

        MoveTowardsTarget(player, speed, rb);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
