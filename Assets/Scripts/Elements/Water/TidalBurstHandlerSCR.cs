using UnityEngine;

public class TidalBurstHandlerSCR : MonoBehaviour
{
    [SerializeField] GameObject tidalBurst;

    [SerializeField] float spawnCooldown = 4.5f;

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

        if (spawnCooldown <= 0) SpawnTidalBurst();
    }


    void SpawnTidalBurst()
    {
        Instantiate(tidalBurst, transform.position, Quaternion.identity);
        spawnCooldown = _originalSpawnCooldown;
    }
}
