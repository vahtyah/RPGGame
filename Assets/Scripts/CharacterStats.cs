using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stats strength; // 1 point increase damage by 1 and crit.power by 1%
    public Stats agility; // 1 point increase evasion by 1% and crit.chance by 1%
    public Stats intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
    public Stats vitality; // 1 point increase health by 3 or 5 points
    
    [Header("Offensive stats")]
    public Stats damage;
    public Stats critChance;
    public Stats critPower; //default 150%
    
    [Header("Defensive stats")]
    public Stats maxHealth;
    public Stats armor;
    public Stats evasion;
    public Stats magicResistance;

    [Header("Magic stats")]
    public Stats fireDamage;
    public Stats iceDamage;
    public Stats lightingDamage;

    public bool isIgnited;
    public bool isChilled;
    public bool isShocked;

    [SerializeField]private int currentHealth;
    private Entity entity;

    protected virtual void Start()
    {
        critChance.Value = 150;
        currentHealth = maxHealth.Value;
        entity = GetComponent<Entity>();
    }

    public virtual void DoDamage(CharacterStats target)
    {
        if(TargetCanAvoidAttack(target)) return;

        var totalDamage = damage.Value + strength.Value;

        if (CanCrit())
            totalDamage = CalculateCriticalDamage(totalDamage);
        
        totalDamage = CheckTargetArmor(target, totalDamage);
        // target.TakeDamage(totalDamage);
        DoMagicDamage(target);
    }

    public virtual void DoMagicDamage(CharacterStats target)
    {
        var fireDamage = this.fireDamage.Value;
        var iceDamage = this.iceDamage.Value;
        var lightingDamage = this.lightingDamage.Value;
        var totalMagicDamage = fireDamage + iceDamage + lightingDamage + intelligence.Value;
        totalMagicDamage = CheckTargetResistance(target, totalMagicDamage);

        target.TakeDamage(totalMagicDamage);


        var canApplyIgnite = fireDamage > iceDamage && fireDamage > lightingDamage;
        var canApplyChill = iceDamage > fireDamage && iceDamage > lightingDamage;
        var canApplyShock = lightingDamage < fireDamage && lightingDamage > iceDamage;

        target.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
    }

    private static int CheckTargetResistance(CharacterStats target, int totalMagicDamage)
    {
        totalMagicDamage -= target.magicResistance.Value + target.intelligence.Value * 3;
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }

    public virtual void ApplyAilments(bool ignite, bool chill, bool shock)
    {
        if (isChilled || isIgnited || isShocked) return;

        isIgnited = ignite;
        isChilled = chill;
        isShocked = shock;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        entity.DamageEffect();
        if (currentHealth < 0)
        {
            Die();
        }
    }

    private bool CanCrit()
    {
        var totalCriticalChange = critChance.Value + agility.Value;
        return Random.Range(0, 100) <= totalCriticalChange;
    }

    private int CalculateCriticalDamage(int damage)
    {
        var totalCriticalPower = critPower.Value + strength.Value * .01f;
        var critDamage = damage * totalCriticalPower;
        return Mathf.RoundToInt(critDamage);
    }
    private int CheckTargetArmor(CharacterStats target, int totalDamage)
    {
        totalDamage -= target.armor.Value;
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStats target)
    {
        var totalEvasion = target.evasion.Value + target.agility.Value;
        return Random.Range(0, 100) < totalEvasion;
    }

    protected virtual void Die()
    {
        Debug.Log(this.gameObject.name + " was died!");
        entity.Die();
    }
}