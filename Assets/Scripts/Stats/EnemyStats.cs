using Item_and_Inventory;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy.Enemy enemy;
    private ItemDrop itemDrop;
    [Header("Level details")]
    [SerializeField] private int level = 1;
    
    [Range(0f,1f)]
    [SerializeField] private float percentageModifier = .4f;

    protected override void Start()
    {
        ApplyLevelModifier();
        base.Start();
        enemy = GetComponent<Enemy.Enemy>();
        itemDrop = GetComponent<ItemDrop>();
    }

    private void ApplyLevelModifier()
    {
        Modify(strength);
        Modify(agility);
        Modify(intelligence);
        Modify(vitality);
        
        Modify(damage);
        Modify(critChance);
        Modify(critPower);
        
        Modify(maxHealth);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);
        
        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightingDamage);
    }

    private void Modify(Stats stats)
    {
        for (int i = 0; i < level; i++)
        {
            var modifier = stats.Value * percentageModifier;
            stats.AddModifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        itemDrop.GenerateDrop();
    }
}