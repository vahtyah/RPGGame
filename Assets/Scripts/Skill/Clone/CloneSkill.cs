using System.Collections;
using UnityEngine;

namespace Skill
{
    public class CloneSkill : Skill
    {
        [Header("Clone Info")]
        [SerializeField]
        private GameObject clonePrefab;

        [SerializeField] private float cloneDuration;
        [Space] 
        [SerializeField] private bool canAttack;
        
        [SerializeField] private bool createCloneOnDashStart;
        [SerializeField] private bool createCloneOnDashOver;
        [SerializeField] private bool canCreateCloneOnCounterAttack;
        
        [Header("Duplicate Clone")]
        [SerializeField] private bool canDuplicateClone;
        [SerializeField] private float chanceToDuplicate;

        [Header("CrystalInsteadOfClone")]
        [SerializeField] private bool crystalInsteadOfClone;
        public void CreateClone(Transform cloneTransform, Vector3 offset)
        {
            if (crystalInsteadOfClone) //TODO chuyển sang chiêu Crystal
            {
                SkillManager.Instance.crystalSkill.CreateCrystal();
                return;
            }
            var newClone = Instantiate(clonePrefab);
            newClone.GetComponent<CloneSkillController>().SetUp(player,cloneTransform, cloneDuration, canAttack, offset,
                FindClosestEnemy(newClone.transform), canDuplicateClone,chanceToDuplicate);
        }

        public void CreateCloneOnDashStart()
        {
            if(createCloneOnDashStart)
                CreateClone(player.transform,Vector3.zero);
        }

        public void CreateCloneOnDashOver()
        {
            if(createCloneOnDashOver)
                CreateClone(player.transform,Vector3.zero);
        }

        public void CreateCloneOnCounterAttack(Transform enemyTransform)
        {
            if (canCreateCloneOnCounterAttack)
                StartCoroutine(CreateCloneWithDelay(enemyTransform.transform, new Vector3(1.5f * player.facingDir,0)));
        }

        private IEnumerator CreateCloneWithDelay(Transform transform, Vector3 offset)
        {
            yield return new WaitForSeconds(.4f);
            CreateClone(transform, offset);
        }

        public bool CrystalInsteadOfClone => crystalInsteadOfClone; 
    }
}