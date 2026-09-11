using UnityEngine;

public class FireRingLogic : MonoBehaviour
{
    [SerializeField] AbilityData data;

    [SerializeField] float rotationSpeed = 180.0f;

    GameObject player; 

    SpriteRenderer sprite;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player"); 
        sprite.sprite = data.icon;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        transform.position = player.transform.position;
    }

    void Rotate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + rotationSpeed * Time.deltaTime);
    
    }
}
