using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Skill
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

        [Header("Bounce Info")]
        [SerializeField] private float bounceSpeed = 20f;
        [SerializeField] private int bounceAmount;
        [SerializeField] private float bounceGravity;

        [Header("Pierce Info")]
        [SerializeField]
        private int pierceAmount;
        [SerializeField] private float pierceGravity;
        
        [Header("Spin Info")]
        [SerializeField] private float hitCooldown = .35f;
        [SerializeField] private float maxTravelDistance = 7;
        [SerializeField] private float spinDuration = 2;
        [SerializeField] private float spinGravity = 1; 

        [Header("Skill Info")]
        [SerializeField] private GameObject swordPrefab;
        [SerializeField] private Vector2 launchDir;
        [SerializeField] private float swordGravity;
        [SerializeField] private float freezeTimeDuration;
        [SerializeField] private float returnSpeed = 12f;
        public Vector2 finalDir;
        

        [Header("Aim dots")] [SerializeField] private int numberOfDots;
        [SerializeField] private float spaceBetweenDots;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private Transform dotsParent;

        private GameObject[] dots;

        protected override void Start()
        {
            base.Start();
            GenerateDots();

            SetupGravity();
        }

        private void SetupGravity()
        {
            if (swordType == SwordType.Bounce) swordGravity = bounceGravity;
            else if (swordType == SwordType.Pierce) swordGravity = pierceGravity;
            else if (swordType == SwordType.Spin) swordGravity = spinGravity;
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
            var newSwordScript = newSword.GetComponent<SwordSkillController>();
            if (swordType == SwordType.Bounce)
                newSwordScript.SetupBounce(true, bounceAmount,bounceSpeed);
            else if(swordType == SwordType.Pierce)
                newSwordScript.SetupPierce(pierceAmount);
            else if(swordType == SwordType.Spin)
                newSwordScript.SetupSpin(true,maxTravelDistance,spinDuration, hitCooldown);
            newSwordScript.Setup(finalDir, swordGravity, player, freezeTimeDuration, returnSpeed);
            player.AssignNewSword(newSword);
            DotsActive(false);
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
    }
}