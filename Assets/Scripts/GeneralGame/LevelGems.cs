using UnityEngine;

public class LevelGems : MonoBehaviour
{
    [SerializeField] float range = 4f;

    float maxRange;

    float moveSpeed = 10f;

    float acceleration = 25f;

    float maxSpeed = 120f;

    public float XpGain = 1; 

    private GameObject _player;

    PlayerLevelSystem levelSystem;

    PlayerCollision playerCollision;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        levelSystem = _player.GetComponent<PlayerLevelSystem>();

        playerCollision = _player.GetComponent<PlayerCollision>();

        maxRange = range * 5;

        if (_player == null)
        {
            Debug.LogError("Player not found in the scene. Please make sure the player has the 'Player' tag.");
        }
    }

    // Update is called once per frame
    void Update()
    {
       float _direction = Vector2.Distance(transform.position, _player.transform.position);

       maxSpeed = Mathf.MoveTowards(moveSpeed,maxSpeed, acceleration * Time.deltaTime);
       range = Mathf.MoveTowards(range, maxRange, 3 * Time.deltaTime);

        if (_direction <= range)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, maxSpeed * Time.deltaTime);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelSystem.currentXP += XpGain;
            playerCollision.Heal();
            Destroy(this.gameObject);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.azure; 
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
