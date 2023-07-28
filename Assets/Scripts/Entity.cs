using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Collision Info")] 
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck; 
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;
    
    #region Component

    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion
    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
    }
    
    public virtual void ZeroVelocity() => rb.velocity = new Vector2(0, 0);

    public virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    
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
    }
    
    public virtual void Flip()
    {
        this.facingRight = !this.facingRight;
        facingDir *= -1;
        transform.localScale = new Vector2(Mathf.Sign(facingDir), 1f);
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
}