using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Skill.Sword
{
    public enum SwordType
    {
        Regular,
        Bounce,
        Pierce,
        Spin
    }
    public class SwordSkillTest : Skill
    {
        public SwordType swordType = SwordType.Regular;
        
        [Header("Skill Info")]
        [SerializeField] private GameObject swordPrefab;

        public Vector2 launchDir;
        public float swordGravity;
        public float freezeTimeDuration;
        public float returnSpeed = 15f;
        public Vector2 finalDir;
        

        private void Awake()
        {
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                var aimDirNor = AimDirection().normalized;
                finalDir = new Vector2(aimDirNor.x * launchDir.x,
                    aimDirNor.y * launchDir.y);
                Debug.Log("finalDir = " + finalDir);
            }
        }
        
        public void CreateSword()
        {
            var newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
            newSword.GetComponent<Sword>().Setup(swordType);
        }
        
        public Vector2 AimDirection()
        {
            var playerPosition = player.transform.position;
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var direction = mousePosition - playerPosition;
            return direction;
        }
    }
}