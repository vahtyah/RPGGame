using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterStats : MonoBehaviour
{
    private Entity entity;
    private EntityFX fx;

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
        // DoMagicDamage(target);
        
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
        Debug.Log("do magic damage" + totalMagicDamage);
        target.TakeDamage(totalMagicDamage);

        if (Mathf.Max(fireDamage, iceDamage, lightingDamage) <= 0) return;

        AttemptToApplyAilment(target, fireDamage, iceDamage, lightingDamage);
    }

    private void AttemptToApplyAilment(CharacterStats target, int fireDamage, int iceDamage, int lightingDamage)
    {
        var canApplyIgnite = fireDamage > iceDamage && fireDamage > lightingDamage;
        var canApplyChill = iceDamage > fireDamage && iceDamage > lightingDamage;
        var canApplyShock = lightingDamage > fireDamage && lightingDamage > iceDamage;

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
                return;
            }
        }

        if (canApplyIgnite) target.IgniteDamage = Mathf.RoundToInt(fireDamage * .2f);
        if (canApplyShock) target.ShockDamage = Mathf.RoundToInt(lightingDamage * .1f);

        target.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
    }

    private int CheckTargetResistance(CharacterStats target, int totalMagicDamage)
    {
        totalMagicDamage -= target.magicResistance.Value + target.intelligence.Value * 3;
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }

    public virtual void ApplyAilments(bool ignite, bool chill, bool shock)
    {
        var canApplyIgnite = !isIgnited && !isShocked && !isChilled;
        var canApplyChill = !isIgnited && !isShocked && !isChilled;
        var canApplyShock = !isIgnited && !isChilled;
        if (ignite && canApplyIgnite)
        {
            isIgnited = ignite;
            ignitedTimer = ailmentDuration;
            fx.IgniteFxFor(ailmentDuration);
        }

        if (chill && canApplyChill)
        {
            isChilled = chill;
            chilledTimer = ailmentDuration;
            var slowPercentage = .2f;
            entity.SlowEntityBy(slowPercentage, ailmentDuration);
            fx.ChillFxFor(ailmentDuration);
        }

        if (shock && canApplyShock)
        {
            if (!isShocked) ApplyShock(shock);
            else
            {
                if (GetComponent<Player.Player>() != null) return;
                HitNearestTargetWithShockStrike();
            }
        }
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
        newThunder.GetComponent<ThunderStrikeController>()
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

    public float GetHealthAmountNormalized => (float)currentHealth / MaxHealthValue;
}