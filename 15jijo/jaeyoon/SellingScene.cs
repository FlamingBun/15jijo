public class SellingScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Selling;

    private Player? player;
    private List<Item>? items;

    private void SellItem(Item item)
    {
        if (player != null &&
            player.equippedItems != null &&
            player.equippedItems.Contains(item))
        {
            player.OffEquip((EquipmentItem)item);
        }
        if (player != null &&
            items != null &&
            GameManager.instance != null &&
            GameManager.instance.purchasedItems != null)
        {
            player.UpdateGold((int)(item.ItemPrice * 0.85f));
            GameManager.instance.purchasedItems.Remove(item);
            items.Remove(item);
        }
    }

    public override SceneState InputHandle()
    {
        if (GameManager.instance != null &&
            GameManager.instance.inventory != null)
        {
            player = GameManager.instance.player;
            items = GameManager.instance.havingItems;
        }
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
                if (items != null && itemIndex >= 0 && itemIndex < items.Count)
                {
                    SellItem(items[itemIndex]);
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