using System;

namespace TextRPG
{
    public static class Program
    {
        public static Player player = new Player();
        public static Shop shop = new Shop();

        // 메인 함수
        public static void Main(string[] args)
        {
            // 예시로 초기 인벤토리에 몇 가지 아이템 추가
            player.Inventory.Add(new Item("무쇠갑옷", ItemType.Armor, 0, 5, "무쇠로 만들어져 튼튼한 갑옷", 0, true, true));
            player.Inventory.Add(new Item("스파르타의 창", ItemType.Weapon, 7, 0, "스파르타 전설 창", 0, true, true));
            player.Inventory.Add(new Item("낡은 검", ItemType.Weapon, 2, 0, "쉽게 볼 수 있는 낡은 검", 0, true, false));

            // 시작할 때 메인 메뉴가 아닌, "인벤토리" 씬으로 가정
            SceneManager.ChangeScene(SceneType.Inventory);

            // 지속적으로 입력을 받는 루프
            while (true)
            {
                SceneManager.ProcessInput();
            }
        }

        /// <summary>
        /// “메인 메뉴” 로직이 필요한 경우 이 메서드 사용
        /// </summary>
        public static void MainMenuLoop()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== 텍스트 RPG - 메인 메뉴 ====");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리(씬 전환)");
                Console.WriteLine("3. 상점(씬 전환)");
                Console.WriteLine("0. 종료");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();
                if (input == "0")
                {
                    Environment.Exit(0);
                }
                else if (input == "1")
                {
                    ShowStatus();
                }
                else if (input == "2")
                {
                    SceneManager.ChangeScene(SceneType.Inventory);
                    return; // 메인 루프 빠져나감
                }
                else if (input == "3")
                {
                    SceneManager.ChangeScene(SceneType.Shop);
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 엔터 키를 누르세요...");
                    Console.ReadLine();
                }
            }
        }

        // 기존 “상태 보기” 로직
        public static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("상태 보기 (Character Info)");
            Console.WriteLine();
            Console.WriteLine($"Lv. {player.Level:00}");
            Console.WriteLine($"{player.Name} ({player.Job})");

            int totalAtk = player.GetTotalAttack();
            int plusAtk = totalAtk - player.BaseAttack;
            int totalDef = player.GetTotalDefense();
            int plusDef = totalDef - player.BaseDefense;

            Console.WriteLine($"공격력: {totalAtk} ( +{plusAtk} )");
            Console.WriteLine($"방어력: {player.BaseDefense} ( +{plusDef} )");
            Console.WriteLine($"체 력: {player.Health}");
            Console.WriteLine($"Gold : {player.Gold} G");

            Console.WriteLine();
            Console.WriteLine("아무 키나 누르면 메인 메뉴로 복귀...");
            Console.ReadLine();
        }
    }
}
