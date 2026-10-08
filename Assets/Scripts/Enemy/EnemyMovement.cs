using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;

    GameObject _player;

    SpriteRenderer sprite; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; 
        agent.updateUpAxis = false;

        _player = GameObject.FindGameObjectWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

      if(agent.enabled == false) return;

        if (agent.desiredVelocity.x < -0.01f)
        {
            sprite.flipX = false;
        }
        else if (agent.desiredVelocity.x > 0.01f)
        {
            sprite.flipX = true;
        }



        SetDestination(_player);
    }

    void SetDestination(GameObject target)
    {
        agent.SetDestination(target.transform.position);
    }
}
