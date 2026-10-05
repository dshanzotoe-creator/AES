using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab; 
    [SerializeField] float bulletCooldown; 

     float originalBulletCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalBulletCooldown = bulletCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        bulletCooldown -= Time.deltaTime;

        if (bulletCooldown <= 0)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulletCooldown = originalBulletCooldown;
        }
    }
}
