using Player;
using Unity.Mathematics;
using UnityEngine;

namespace Item_and_Inventory
{
    [CreateAssetMenu(fileName = "Thunder strike effect", menuName = "Data/Item Effect/Thunder strike")]
    public class ThunderStrikeEffect : ItemEffect
    {
        [SerializeField] private GameObject thunderStrikePrefab;

        public override void ExecuteEffect(Transform targetTransform)
        {
            var newThunderStrike = Instantiate(thunderStrikePrefab, targetTransform.position, Quaternion.identity);
            Destroy(newThunderStrike, .9f);
        }
    }
}