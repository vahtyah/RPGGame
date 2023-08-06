using Player;
using UnityEngine;

namespace Item_and_Inventory
{
    [CreateAssetMenu(fileName = "Ice And Fire Effect", menuName = "Data/Item Effect/Ice and Fire Effect")]
    public class IceAndFireEffect : ItemEffect
    {
        [SerializeField] private GameObject iceAndFireEffectPrefab;
        [SerializeField] private Vector2 newVelocity;

        public override void ExecuteEffect(Transform transform)
        {
            var player = PlayerManager.Instance.player;

            var thirdAttack = player.primaryAttackState.comboCounter == 2;
            
            if(!thirdAttack) return;
            
            var newIceAndFireEffect = Instantiate(iceAndFireEffectPrefab, transform.position, player.transform.rotation);
            newIceAndFireEffect.GetComponent<Rigidbody2D>().velocity = newVelocity * player.GetComponent<Player.Player>().facingDir;
            Destroy(newIceAndFireEffect, 2f);
        }
    }
}