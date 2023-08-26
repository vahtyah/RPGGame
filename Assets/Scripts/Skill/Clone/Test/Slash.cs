using System;
using UnityEngine;

namespace Skill.Test
{
    public class Slash : MonoBehaviour
    {
        public static Slash Create(GameObject prefab, Vector3 position, Vector3 dir)
        {
            var slashOj = Instantiate(prefab, position, Quaternion.identity);
            slashOj.transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dir));
            return slashOj.GetComponent<Slash>();
        }
        
        [SerializeField] private float timer;
        private SpriteRenderer sr;
        private void Start() { sr = GetComponent<SpriteRenderer>(); }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer < 0)
                sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * 1.5f));
            if(sr.color.a <= 0)
                Destroy(gameObject);
        }
    }
}