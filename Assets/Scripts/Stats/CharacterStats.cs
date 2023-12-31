﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Item_and_Inventory;
using UnityEngine;
using Random = UnityEngine.Random;

public enum NextAilment
{
    none,
    ignite,
    chill,
    shock
}

public class CharacterStats : MonoBehaviour
{
    private Entity entity;
    private EntityFX fx;
    private ItemDrop itemDrop;

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

    [SerializeField] private float ailmentDuration = 4f;

    [SerializeField] private GameObject shockStrikePrefabs;

    private float ignitedTimer;
    private float shockedTimer;
    private float chilledTimer;

    private float igniteDamageCooldown = .3f;
    private float igniteDamageTimer;

    private int igniteDamage;
    private int shockDamage;

    [SerializeField] private int currentHealth;
    public event EventHandler onHealthChanged;

    public NextAilment nextAilment;

    public bool isDead { get; private set; }

    public int IgniteDamage
    {
        set => igniteDamage = value;
    }

    public int ShockDamage
    {
        set => shockDamage = value;
    }

    protected virtual void Start()
    {
        // critChance.Value = 150;
        currentHealth = MaxHealthValue;
        entity = GetComponent<Entity>();
        fx = GetComponent<EntityFX>();
        itemDrop = GetComponent<ItemDrop>();
        SetNextAilmentToApply();
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

        if (isIgnited)
        {
            ApplyIgniteDamage();
        }
    }

    public void IncreaseStatBy(int modifier, float duration, Stats statToModifier)
    {
        StartCoroutine(StatModCoroutine(modifier, duration, statToModifier));
    }

    private IEnumerator StatModCoroutine(int modifier, float duration, Stats statToModifier)
    {
        statToModifier.AddModifier(modifier);

        yield return new WaitForSeconds(duration);

        statToModifier.RemoveModifier(modifier);
    }

    public void IncreaseHealthFor1S(int amountHealth, int timeToIncrease)
    {
        StartCoroutine(HealthIncreaseCoroutine(amountHealth, timeToIncrease));
    }

    private IEnumerator HealthIncreaseCoroutine(int amountHealth, int timeToIncrease)
    {
        var increaseFor1s = amountHealth / timeToIncrease;
        var sumIncrease = 0;
        while (sumIncrease < amountHealth)
        {
            yield return new WaitForSeconds(1);
            IncreaseHealthBy(increaseFor1s);
            sumIncrease += increaseFor1s;
        }
    }

    private void ApplyIgniteDamage()
    {
        if (igniteDamageTimer < 0 && isIgnited)
        {
            DecreaseHealthBy(igniteDamage);
            if (currentHealth <= 0)
            {
                isIgnited = false;
                Debug.Log("ignite damage");
                Die();
            }

            igniteDamageTimer = igniteDamageCooldown;
        }
    }

    public virtual void DoDamage(CharacterStats target)
    {
        if (TargetCanAvoidAttack(target)) return;

        var totalDamage = damage.Value + strength.Value;

        if (CanCrit())
            totalDamage = CalculateCriticalDamage(totalDamage);

        totalDamage = CheckTargetArmor(target, totalDamage);
        target.TakeDamage(totalDamage);
        DoMagicDamage(target);

        //if invnteroy current weapon has fire effect
        // then DoMagicalDamage(_targetStats);
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
        target.ApplyAilments(nextAilment);
    }

    private int CheckTargetResistance(CharacterStats target, int totalMagicDamage)
    {
        totalMagicDamage -= target.magicResistance.Value + target.intelligence.Value * 3;
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }

    public virtual void ApplyAilments(NextAilment nextAilment)
    {
        if (nextAilment == NextAilment.ignite)
        {
            isIgnited = true;
            ignitedTimer = ailmentDuration;
            fx.IgniteFxFor(ailmentDuration);
        }
        else if (nextAilment == NextAilment.chill)
        {
            if(isChilled) return; //TODO: Tạm thời/
            isChilled = true;
            chilledTimer = ailmentDuration;
            var slowPercentage = .2f;
            entity.SlowEntityBy(slowPercentage, ailmentDuration);
            fx.ChillFxFor(ailmentDuration);
        }
        else if (nextAilment == NextAilment.shock)
        {
            if (!isShocked) ApplyShock(true);
            else
            {
                if (GetComponent<Player.Player>() != null) return;
                HitNearestTargetWithShockStrike();
            }
        }
    }

    public void SetNextAilmentToApply() { nextAilment = NextAilmentToApply(); }

    private NextAilment NextAilmentToApply()
    {
        var fireDamage = this.fireDamage.Value;
        var iceDamage = this.iceDamage.Value;
        var lightingDamage = this.lightingDamage.Value;

        if (fireDamage == 0 && iceDamage == 0 && lightingDamage == 0) return NextAilment.none;
        var canApplyAilment = !isIgnited && !isShocked && !isChilled;
        if (canApplyAilment)
        {
            var listAilments = new List<(NextAilment, int)>()
            {
                (NextAilment.ignite, fireDamage), (NextAilment.chill, iceDamage), (NextAilment.shock, lightingDamage)
            };
            listAilments = listAilments.OrderBy(item => item.Item2).ToList();
            var totalDamage = fireDamage + iceDamage + lightingDamage;
            var firstDamageRatio = (double)listAilments[0].Item2 / totalDamage;
            var secondDamageRatio = (double)listAilments[1].Item2 / totalDamage;

            double randomValue = Random.value;
            if (randomValue < firstDamageRatio) return listAilments[0].Item1;
            if (randomValue < firstDamageRatio + secondDamageRatio) return listAilments[1].Item1;
            return listAilments[2].Item1;
        }

        return NextAilment.none;
    }

    public void ApplyShock(bool shock)
    {
        if (isShocked) return;
        isShocked = shock;
        shockedTimer = ailmentDuration;
        fx.ShockFxFor(ailmentDuration);
    }

    private void HitNearestTargetWithShockStrike()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        var closestDistance = Mathf.Infinity;
        Transform closestTarget = null;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy.Enemy>() != null &&
                Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestTarget = hit.transform;
                }

                if (closestTarget == null) // delete if you don't want shocked target to be hit by shock strike
                    closestTarget = transform;
            }
        }

        if (closestTarget == null) return;
        var newThunder = Instantiate(shockStrikePrefabs, transform.position, Quaternion.identity);
        newThunder.GetComponent<ShockStrike>()
            .Setup(shockDamage, closestTarget.GetComponent<CharacterStats>());
    }

    public virtual void TakeDamage(int damage)
    {
        DecreaseHealthBy(damage);
        entity.DamageImpact();
        fx.StartCoroutine("FlashFX");
        if (currentHealth < 0)
        {
            Die();
        }
    }

    public virtual void IncreaseHealthBy(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealthValue);
        OnHealthChanged();
    }

    protected virtual void DecreaseHealthBy(int damage)
    {
        currentHealth -= damage;
        OnHealthChanged();
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
        if (isDead) return;
        Debug.Log(this.gameObject.name + " was died!");
        isDead = true;
        entity.Die();
        itemDrop.GenerateDrop();
    }

    public virtual Stats StatOfType(StatType stats)
    {
        return stats switch
        {
            StatType.strength => strength,
            StatType.agility => agility,
            StatType.intelligence => intelligence,
            StatType.vitality => vitality,
            StatType.damage => damage,
            StatType.critChance => critChance,
            StatType.critPower => critPower,
            StatType.maxHealth => maxHealth,
            StatType.armor => armor,
            StatType.evasion => evasion,
            StatType.magicResistance => magicResistance,
            StatType.fireDamage => fireDamage,
            StatType.iceDamage => iceDamage,
            StatType.lightingDamage => lightingDamage,
            _ => null
        };
    }

    public int MaxHealthValue => maxHealth.Value + vitality.Value * 5;

    public float GetHealthAmountNormalized => (float)currentHealth / MaxHealthValue;
    protected virtual void OnHealthChanged() { onHealthChanged?.Invoke(this, EventArgs.Empty); }

    public int CurrentHealth => currentHealth;
    public NextAilment GetNextAilment => nextAilment;
}