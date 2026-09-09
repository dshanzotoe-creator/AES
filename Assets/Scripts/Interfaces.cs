using UnityEngine;

public interface IDamageable
{

    public void TakeDamage(float damage);

    public void Death(); 
}

public interface IProjectile
{
    public void MoveTowardsTarget(GameObject target);
}
