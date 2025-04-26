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
    private List<EquipmentItem>? equippedItems;
    private List<EquipmentItem>? EquippedItems
    {
        get
        {
            if (items == null)
            {
                equippedItems = GameManager.instance.player.equippedItems;
            }
            return equippedItems;
        }
    }
    private List<Item>? equipmentItems;
    private List<Item>? consumableItems;

    private void SellItem(Item item)
    {
        if (Player.equippedItems.Contains(item))
        {
            Player.OffEquip((EquipmentItem)item);
        }

        Player.UpdateGold((int)(item.ItemPrice * 0.85f));
        Items.Remove(item);
        if (item.ItemType == ItemType.Equipment)
        {
            GameManager.instance.purchasedItems.Remove(item);
        }
    }

    public override SceneState InputHandle()
    {
        equipmentItems = Items.Where(item => item.ItemType == ItemType.Equipment).ToList();
        consumableItems = items.Where(item => item.ItemType == ItemType.Consumable).ToList();

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
                    SellItem(equipmentItems[itemIndex]);
                    Console.WriteLine("아이템을 판매하였습니다.");
                    Thread.Sleep(1500);
                    return SceneState.Selling;
                }
                else if(itemIndex >= equipmentItems.Count && itemIndex < equipmentItems.Count + consumableItems.Count)
                {
                    SellItem(consumableItems[itemIndex - equipmentItems.Count]);
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