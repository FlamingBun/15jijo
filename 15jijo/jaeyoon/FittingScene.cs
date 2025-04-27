using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public class FittingScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Fitting;

    public override SceneState InputHandle()
    {
        Player? player = GameManager.instance.player;
        List<Item>? items = GameManager.instance.havingItems;
        List<Item>? equipmentItems = items.Where(item => item.ItemType == ItemType.Equipment).ToList();

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
                if (itemIndex >= 0 && itemIndex < equipmentItems.Count)
                {
                    EquipmentItem selectedItem = (EquipmentItem)equipmentItems[itemIndex];
                    switch(selectedItem.EquipmentItemType)
                    {
                        case EquipmentItemType.Weapon:
                            if (player.equippedAttackPowerItem == selectedItem)
                            {
                                player.OffWeapon(selectedItem);
                                Console.WriteLine("장비를 해제 하였습니다.");
                                Thread.Sleep(1500);
                                return SceneState.Fitting;
                            }
                            else
                            {
                                player.OnWeapon(selectedItem);
                                Console.WriteLine("장비를 장착 하였습니다.");
                                Thread.Sleep(1500);
                                return SceneState.Fitting;
                            }
                        case EquipmentItemType.Armor:
                            if (player.equippedDefensivePowerItem == selectedItem)
                            {
                                player.OffArmor(selectedItem);
                                Console.WriteLine("장비를 해제 하였습니다.");
                                Thread.Sleep(1500);
                                return SceneState.Fitting;
                            }
                            else
                            {
                                player.OnArmor(selectedItem);
                                Console.WriteLine("장비를 장착 하였습니다.");
                                Thread.Sleep(1500);
                                return SceneState.Fitting;
                            }
                        default:
                            Console.WriteLine("오류입니다.");
                            break;
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