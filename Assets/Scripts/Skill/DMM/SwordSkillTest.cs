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
        
        [Header("Bounce Info")]
        [SerializeField] private float bounceSpeed = 20f;
        [SerializeField] private int bounceAmount;
        [SerializeField] private float bounceGravity;
        [SerializeField] private float bounceRadius = 10f;
        
        [Header("Aim dots")] [SerializeField] private int numberOfDots;
        [SerializeField] private float spaceBetweenDots;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private Transform dotsParent;

        private GameObject[] dots;
        

        private void Awake()
        {
        }

        protected override void Start()
        {
            base.Start();
            GenerateDots();
        }

        protected override void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                var aimDirNor = AimDirection().normalized;
                finalDir = new Vector2(aimDirNor.x * launchDir.x,
                    aimDirNor.y * launchDir.y);
            }
            
            if (Input.GetKey(KeyCode.Mouse1))
            {
                for (var i = 0; i < dots.Length; i++)
                {
                    var dot = dots[i];
                    dot.transform.position = DotsPosition(i * spaceBetweenDots);
                }
            }
        }
        
        public void CreateSword()
        {
            var newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
            newSword.GetComponent<Sword>().Setup(swordType, returnSpeed, player);
            DotsActive(false);
            player.AssignNewSword(newSword);
        }
        
        public Vector2 AimDirection()
        {
            var playerPosition = player.transform.position;
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var direction = mousePosition - playerPosition;
            return direction;
        }
        
        public void DotsActive(bool isActive)
        {
            foreach (var dot in dots)
            {
                dot.SetActive(isActive);
            }
        }

        private void GenerateDots()
        {
            dots = new GameObject[numberOfDots];
            for (int i = 0; i < numberOfDots; i++)
            {
                dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
                dots[i].SetActive(false);
            }
        }

        private Vector2 DotsPosition(float t)
        {
            var position = (Vector2)player.transform.position +
                           new Vector2(AimDirection().normalized.x * launchDir.x,
                               AimDirection().normalized.y * launchDir.y) * t +
                           .5f * (Physics2D.gravity * swordGravity) * (t * t);
            return position;
        }

        public float BounceSpeed => bounceSpeed;

        public int BounceAmount => bounceAmount;

        public float BounceGravity => bounceGravity;

        public float BounceRadius => bounceRadius;

        public Player.Player Player => player;

        public float ReturnSpeed => returnSpeed;

        public float FreezeTimeDuration => freezeTimeDuration;
    }
}