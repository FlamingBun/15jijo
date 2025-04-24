using System;

namespace TextRPG
{
    /// <summary>
    /// (5) 아이템 판매 씬 (예시)
    /// </summary>
    public class SceneSell : BaseScene
    {
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("===== [아이템 판매] =====");
            Console.WriteLine($"보유 골드: {Program.player.Gold} G");
            Console.WriteLine("현재 인벤토리 중 판매 가능한 아이템을 골라보세요.");
            Console.WriteLine("(이미 상점에서 구매하지 않은 아이템은 0원 처리거나, 가정에 따라 안팔리게 할 수도 있음)");
            Console.WriteLine();

            // 인벤토리에서 "상점에서 산 것"만 팔 수 있다고 가정
            var inventory = Program.player.Inventory;
            for (int i = 0; i < inventory.Count; i++)
            {
                var it = inventory[i];
                // 판매가는 구매가의 절반이라고 가정
                int sellPrice = it.Price / 2;

                // 만약 구매가(Price)가 0이면 판매 불가라고 가정
                if (it.Price <= 0)
                {
                    Console.WriteLine($"{i + 1}. {it.Name} [판매 불가: 0G짜리 or 기본 장비]");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {it.Name} [판매가: {sellPrice}G] (구매가 {it.Price}G)");
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 상점 메인으로 돌아가기");
            Console.WriteLine();
            Console.Write("판매할 아이템 번호: ");
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

            var inventory = Program.player.Inventory;
            if (int.TryParse(input, out int idx))
            {
                if (idx >= 1 && idx <= inventory.Count)
                {
                    var selected = inventory[idx - 1];
                    // 가격이 0 이하이면 판매 불가
                    if (selected.Price > 0)
                    {
                        int gain = selected.Price / 2;  // 판매가는 절반
                        Program.player.Gold += gain;
                        Console.WriteLine($"{selected.Name} 판매 완료! Gold +{gain}");
                        // 인벤토리에서 제거
                        inventory.Remove(selected);
                    }
                    else
                    {
                        Console.WriteLine($"'{selected.Name}'은(는) 판매 불가(또는 0G) 아이템입니다!");
                    }
                }
            }
            Console.WriteLine("계속하려면 엔터 키를 누르세요.");
            Console.ReadLine();
            Render();
        }
    }
}
