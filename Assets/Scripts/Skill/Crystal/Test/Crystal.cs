using UnityEngine;

namespace Skill.Crystal.Test
{
    public class Crystal : MonoBehaviour
    {
        public static Crystal Create(GameObject prefab, Transform newTransform)
        {
            var crystalGo = Instantiate(prefab);
            var crystalCtr = crystalGo.GetComponent<Crystal>();

            return crystalCtr;
        }
        
        public CrystalTypeMachine typeMachine { get; private set; }

        
    }
}