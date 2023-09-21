using DefaultNamespace;
using Item_and_Inventory;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player.Player player;

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player.Player>();
    }

    protected override void DecreaseHealthBy(int damage)
    {
        base.DecreaseHealthBy(damage);
        // var currentArmor = Inventory.Instance.GetEquipmentByType(EquipmentType.Armor);
        //
        // if(currentArmor) currentArmor.ExecuteItemEffect(player.transform);
        CinemachineShake.Instance.SnakeCamera(.6f, .5f);
    }

    protected override void Update()
    {
        base.Update();
    }
}