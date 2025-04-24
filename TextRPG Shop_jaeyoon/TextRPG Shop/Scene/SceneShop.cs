using System;

namespace _15jijo
{
    public class SceneShop : BaseScene
    {
        public override SceneState SceneState { get; protected set; } = SceneState.Shop;

        public override SceneState InputHandle()
        {
            DrawScene(SceneState);

            Console.WriteLine("===== [상점 Main] =====");
            Console.WriteLine($"보유 Gold: {GameManager.player.Gold} G\n");
            Console.WriteLine("1. 아이템 구매 (Buy)");
            Console.WriteLine("2. 아이템 판매 (Sell)");
            Console.WriteLine("0. 메인으로 돌아가기\n");

            selectionCount = 2; // 0~2
            Console.Write("원하시는 행동: ");
            string input = Console.ReadLine();

            int num=-1;
            bool valid = ConsoleHelper.CheckUserInput(input, selectionCount, ref num);
            if(!valid)
                return SceneState.Shop;

            switch(num)
            {
                case 0:
                    return SceneState.Main;
                case 1:
                    return SceneState.Buy;
                case 2:
                    return SceneState.Sell;
            }
            return SceneState.Shop;
        }
    }
}
