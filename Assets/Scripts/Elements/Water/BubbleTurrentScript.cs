using System.Collections;
using UnityEngine;

public class BubbleTurrentScript : MonoBehaviour
{
    [SerializeField] AbilityData abData;

    SpriteRenderer sR;

    [SerializeField] GameObject bulletPrefab;

    [SerializeField] int numberOfBullets = 8;

    [SerializeField] float bulletSpeed = 15f;

    float shootCoolDown = 3.5f;

    bool Alive = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        sR.sprite = abData.icon;
        StartCoroutine(Shoot360());
        StartCoroutine(DestroyBubble());
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator DestroyBubble()
    {
        yield return new WaitForSeconds(abData.cooldown);
        Alive = false; 
        Destroy(gameObject);
    }

    IEnumerator Shoot360()
    {
        while (Alive)
        {
            float angleStep = 360f / numberOfBullets;
            float angle = 0f;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float bulletDirectionX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float bulletDirectionY = Mathf.Sin(angle * Mathf.Deg2Rad);

                Vector3 bulletDirection = new Vector3(bulletDirectionX, bulletDirectionY, 0f);

                float rotationZ = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
                Quaternion bulletRotation = Quaternion.Euler(0f, 0f, rotationZ);

                GameObject _bulletPrefab = Instantiate(bulletPrefab, transform.position, bulletRotation);

                Rigidbody2D rb = _bulletPrefab.GetComponent<Rigidbody2D>();
                if (rb != null) rb.linearVelocity = bulletDirection * bulletSpeed;

                angle += angleStep;

            }

            yield return new WaitForSeconds(shootCoolDown);
        }
     



    }
}
