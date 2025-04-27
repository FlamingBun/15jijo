public class SellingScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Selling;

    private Player? player;
    private Player Player
    {
        get
        {
            if (player == null)
            {
                player = GameManager.instance.player;
            }
            return player;
        }
    }
    private List<Item>? items;
    private List<Item>? Items
    {
        get
        {
            if (items == null)
            {
                items = GameManager.instance.havingItems;
            }
            return items;
        }
    }
    private List<Item>? equipmentItems;
    private List<Item>? consumableItems;
    private void SellEquipmentItem(EquipmentItem item)
    {
        switch (item.EquipmentItemType)
        {
            case EquipmentItemType.Weapon:
                if (Player.equippedAttackPowerItem != null && Player.equippedAttackPowerItem.ItemName == item.ItemName)
                {
                    Player.OffWeapon(item);
                }
                break;
            case EquipmentItemType.Armor:
                if (Player.equippedDefensivePowerItem != null && Player.equippedDefensivePowerItem.ItemName == item.ItemName)
                {
                    Player.OffArmor(item);
                }
                break;
            default:
                Console.WriteLine("오류입니다.");
                break;
        }
        Player.UpdateGold((int)(item.ItemPrice * 0.85f));
        Items.Remove(item);
        GameManager.instance.purchasedItems.Remove(item);
    }
    private void SellConsumeItem(ConsumeItem item)
    {
        Player.UpdateGold((int)(item.ItemPrice * 0.85f));
        if (item.ItemCount == 1)
        {
            Items.Remove(item);
        }
        else
        {
            item.Sell();
        }
    }
    public override SceneState InputHandle()
    {
        equipmentItems = Items.Where(item => item.ItemType == ItemType.Equipment).ToList();
        consumableItems = Items.Where(item => item.ItemType == ItemType.Consumable).ToList();

        while (true)
        {
            DrawScene(SceneState);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int selectedIndex) && input == selectedIndex.ToString())
            {
                if (selectedIndex == 0)
                {
                    return SceneState.Shop;
                }

                int itemIndex = selectedIndex - 1;
                if (itemIndex >= 0 && itemIndex < equipmentItems.Count)
                {
                    SellEquipmentItem((EquipmentItem)equipmentItems[itemIndex]);
                    Console.WriteLine("아이템을 판매하였습니다.");
                    Thread.Sleep(1500);
                    return SceneState.Selling;
                }
                else if (itemIndex >= equipmentItems.Count && itemIndex < equipmentItems.Count + consumableItems.Count)
                {
                    SellConsumeItem((ConsumeItem)consumableItems[itemIndex - equipmentItems.Count]);
                    Console.WriteLine("아이템을 판매하였습니다.");
                    Thread.Sleep(1500);
                    return SceneState.Selling;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1500);
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1500);
            }
        }
    }
}