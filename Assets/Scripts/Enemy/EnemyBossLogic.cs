using UnityEngine;
using UnityEngine.AI;

public class EnemyBossLogic : MonoBehaviour
{

    BossStates states = new BossStates();

    [SerializeField] NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        states = BossStates.Chasing;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


enum BossStates
{
    Chasing, 
    Shooting, 
    Attacking
}
