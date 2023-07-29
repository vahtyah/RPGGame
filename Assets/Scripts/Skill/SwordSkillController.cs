using UnityEngine;
using Player;
namespace Skill
{
    public class SwordSkillController : MonoBehaviour
    {
        private Animator anim;
        private Rigidbody2D rb;
        private CircleCollider2D col;
        private Player.Player player;

        private void Awake()
        {
            player = PlayerManager.Instance.player;
            col = GetComponent<CircleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
        }

        public void Setup(Vector2 dir, float gravityScale)
        {
            rb.gravityScale = gravityScale;
            rb.velocity = dir;
        }
    }
}