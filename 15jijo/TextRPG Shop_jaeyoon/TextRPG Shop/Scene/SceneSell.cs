using System;

namespace _15jijo
{
    public class SceneSell : BaseScene
    {
        public override SceneState SceneState { get; protected set; } = SceneState.Sell;

        public override SceneState InputHandle()
        {
            DrawScene(SceneState);

            Console.WriteLine("===== [아이템 판매] =====");
            Console.WriteLine($"보유 골드: {GameManager.player.Gold} G");
            Console.WriteLine("(이미 상점에서 구매하지 않은 아이템은 0원 or 판매 불가 가정)\n");

            var inventory = GameManager.player.Inventory;
            for(int i=0; i<inventory.Count; i++)
            {
                var it = inventory[i];
                int sellPrice = it.Price/2;

                if(it.Price <= 0)
                    Console.WriteLine($"{i+1}. {it.Name} [판매 불가: 0G]");
                else
                    Console.WriteLine($"{i+1}. {it.Name} [판매가: {sellPrice}G] (구매가 {it.Price}G)");
            }

            Console.WriteLine("\n0. 상점 Main으로 돌아가기\n");
            selectionCount = inventory.Count; // 0 ~ 인벤토리 수

            Console.Write("판매할 아이템 번호: ");
            string input = Console.ReadLine();

            int idx=-1;
            bool valid = ConsoleHelper.CheckUserInput(input, selectionCount, ref idx);
            if(!valid)
                return SceneState.Sell;
            if(idx == 0)
                return SceneState.Shop;

            if(idx >=1 && idx<= inventory.Count)
            {
                var selected = inventory[idx-1];
                if(selected.Price > 0)
                {
                    int gain = selected.Price /2;
                    GameManager.player.Gold += gain;
                    Console.WriteLine($"{selected.Name} 판매 완료! Gold+{gain}");
                    inventory.Remove(selected);
                }
                else
                {
                    Console.WriteLine($"'{selected.Name}'은(는) 판매 불가 아이템입니다!");
                }
            }
            Console.WriteLine("\n계속하려면 엔터...");
            Console.ReadLine();
            return SceneState.Sell;
        }
    }
}
