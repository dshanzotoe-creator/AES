using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float Health;

    public float Damage;

    public float Speed;

    public int XpGained;

    public bool IsShootingEnemy; 

    public bool IsBoss;

    public GameObject prefab;

    public NavMeshAgent agent;
    public Sprite enemySprite; 
}
