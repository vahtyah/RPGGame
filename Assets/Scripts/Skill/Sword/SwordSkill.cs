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

    public class SwordSkill : Skill
    {
        public SwordType swordType = SwordType.Regular;

        [Header("Skill Info")]
        [SerializeField] private GameObject swordPrefab;
        [SerializeField] private float swordGravity = 1f;
        [SerializeField] private Vector2 launchDir;
        [SerializeField] private float freezeTimeDuration;
        [SerializeField] private float returnSpeed = 15f;
        private Vector2 finalDir;

        [Header("Regular Info")] 
        [SerializeField] private float regularGravity;
        
        [Header("Bounce Info")]
        [SerializeField] private float bounceSpeed = 20f;
        [SerializeField] private int bounceAmount;
        [SerializeField] private float bounceGravity;
        [SerializeField] private float bounceRadius = 10f;

        [Header("Pierce Info")]
        [SerializeField] private int pierceAmount;
        [SerializeField] private float pierceGravity = 1f;
        
        [Header("Spin Info")]
        [SerializeField] private float hitCooldown = .35f;
        [SerializeField] private float maxTravelDistance = 7;
        [SerializeField] private float spinDuration = 2;
        [SerializeField] private float spinGravity = 1; 

        [Header("Aim dots")] [SerializeField] private int numberOfDots;
        [SerializeField] private float spaceBetweenDots;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private Transform dotsParent;
        
        private GameObject[] dots;

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

        public void SetupGravity()
        {
            if (swordType == SwordType.Bounce) swordGravity = bounceGravity;
            else if (swordType == SwordType.Pierce) swordGravity = pierceGravity;
            else if (swordType == SwordType.Spin) swordGravity = spinGravity;
        }

        public void CreateSword()
        {
            var newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
            newSword.GetComponent<SwordController>().Setup(swordType);
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

        public int PierceAmount => pierceAmount;

        public float PierceGravity => pierceGravity;

        public float HitCooldown => hitCooldown;

        public float MaxTravelDistance => maxTravelDistance;

        public float SpinDuration => spinDuration;

        public float SpinGravity => spinGravity;

        public Vector2 FinalDir => finalDir;

        public float RegularGravity => regularGravity;

        public float SwordGravity => swordGravity;
    }
}