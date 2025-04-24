using System;

namespace TextRPG
{
    /// <summary>
    /// (4) 아이템 구매 씬
    /// </summary>
    public class SceneBuy : BaseScene
    {
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("===== [아이템 구매] =====");
            Console.WriteLine($"보유 골드: {Program.player.Gold} G");
            Console.WriteLine();
            var shopItems = Program.shop.ShopItems;
            for (int i = 0; i < shopItems.Count; i++)
            {
                Item it = shopItems[i];
                string purchasedMark = it.IsPurchased ? "(구매완료)" : $"{it.Price} G";
                if (it.Type == ItemType.Armor)
                {
                    Console.WriteLine($"{i + 1}. {it.Name} | 방어+{it.Defense} | {it.Description} | {purchasedMark}");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {it.Name} | 공격+{it.Attack} | {it.Description} | {purchasedMark}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 상점 메인으로 돌아가기");
            Console.WriteLine();
            Console.Write("구매할 아이템 번호 입력: ");
        }

        public override void InputHandle()
        {
            string input = Console.ReadLine();
            if (input == "0")
            {
                // 상점 메인으로
                SceneManager.ChangeScene(SceneType.Shop);
                return;
            }

            if (int.TryParse(input, out int idx))
            {
                var shopItems = Program.shop.ShopItems;
                if (idx >= 1 && idx <= shopItems.Count)
                {
                    Item target = shopItems[idx - 1];
                    if (target.IsPurchased)
                    {
                        Console.WriteLine("이미 구매 완료된 아이템입니다!");
                    }
                    else
                    {
                        if (Program.player.Gold >= target.Price)
                        {
                            // 구매 처리
                            Program.player.Gold -= target.Price;
                            target.IsPurchased = true;
                            // 인벤토리에 추가
                            Program.player.Inventory.Add(new Item(
                                target.Name, target.Type,
                                target.Attack, target.Defense,
                                target.Description, target.Price,
                                true, false
                            ));
                            Console.WriteLine($"{target.Name} 구매 성공!");
                        }
                        else
                        {
                            Console.WriteLine("Gold가 부족합니다!");
                        }
                    }
                }
            }
            Console.WriteLine("계속하려면 엔터를 누르세요.");
            Console.ReadLine();
            Render(); // 다시 자기 화면
        }
    }
}
