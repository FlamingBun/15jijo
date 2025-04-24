using System;

namespace TextRPG
{
    /// <summary>
    /// (3) 상점 씬
    /// </summary>
    public class SceneShop : BaseScene
    {
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("===== [상점 Main] =====");
            Console.WriteLine("[보유 Gold] " + Program.player.Gold + " G");
            Console.WriteLine("상점에 오신 것을 환영합니다.");
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매 (Buy)");
            Console.WriteLine("2. 아이템 판매 (Sell)");
            Console.WriteLine("0. 나가기 -> 메인 메뉴");
            Console.WriteLine();
            Console.Write("원하시는 행동: ");
        }

        public override void InputHandle()
        {
            string input = Console.ReadLine();
            if (input == "0")
            {
                // 메인 메뉴로 복귀
                Program.MainMenuLoop();
            }
            else if (input == "1")
            {
                // 아이템 구매 씬
                SceneManager.ChangeScene(SceneType.Buy);
            }
            else if (input == "2")
            {
                // 아이템 판매 씬
                SceneManager.ChangeScene(SceneType.Sell);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 엔터를 누르세요...");
                Console.ReadLine();
                Render();
            }
        }
    }
}
