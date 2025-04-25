using System;

namespace _15jijo
{
    public class SceneBuy : BaseScene
    {
        public override SceneState SceneState { get; protected set; } = SceneState.Buy;

        public override SceneState InputHandle()
        {
            DrawScene(SceneState);

            Console.WriteLine("===== [아이템 구매] =====");
            Console.WriteLine($"보유 골드: {GameManager.player.Gold} G\n");

            var shopItems = GameManager.shop.ShopItems;
            for(int i=0; i<shopItems.Count; i++)
            {
                var it = shopItems[i];
                string purchasedMark = it.IsPurchased ? "(구매완료)" : $"{it.Price} G";
                if(it.Type == ItemType.Armor)
                    Console.WriteLine($"{i+1}. {it.Name} | 방어+{it.Defense} | {it.Description} | {purchasedMark}");
                else
                    Console.WriteLine($"{i+1}. {it.Name} | 공격+{it.Attack} | {it.Description} | {purchasedMark}");
            }

            Console.WriteLine("\n0. 상점 Main으로 돌아가기\n");
            selectionCount = shopItems.Count; // 0 ~ shopItems수

            Console.Write("구매할 아이템 번호 입력: ");
            string input = Console.ReadLine();

            int idx=-1;
            bool valid = ConsoleHelper.CheckUserInput(input, selectionCount, ref idx);
            if(!valid)
                return SceneState.Buy;
            if(idx == 0)
                return SceneState.Shop;

            if(idx >=1 && idx <= shopItems.Count)
            {
                var target = shopItems[idx-1];
                if(target.IsPurchased)
                {
                    Console.WriteLine("이미 구매 완료된 아이템입니다!");
                }
                else
                {
                    if(GameManager.player.Gold >= target.Price)
                    {
                        GameManager.player.Gold -= target.Price;
                        target.IsPurchased = true;
                        // 인벤토리에 추가
                        GameManager.player.Inventory.Add(new Item(
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
            Console.WriteLine("\n계속하려면 엔터...");
            Console.ReadLine();
            return SceneState.Buy;
        }
    }
}
