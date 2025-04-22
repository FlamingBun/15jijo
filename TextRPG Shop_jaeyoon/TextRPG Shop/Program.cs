// See https://aka.ms/new-console-template for more information


namespace TextRPG
{
    // 아이템 종류를 구분할 열거형(공격/방어 구분용 - 필요 시 사용)
    public enum ItemType
    {
        Weapon,
        Armor
    }

    // 아이템 정보를 담는 클래스
    public class Item
    {
        public string Name;
        public ItemType Type;
        public int Attack;
        public int Defense;
        public string Description;
        public int Price;             // 상점에서 구매할 때 필요한 금액
        public bool IsPurchased;      // 상점에서 구매 완료 여부
        public bool IsEquipped;       // 인벤토리에서 장착 여부

        public Item(string name, ItemType type, int attack, int defense, string desc, int price = 0, bool purchased = false, bool equipped = false)
        {
            Name = name;
            Type = type;
            Attack = attack;
            Defense = defense;
            Description = desc;
            Price = price;
            IsPurchased = purchased;
            IsEquipped = equipped;
        }
    }

    // 플레이어(캐릭터) 정보
    public class Player
    {
        public string Name = "Chad";
        public string Job = "전사";
        public int Level = 1;

        // 기본 능력치
        public int BaseAttack = 10;
        public int BaseDefense = 10;
        public int Health = 100;

        // 보유 Gold
        public int Gold = 1500;

        // 플레이어가 소유한 아이템 목록(인벤토리)
        public List<Item> Inventory = new List<Item>();

        // 현재 착용 중인 아이템들의 능력치를 합산해서 반환
        public int GetTotalAttack()
        {
            int totalAttack = BaseAttack;
            foreach (var item in Inventory)
            {
                if (item.IsEquipped)
                {
                    totalAttack += item.Attack;
                }
            }
            return totalAttack;
        }

        public int GetTotalDefense()
        {
            int totalDefense = BaseDefense;
            foreach (var item in Inventory)
            {
                if (item.IsEquipped)
                {
                    totalDefense += item.Defense;
                }
            }
            return totalDefense;
        }
    }

    // 상점 기능 담당
    public class Shop
    {
        // 상점에서 판매(또는 이미 구매 완료)하는 아이템 목록
        public List<Item> ShopItems = new List<Item>();

        public Shop()
        {
            // 예시상 "구매완료" 상태를 반영하여 아이템 초기화
            // 기획서 예시에 맞춰 일부 아이템은 이미 구매완료로 표시
            ShopItems.Add(new Item("수련자 갑옷", ItemType.Armor, 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000, purchased: false));
            ShopItems.Add(new Item("무쇠갑옷", ItemType.Armor, 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, purchased: true));   // 구매완료
            ShopItems.Add(new Item("스파르타의 갑옷", ItemType.Armor, 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, purchased: false));
            ShopItems.Add(new Item("낡은 검", ItemType.Weapon, 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600, purchased: false));
            ShopItems.Add(new Item("청동 도끼", ItemType.Weapon, 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500, purchased: false));
            ShopItems.Add(new Item("스파르타의 창", ItemType.Weapon, 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 0, purchased: true)); // 구매완료
        }

        // 상점 메인 화면
        public void ShowShop(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                // 기획서 예시 형태대로 출력
                // - 이미 구매가 완료된 아이템이면 "구매완료" 표기
                // - 아직 구매 전이면 가격 노출
                // 순서대로 1)수련자갑옷 2)무쇠갑옷(구매완료) 3)스파르타의 갑옷 4)낡은 검 5)청동 도끼 6)스파르타의 창(구매완료)
                // 위 순서로 ShopItems에 이미 담았으므로, 그대로 인덱스 순회
                Console.WriteLine($"- {ShopItems[0].Name}    | 방어력 +{ShopItems[0].Defense}  | {ShopItems[0].Description}             |  {ShopItems[0].Price} G");
                Console.WriteLine($"- {ShopItems[1].Name}      | 방어력 +{ShopItems[1].Defense}  | {ShopItems[1].Description}           |  {(ShopItems[1].IsPurchased ? "구매완료" : $"{ShopItems[1].Price} G")}");
                Console.WriteLine($"- {ShopItems[2].Name} | 방어력 +{ShopItems[2].Defense} | {ShopItems[2].Description} |  {ShopItems[2].Price} G");
                Console.WriteLine($"- {ShopItems[3].Name}      | 공격력 +{ShopItems[3].Attack}  | {ShopItems[3].Description}            |  {ShopItems[3].Price} G");
                Console.WriteLine($"- {ShopItems[4].Name}     | 공격력 +{ShopItems[4].Attack}  | {ShopItems[4].Description}        |  {ShopItems[4].Price} G");
                Console.WriteLine($"- {ShopItems[5].Name}  | 공격력 +{ShopItems[5].Attack}  | {ShopItems[5].Description} |  {(ShopItems[5].IsPurchased ? "구매완료" : $"{ShopItems[5].Price} G")}");
                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    // 나가기
                    break;
                }
                else if (input == "1")
                {
                    BuyItems(player);
                }
                else
                {
                    // 잘못된 입력
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("엔터를 누르면 계속합니다.");
                    Console.ReadLine();
                }
            }
        }

        // 아이템 구매
        public void BuyItems(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine($"- 1 {ShopItems[0].Name}    | 방어력 +{ShopItems[0].Defense}  | {ShopItems[0].Description}             |  {ShopItems[0].Price} G");
                Console.WriteLine($"- 2 {ShopItems[1].Name}      | 방어력 +{ShopItems[1].Defense}  | {ShopItems[1].Description}           |  {(ShopItems[1].IsPurchased ? "구매완료" : $"{ShopItems[1].Price} G")}");
                Console.WriteLine($"- 3 {ShopItems[2].Name} | 방어력 +{ShopItems[2].Defense} | {ShopItems[2].Description} |  {ShopItems[2].Price} G");
                Console.WriteLine($"- 4 {ShopItems[3].Name}      | 공격력 +{ShopItems[3].Attack}  | {ShopItems[3].Description}            |  {ShopItems[3].Price} G");
                Console.WriteLine($"- 5 {ShopItems[4].Name}     | 공격력 +{ShopItems[4].Attack}  | {ShopItems[4].Description}        |  {ShopItems[4].Price} G");
                Console.WriteLine($"- 6 {ShopItems[5].Name}  | 공격력 +{ShopItems[5].Attack}  | {ShopItems[5].Description} |  {(ShopItems[5].IsPurchased ? "구매완료" : $"{ShopItems[5].Price} G")}");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    break; // 상점 메뉴로 돌아가기
                }

                int idx;
                bool isNumber = int.TryParse(input, out idx);

                if (!isNumber || idx < 1 || idx > 6)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                else
                {
                    // 인덱스 변환 (사용자 입력은 1~6, List 인덱스는 0~5)
                    idx -= 1;
                    Item targetItem = ShopItems[idx];

                    if (targetItem.IsPurchased)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else
                    {
                        // 구매 가능 여부 체크
                        if (player.Gold >= targetItem.Price)
                        {
                            // 구매 처리
                            player.Gold -= targetItem.Price;
                            targetItem.IsPurchased = true;

                            // 플레이어 인벤토리에 해당 아이템 추가 (복제 or 참조)
                            // 여기서는 단순 참조만 추가
                            player.Inventory.Add(new Item(
                                targetItem.Name,
                                targetItem.Type,
                                targetItem.Attack,
                                targetItem.Defense,
                                targetItem.Description,
                                targetItem.Price,
                                true,   // 구매됨
                                false  // 장착X
                            ));

                            Console.WriteLine("구매를 완료했습니다.");
                        }
                        else
                        {
                            Console.WriteLine("Gold 가 부족합니다.");
                        }
                    }
                }
                Console.WriteLine("엔터를 누르면 계속합니다.");
                Console.ReadLine();
            }
        }
    }

    class Program
    {
        static Player player = new Player();
        static Shop shop = new Shop();

        static void Main(string[] args)
        {
            // 예시상 플레이어가 이미 보유 중인 아이템들을 셋업
            // (무쇠갑옷 +5 방어 / 스파르타의 창 +7 공격 장착, 낡은 검 +2 공격 미장착)
            // 기획서 예시에 맞춰 초기화
            player.Inventory.Add(new Item("무쇠갑옷", ItemType.Armor, 0, 5, "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, true, true));
            player.Inventory.Add(new Item("스파르타의 창", ItemType.Weapon, 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 0, true, true));
            player.Inventory.Add(new Item("낡은 검", ItemType.Weapon, 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 0, true, false));

            // 메인 루프
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== 텍스트 RPG - 메인 메뉴 ====");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("0. 종료");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    // 종료
                    break;
                }
                else if (input == "1")
                {
                    ShowStatus();
                }
                else if (input == "2")
                {
                    ShowInventory();
                }
                else if (input == "3")
                {
                    shop.ShowShop(player);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("엔터를 누르면 계속합니다.");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("게임을 종료합니다. 엔터를 누르면 닫습니다.");
            Console.ReadLine();
        }

        // 1. 상태 보기
        static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {player.Level:00}");
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            // 기획서 예시: "공격력 : 17 (+7)" 처럼 표기
            int totalAtk = player.GetTotalAttack();
            int plusAtk = totalAtk - player.BaseAttack;
            int totalDef = player.GetTotalDefense();
            int plusDef = totalDef - player.BaseDefense;

            Console.WriteLine($"공격력 : {totalAtk} ( +{plusAtk} )");
            // 예시에는 "방어력 : 10 (+5)" 로 되어 있지만, 실제로는 10 + 5 = 15로 표시할 수 있음.
            // 여기서는 예시대로 '기본수치 ( +추가수치 )' 형식을 출력
            Console.WriteLine($"방어력 : {player.BaseDefense} ( +{plusDef} )");
            Console.WriteLine($"체 력 : {player.Health}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();
            // 0 입력 시 단순히 메뉴로 복귀
        }

        // 2. 인벤토리
        static void ShowInventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                // 아이템 목록 표시
                // 장착 중이면 [E], 아니면 그대로
                // 예시:
                // - [E]무쇠갑옷 | 방어력 +5 | 무쇠로 만들어져 튼튼한 갑옷입니다.
                // - [E]스파르타의 창 | 공격력 +7 | ...
                // - 낡은 검 | 공격력 +2 | ...
                foreach (var item in player.Inventory)
                {
                    string equipMark = item.IsEquipped ? "[E]" : "";
                    if (item.Type == ItemType.Armor)
                    {
                        Console.WriteLine($"- {equipMark}{item.Name}      | 방어력 +{item.Defense} | {item.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"- {equipMark}{item.Name}      | 공격력 +{item.Attack} | {item.Description}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                // 기획서 예시에 따르면, 인벤토리의 '나가기'는 2 또는 0 등 상황별로 다릅니다.
                // 아이템이 있을 때 예시에서는 "2. 나가기" 였음
                // 여기서는 기획서 예시를 우선 적용
                Console.WriteLine("2. 나가기");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "2")
                {
                    // 인벤토리 나가기 -> 메인 메뉴로
                    break;
                }
                else if (input == "1")
                {
                    ManageEquip();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("엔터를 누르면 계속합니다.");
                    Console.ReadLine();
                }
            }
        }

        // 2-1. 장착 관리
        static void ManageEquip()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                // 목록 앞에 숫자를 붙이고, 장착여부(E) 표시
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    Item item = player.Inventory[i];
                    string equipMark = item.IsEquipped ? "[E]" : "";
                    if (item.Type == ItemType.Armor)
                    {
                        Console.WriteLine($"- {i + 1} {equipMark}{item.Name}      | 방어력 +{item.Defense} | {item.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"- {i + 1} {equipMark}{item.Name}      | 공격력 +{item.Attack} | {item.Description}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    break; // 인벤토리 메뉴로 돌아감
                }

                int idx;
                bool isNumber = int.TryParse(input, out idx);

                if (!isNumber || idx < 1 || idx > player.Inventory.Count)
                {
                    Console.WriteLine("잘못된 입력입니다");
                }
                else
                {
                    // 선택한 아이템 장착/해제
                    Item selected = player.Inventory[idx - 1];
                    if (selected.IsEquipped)
                    {
                        // 이미 장착 중 -> 장착 해제
                        selected.IsEquipped = false;
                        Console.WriteLine($"{selected.Name} 장착을 해제했습니다.");
                    }
                    else
                    {
                        // 장착
                        selected.IsEquipped = true;
                        Console.WriteLine($"{selected.Name} 장착을 완료했습니다.");
                    }
                }
                Console.WriteLine("엔터를 누르면 계속합니다.");
                Console.ReadLine();
            }
        }
    }
}
