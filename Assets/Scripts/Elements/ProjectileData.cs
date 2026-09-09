using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public Sprite icon;

    public float damage; 

    public float speed;

    public float lifeTime;  

    public float projectileSpawnRate;

    public int maxSpawns;
}
