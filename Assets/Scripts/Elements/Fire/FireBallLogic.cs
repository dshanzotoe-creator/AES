using System.Collections;
using UnityEngine;

public class FireBallLogic : ProjectileClass
{

    Rigidbody2D rb;
    SpriteRenderer _sprite; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = Data.icon;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyBullet(Data.lifeTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
