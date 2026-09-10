using System.Collections;
using UnityEngine;

public class WaterBoltLogic : ProjectileClass
{

    [SerializeField] private ProjectileData _waterBoltData;

    Rigidbody2D rb;
    SpriteRenderer _sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = _waterBoltData.icon;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyBullet(_waterBoltData.lifeTime));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
