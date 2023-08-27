using System;
using UnityEngine;

namespace Skill.Test
{
    public class Slash : Effect
    {
        public static Slash Create(GameObject prefab, Vector3 position, Vector3 dir)
        {
            var slashOj = Instantiate(prefab, position, Quaternion.identity);
            slashOj.transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dir));
            return slashOj.GetComponent<Slash>();
        }
    }
}