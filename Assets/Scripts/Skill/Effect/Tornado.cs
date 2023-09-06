using System;
using UnityEngine;

namespace Skill.Test
{
    public class Tornado : Effect
    {
        public static Tornado Create(GameObject prefab, Vector3 position, float dir)
        {
            var hitOj = Instantiate(prefab, position, Quaternion.identity);
            var hitScript = hitOj.GetComponent<Tornado>();
            hitScript.Setup(dir);
            return hitScript;
        }
        
        private float dir;
        
        private void Setup(float dir)
        {
            this.dir = dir;
        }

        protected override void Start()
        {
            base.Start();
            transform.localScale = new Vector3(0, 0);
        }

        protected override void Update()
        {
            timerToDisappear -= Time.deltaTime;
            if(timerToDisappear < 0) Destroy(gameObject);
            transform.position += new Vector3(dir *15 * Time.deltaTime, 0);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1),
                5 * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Enemy.Enemy>() && SkillManager.Instance.lastBreathSkill.Target == null)
            {
                SkillManager.Instance.lastBreathSkill.Target = other.transform;
                SkillManager.Instance.lastBreathSkill.use(); //TODO: fix
            }
        }
    }
}