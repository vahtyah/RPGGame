using UnityEngine;

namespace Item_and_Inventory
{
    public class ItemEffect : ScriptableObject
    {
        public virtual void ExecuteEffect(Transform targetTransform)
        {
            Debug.Log("Effect execute!");
        }
    }
}