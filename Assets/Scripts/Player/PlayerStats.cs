using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    protected float movementSpeed;
    
    protected float damageModifier;

    protected float health;

    protected float shotSpeedModifier;

    protected float defenseModifier;

    protected float rangeModifier;

    protected float projectileSpawnRateModifier; 

    private void Awake()
    {
        movementSpeed = 5.0f;

        damageModifier = 1f;

        health = 100f; 

        shotSpeedModifier = 1f;

        defenseModifier = 1f;

        rangeModifier = 1f;

        projectileSpawnRateModifier = 1f;
    }
}
