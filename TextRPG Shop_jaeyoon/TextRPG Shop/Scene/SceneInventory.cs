using System;

namespace _15jijo
{
    public class SceneInventory : BaseScene
    {
        public override SceneState SceneState { get; protected set; } = SceneState.Inventory;

        public override SceneState InputHandle()
        {
            // 팀 공통 메서드: DrawScene(어떤 SceneState인지)
            DrawScene(SceneState);

            Console.WriteLine("===== [인벤토리] =====");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            // 플레이어 인벤토리 목록 표시
            foreach (var item in GameManager.player.Inventory)
            {
                string equipMark = item.IsEquipped ? "[E]" : "";
                if (item.Type == ItemType.Armor)
                    Console.WriteLine($"- {equipMark}{item.Name} | 방어+{item.Defense} | {item.Description}");
                else
                    Console.WriteLine($"- {equipMark}{item.Name} | 공격+{item.Attack} | {item.Description}");
            }

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("2. 아이템 사용으로 이동");
            Console.WriteLine("3. 상점으로 이동");
            Console.WriteLine("0. 메인으로 돌아가기\n");

            selectionCount = 3; // 0~3 입력 가능
            Console.Write("원하시는 행동을 입력해주세요 >> ");
            string input = Console.ReadLine();

            int num = -1;
            bool valid = ConsoleHelper.CheckUserInput(input, selectionCount, ref num);
            if (!valid) // 잘못된 입력이면 현재 씬 유지
                return SceneState.Inventory;

            switch(num)
            {
                case 0:
                    return SceneState.Main;
                case 1:
                    ManageEquip();
                    return SceneState.Inventory;  // 계속 인벤토리에 머무름
                case 2:
                    return SceneState.ItemUse;    // 아이템 사용 씬으로
                case 3:
                    return SceneState.Shop;       // 상점 씬으로
            }
            return SceneState.Inventory; // 기본값
        }

        private void ManageEquip()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리\n[아이템 목록]");
                var inventory = GameManager.player.Inventory;
                for(int i=0; i<inventory.Count; i++)
                {
                    Item it = inventory[i];
                    string eq = it.IsEquipped ? "[E]" : "";
                    if (it.Type == ItemType.Armor)
                        Console.WriteLine($"{i+1}. {eq}{it.Name} | 방어+{it.Defense} | {it.Description}");
                    else
                        Console.WriteLine($"{i+1}. {eq}{it.Name} | 공격+{it.Attack} | {it.Description}");
                }
                Console.WriteLine("\n0. 나가기\n");

                Console.Write("장착/해제할 아이템 번호: ");
                string input = Console.ReadLine();
                if(input == "0") break;

                if (int.TryParse(input, out int idx))
                {
                    if (idx >=1 && idx <= inventory.Count)
                    {
                        var selected = inventory[idx-1];
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
