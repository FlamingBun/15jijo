using System;

namespace TextRPG
{
    /// <summary>
    /// (2) 아이템 사용 씬
    /// </summary>
    public class SceneItemUse : BaseScene
    {
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("===== [아이템 사용] =====");
            Console.WriteLine("사용할 아이템을 선택합니다 (체력회복 등 가정).");
            Console.WriteLine();

            // 간단 예시: 소비 아이템이 없으므로 무기/방어구라도 임시로 표시
            for (int i = 0; i < Program.player.Inventory.Count; i++)
            {
                Item it = Program.player.Inventory[i];
                Console.WriteLine($"{i + 1}. {it.Name} ({it.Description})");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기(인벤토리 씬 등)");
            Console.WriteLine();
            Console.Write("아이템 번호 선택 -> 사용 예시: ");
        }

        public override void InputHandle()
        {
            string input = Console.ReadLine();

            if (input == "0")
            {
                // 인벤토리로 돌아간다고 가정
                SceneManager.ChangeScene(SceneType.Inventory);
            }
            else
            {
                if (int.TryParse(input, out int idx))
                {
                    if (idx >= 1 && idx <= Program.player.Inventory.Count)
                    {
                        // 간단 예시: 아이템 사용 -> 체력 +10 회복
                        var selItem = Program.player.Inventory[idx - 1];
                        Console.WriteLine($"'{selItem.Name}' 을(를) 사용 -> 체력 10 회복!");
                        Program.player.Health += 10;
                        Console.WriteLine($"현재 체력: {Program.player.Health}");
                    }
                }
                Console.WriteLine("계속하려면 엔터키를 누르세요...");
                Console.ReadLine();
                Render();
            }
        }
    }
}
