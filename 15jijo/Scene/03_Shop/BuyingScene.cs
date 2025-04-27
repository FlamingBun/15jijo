public class BuyingScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Buying;

    Player? player;
    List<Item>? itemList;
    List<Item>? havingItems;
    List<Item>? purchasedItems;

    private BuyResult BuyItem(Item item)
    {
        if (item.ItemType == ItemType.Equipment)
        {
            if (purchasedItems.Any(p => p.ItemName == item.ItemName))
            {
                return BuyResult.AlreadyPurchased;
            }
            if (!player.SpendGold(item.ItemPrice))
            {
                return BuyResult.NotEnoughGold;
            }

            havingItems.Add(item);
            purchasedItems.Add(item);
            return BuyResult.Success;
        }
        else
        {
            ConsumeItem consumeItem = (ConsumeItem)item;

            if (!player.SpendGold(item.ItemPrice))
            {   
                return BuyResult.NotEnoughGold;
            }

            if (havingItems.Any(h => h.ItemName == item.ItemName))
            {
                consumeItem.Buy();
            }
            else
            {
                havingItems.Add(item);
            }
            return BuyResult.Success;
        }
    }
    public override SceneState InputHandle()
    {
        if (GameManager.instance != null)
        {
            player = GameManager.instance.player;
            itemList = DataManager.instance.itemDatas.GetDatas();
            havingItems = GameManager.instance.havingItems;
            purchasedItems = GameManager.instance.purchasedItems;
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
                if (itemList != null && itemIndex >= 0 && itemIndex < itemList.Count)
                {
                    BuyResult buyResult = BuyItem(itemList[itemIndex]);
                    switch (buyResult)
                    {
                        case BuyResult.AlreadyPurchased:
                            Console.WriteLine("이미 구매한 아이템입니다.");
                            Thread.Sleep(1500);
                            break;
                        case BuyResult.NotEnoughGold:
                            Console.WriteLine("골드가 부족합니다.");
                            Thread.Sleep(1500);
                            break;
                        case BuyResult.Success:
                            Console.WriteLine("아이템을 구매하였습니다.");
                            Thread.Sleep(1500);
                            return SceneState.Buying;
                    }
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