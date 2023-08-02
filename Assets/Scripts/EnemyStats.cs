using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy.Enemy enemy;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy.Enemy>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
    }
}