public class FittingScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Fitting;

    private Player? player;
    private List<Item>? equippedItems;
    private List<Item>? items;

    public override SceneState InputHandle()
    {
        if (GameManager.instance != null &&
            GameManager.instance.player != null &&
            GameManager.instance.inventory != null)
        {
            player = GameManager.instance.player;
            equippedItems = GameManager.instance.player.equippedItems;
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
                    return SceneState.Inventory;
                }

                int itemIndex = selectedIndex - 1;
                if (equippedItems != null &&
                    items != null &&
                    itemIndex >= 0 && itemIndex < items.Count)
                {
                    if (equippedItems.Contains(items[itemIndex]))
                    {
                        player.OffEquip(items[itemIndex]);
                        return SceneState.Fitting;
                    }
                    else
                    {
                        player.OnEquip(items[itemIndex]);
                        return SceneState.Fitting;
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