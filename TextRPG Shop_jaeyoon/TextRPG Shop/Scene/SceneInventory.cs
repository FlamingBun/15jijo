using System;

namespace TextRPG
{
    /// <summary>
    /// (1) 인벤토리 씬
    /// </summary>
    public class SceneInventory : BaseScene
    {
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("===== [인벤토리] =====");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            // 플레이어 인벤토리 목록 표시
            foreach (var item in Program.player.Inventory)
            {
                string equipMark = item.IsEquipped ? "[E]" : "";
                if (item.Type == ItemType.Armor)
                {
                    Console.WriteLine($"- {equipMark}{item.Name} | 방어+{item.Defense} | {item.Description}");
                }
                else
                {
                    Console.WriteLine($"- {equipMark}{item.Name} | 공격+{item.Attack} | {item.Description}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 씬 전환 -> 아이템 사용");
            Console.WriteLine("3. 씬 전환 -> 메인 상점");
            Console.WriteLine("0. 나가기(메인 메뉴로 가정)");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
        }

        public override void InputHandle()
        {
            string input = Console.ReadLine();

            if (input == "0")
            {
                // 메인 메뉴로 돌아간다고 가정(종료 혹은 다른 로직)
                Program.MainMenuLoop();
            }
            else if (input == "1")
            {
                ManageEquip();
                // 다시 자기 씬 화면을 띄우기 위해 Render() 재호출
                Render();
            }
            else if (input == "2")
            {
                // 아이템 사용 씬으로 전환
                SceneManager.ChangeScene(SceneType.ItemUse);
            }
            else if (input == "3")
            {
                // 상점 씬으로 전환
                SceneManager.ChangeScene(SceneType.Shop);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                Render();
            }
        }

        /// <summary>
        /// 기존 "장착 관리" 로직
        /// </summary>
        private void ManageEquip()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < Program.player.Inventory.Count; i++)
                {
                    Item it = Program.player.Inventory[i];
                    string eq = it.IsEquipped ? "[E]" : "";
                    if (it.Type == ItemType.Armor)
                        Console.WriteLine($"{i + 1}. {eq}{it.Name} | 방어+{it.Defense} | {it.Description}");
                    else
                        Console.WriteLine($"{i + 1}. {eq}{it.Name} | 공격+{it.Attack} | {it.Description}");
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.Write("장착/해제할 아이템 번호: ");
                string input = Console.ReadLine();

                if (input == "0") break;

                if (int.TryParse(input, out int idx))
                {
                    if (idx >= 1 && idx <= Program.player.Inventory.Count)
                    {
                        var selected = Program.player.Inventory[idx - 1];
                        if (selected.IsEquipped)
                        {
                            selected.IsEquipped = false;
                            Console.WriteLine($"{selected.Name} 장착 해제됨.");
                        }
                        else
                        {
                            selected.IsEquipped = true;
                            Console.WriteLine($"{selected.Name} 장착 완료!");
                        }
                    }
                }
                Console.WriteLine("계속하려면 엔터를 누르세요.");
                Console.ReadLine();
            }
        }
    }
}
