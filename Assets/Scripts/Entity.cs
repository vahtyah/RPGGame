using System;
using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Collision Info")] 
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;
    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Component

    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public EntityFX fx { get; private set; }

    #endregion

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
    }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine(HitKnockback());
        Debug.Log(gameObject.name + "was damaged!");
    }
    
    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    #region Velocity

    public virtual void SetZeroVelocity()
    {
        if (isKnocked) return;
        rb.velocity = new Vector2(0, 0);
    }

    public virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked) return;
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    #endregion

    #region Collision

    public virtual bool IsGroundDetected() =>
        Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir,
        wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        var position = groundCheck.position;
        Gizmos.DrawLine(position, new Vector3(position.x, position.y - groundCheckDistance));
        var position1 = wallCheck.position;
        Gizmos.DrawLine(position1, new Vector3(position1.x + wallCheckDistance, position1.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    #endregion

    #region Flip

    public virtual void Flip()
    {
        this.facingRight = !this.facingRight;
        facingDir *= -1;
        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float xVelocity)
    {
        switch (xVelocity)
        {
            case > 0 when !facingRight:
            case < 0 when facingRight:
                Flip();
                break;
        }
    }

    #endregion

    public void MakeTransparent(bool transparent)
    {
        if(transparent)
            sr.color = Color.clear;
        else sr.color = Color.white;
    }
}