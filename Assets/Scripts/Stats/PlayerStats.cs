using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player.Player player;

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player.Player>();
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