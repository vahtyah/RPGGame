using System;
using UnityEngine;

public class ShockStrikeController : MonoBehaviour
{
    private CharacterStats targetStats;
    [SerializeField] private float speed = 5f;
    private int damage;
    
    private Animator anim;
    private bool trigger;

    public void Setup(int damage, CharacterStats targetStats)
    {
        this.damage = damage;
        this.targetStats = targetStats;
    }
    private void Start() { anim = GetComponentInChildren<Animator>(); }

    private void Update()
    {
        if (trigger || !targetStats) return;

    transform.position =
            Vector2.MoveTowards(transform.position, targetStats.transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetStats.transform.position) < .1f)
        {
            anim.transform.localRotation = Quaternion.identity;
            transform.localRotation = Quaternion.identity;
            anim.transform.localScale = new Vector3(3, 3);
            anim.transform.localPosition = new Vector3(0, .5f);
            
            trigger = true;
            anim.SetTrigger("Hit");
            Invoke("DamageAndSeflDestroy",.2f);
        }
    }

    private void DamageAndSeflDestroy()
    {
        // targetStats.ApplyShock(true);
        Debug.Log("damage and sefl destroy");
        targetStats.TakeDamage(damage);
        Destroy(gameObject, .4f);
    }
}