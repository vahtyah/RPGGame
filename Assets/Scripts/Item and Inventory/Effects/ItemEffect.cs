using UnityEngine;

namespace Item_and_Inventory
{
    public class ItemEffect : ScriptableObject
    {
        public virtual void ExecuteEffect(Transform transform)
        {
            Debug.Log("Effect execute!");
        }
    }
}