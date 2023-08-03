using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")] public Stats strength; // 1 point increase damage by 1 and crit.power by 1%
    public Stats agility; // 1 point increase evasion by 1% and crit.chance by 1%
    public Stats intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
    public Stats vitality; // 1 point increase health by 3 or 5 points

    [Header("Offensive stats")] public Stats damage;
    public Stats critChance;
    public Stats critPower; //default 150%

    [Header("Defensive stats")] public Stats maxHealth;
    public Stats armor;
    public Stats evasion;
    public Stats magicResistance;

    [Header("Magic stats")] public Stats fireDamage;
    public Stats iceDamage;
    public Stats lightingDamage;

    public bool isIgnited; //does damage over time
    public bool isChilled; //reduce armor by 20%
    public bool isShocked; //reduce accuracy by 20%

    private float ignitedTimer;
    private float shockedTimer;
    private float chilledTimer;

    private float igniteDamageCooldown = .3f;
    private float igniteDamageTimer;
    private int igniteDamage;

    [SerializeField] private int currentHealth;
    private Entity entity;
    public event EventHandler onHealthChanged;

    public int IgniteDamage {set => igniteDamage = value;}

    protected virtual void Start()
    {
        // critChance.Value = 150;
        currentHealth = MaxHealthValue;
        entity = GetComponent<Entity>();
    }

    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        
        igniteDamageTimer -= Time.deltaTime;

        if (ignitedTimer < 0) isIgnited = false;
        if (shockedTimer < 0) isShocked = false;
        if (chilledTimer < 0) isChilled = false;
        
        if(igniteDamageTimer < 0 && isIgnited)
        {
            DecreaseHealthBy(igniteDamage);
            if(currentHealth <= 0)
            {
                isIgnited = false;
                Die();
            }

            igniteDamageTimer = igniteDamageCooldown;
        }
    }

    public virtual void DoDamage(CharacterStats target)
    {
        if (TargetCanAvoidAttack(target)) return;
        Debug.Log("damage before plus to strength = " + damage.Value);

        var totalDamage = damage.Value + strength.Value;
        Debug.Log("damage after plus to strength = " + totalDamage);

        if (CanCrit())
            totalDamage = CalculateCriticalDamage(totalDamage);
        Debug.Log("damage after plus to crit = " + totalDamage);

        totalDamage = CheckTargetArmor(target, totalDamage);
        
        Debug.Log("damage after minus armor = " + totalDamage);

        target.TakeDamage(totalDamage);
        // DoMagicDamage(target);
    }

    public virtual void DoMagicDamage(CharacterStats target)
    {
        var fireDamage = this.fireDamage.Value;
        var iceDamage = this.iceDamage.Value;
        var lightingDamage = this.lightingDamage.Value;
        var totalMagicDamage = fireDamage + iceDamage + lightingDamage + intelligence.Value;
        totalMagicDamage = CheckTargetResistance(target, totalMagicDamage);

        target.TakeDamage(totalMagicDamage);

        if (Mathf.Max(fireDamage, iceDamage, lightingDamage) <= 0) return;
            
        var canApplyIgnite = fireDamage > iceDamage && fireDamage > lightingDamage;
        var canApplyChill = iceDamage > fireDamage && iceDamage > lightingDamage;
        var canApplyShock = lightingDamage < fireDamage && lightingDamage > iceDamage;

        while (!canApplyChill && !canApplyIgnite && !canApplyShock)
        {
            if (Random.value < .5f && fireDamage > 0)
            {
                canApplyIgnite = true;
                target.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if (Random.value < .5f && iceDamage > 0)
            {
                canApplyChill = true;
                target.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if (Random.value < .5f && lightingDamage > 0)
            {
                canApplyShock = true;
                target.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
            }
        }

        if (canApplyIgnite) target.IgniteDamage = Mathf.RoundToInt(fireDamage * .2f);
        
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

        if (ignite)
        {
            isIgnited = ignite;
            ignitedTimer = 2f;
        }

        if (chill)
        {
            isChilled = chill;
            chilledTimer = 2f;
        }

        if (shock)
        {
            isShocked = shock;
            shockedTimer = 2f;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        DecreaseHealthBy(damage);
        entity.DamageEffect();
        if (currentHealth < 0)
        {
            Die();
        }
    }

    protected virtual void DecreaseHealthBy(int damage)
    {
        currentHealth -= damage;
        onHealthChanged?.Invoke(this, EventArgs.Empty);
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
        if (target.isChilled)
            totalDamage -= Mathf.RoundToInt(target.armor.Value * .8f);
        else
            totalDamage -= target.armor.Value;
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStats target)
    {
        var totalEvasion = target.evasion.Value + target.agility.Value;
        if (isShocked) totalEvasion += 20;
        return Random.Range(0, 100) < totalEvasion;
    }

    protected virtual void Die()
    {
        Debug.Log(this.gameObject.name + " was died!");
        entity.Die();
    }

    private int MaxHealthValue => maxHealth.Value + vitality.Value * 5;

    public float GetHealthAmountNormalized => (float)currentHealth /MaxHealthValue;
}