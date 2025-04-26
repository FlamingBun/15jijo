using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public class EatingScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Eating;

    public override SceneState InputHandle()
    {
        Player? player = GameManager.instance.player;
        List<Item>? items = GameManager.instance.havingItems;
        List<Item>? consumableItems = items.Where(item => item.ItemType == ItemType.Consumable).ToList();

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
                if (itemIndex >= 0 && itemIndex < consumableItems.Count)
                {
                    Item selectedItem = consumableItems[itemIndex];
                    //player.UseItem((EquipmentItem)selectedItem);
                    Console.WriteLine("아이템을 사용 하였습니다.");
                    Thread.Sleep(1500);
                    return SceneState.Eating;
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