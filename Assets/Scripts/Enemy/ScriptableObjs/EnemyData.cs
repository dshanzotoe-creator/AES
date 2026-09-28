using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float Health;

    public float Damage;

    public float Speed;

    public bool IsShootingEnemy; 

}
