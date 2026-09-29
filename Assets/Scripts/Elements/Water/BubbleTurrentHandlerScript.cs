using UnityEngine;

public class BubbleTurrentHandlerScript : MonoBehaviour
{

    [SerializeField] GameObject bubblePrefab;

    [SerializeField] float spawnCooldown = 7.5f;

    GameObject player;

    float _originalSpawnCooldown;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _originalSpawnCooldown = spawnCooldown;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position; 

        spawnCooldown -= Time.deltaTime;

        if (spawnCooldown <= 0) SpawnBubble();
    }
    

    void SpawnBubble()
    {
        Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        spawnCooldown = _originalSpawnCooldown; 
    }
    
}
