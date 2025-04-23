using System;
using System.Collections.Generic;

namespace TextRPG
{
    // ================================
    // 1) 기존: Item / ItemType
    // ================================
    public enum ItemType
    {
        Weapon,
        Armor
    }

    public class Item
    {
        public string Name;
        public ItemType Type;
        public int Attack;
        public int Defense;
        public string Description;
        public int Price;        // 상점에서 구매할 때 필요한 금액
        public bool IsPurchased; // 상점에서 구매 완료 여부
        public bool IsEquipped;  // 인벤토리에서 장착 여부

        public Item(string name, ItemType type, int attack, int defense,
                    string desc, int price = 0,
                    bool purchased = false, bool equipped = false)
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

    // ================================
    // 2) 기존: Player
    // ================================
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

    // ================================
    // 3) 기존: Shop
    // ================================
    public class Shop
    {
        // 상점에서 판매(또는 이미 구매 완료)하는 아이템 목록
        public List<Item> ShopItems = new List<Item>();

        public Shop()
        {
            // 예시상 "구매완료" 상태를 반영하여 아이템 초기화
            // 기획서 예시에 맞춰 일부 아이템은 이미 구매완료로 표시
            ShopItems.Add(new Item("수련자 갑옷", ItemType.Armor, 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000, purchased: false));
            ShopItems.Add(new Item("무쇠갑옷", ItemType.Armor, 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, purchased: true)); // 구매완료
            ShopItems.Add(new Item("스파르타의 갑옷", ItemType.Armor, 0, 15, "스파르타 전설의 갑옷입니다.", 3500, purchased: false));
            ShopItems.Add(new Item("낡은 검", ItemType.Weapon, 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600, purchased: false));
            ShopItems.Add(new Item("청동 도끼", ItemType.Weapon, 5, 0, "어디선가 사용됐던 도끼입니다.", 1500, purchased: false));
            ShopItems.Add(new Item("스파르타의 창", ItemType.Weapon, 7, 0, "스파르타의 전설적인 창입니다.", 0, purchased: true)); // 구매완료
        }
    }

    // ================================
    // ** (새로 추가) 씬(Scene) 관리
    // ================================
    public enum SceneType
    {
        None,
        Inventory,   // 인벤토리
        ItemUse,     // 아이템 사용
        Shop,        // 상점
        Buy,         // 아이템 구매
        Sell,        // 아이템 판매
    }

    /// <summary>
    /// 모든 Scene이 상속받을 추상 클래스
    /// </summary>
    public abstract class BaseScene
    {
        // Scene마다 UI를 갱신(출력)할 때 호출
        public abstract void Render();

        // Scene마다 사용자 입력을 처리할 때 호출
        public abstract void InputHandle();
    }

    /// <summary>
    /// Scene 전환을 관리
    /// </summary>
    public static class SceneManager
    {
        private static BaseScene currentScene = null;
        public static SceneType currentSceneType = SceneType.None;

        public static void ChangeScene(SceneType next)
        {
            // 씬 전환
            currentSceneType = next;

            // 현재 Scene 객체를 새 Scene 객체로 교체
            switch (next)
            {
                case SceneType.Inventory:
                    currentScene = new SceneInventory();
                    break;
                case SceneType.ItemUse:
                    currentScene = new SceneItemUse();
                    break;
                case SceneType.Shop:
                    currentScene = new SceneShop();
                    break;
                case SceneType.Buy:
                    currentScene = new SceneBuy();
                    break;
                case SceneType.Sell:
                    currentScene = new SceneSell();
                    break;
                default:
                    currentScene = null;
                    break;
            }

            if (currentScene != null)
            {
                currentScene.Render(); // 씬에 진입하자마자 한 번 출력
            }
        }

        /// <summary>
        /// 현재 씬의 InputHandle()을 호출
        /// </summary>
        public static void ProcessInput()
        {
            if (currentScene != null)
            {
                currentScene.InputHandle();
            }
        }
    }

    // ============================================
    // 4) 구체 Scene 클래스들 (5개)
    //    1) 인벤토리 씬 (SceneInventory)
    //    2) 아이템 사용 씬 (SceneItemUse)
    //    3) 상점 씬 (SceneShop)
    //    4) 아이템 구매 씬 (SceneBuy)
    //    5) 아이템 판매 씬 (SceneSell)
    // ============================================

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

            // 간단히: 아이템 목록 중에 무기/방어구 말고 "Consumable"같은게 없으니
            // 여기서는 그냥 "낡은 검 사용 -> 체력 10 회복" 같은 식으로 예시 처리
            // 실제론 소모성 아이템이 있어야 의미가 있을 것임.

            // 예시로 현재 인벤토리 모든 아이템을 보여주고, 선택 시 "사용" 효과를 넣어봄
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
                    // 인덱스 처리
                    if (idx >= 1 && idx <= Program.player.Inventory.Count)
                    {
                        // 간단 예시: 아이템 사용 -> 체력 조금 회복
                        var selItem = Program.player.Inventory[idx - 1];
                        Console.WriteLine($"'{selItem.Name}' 을(를) 사용한다고 가정 -> 체력 +10 회복!");
                        Program.player.Health += 10;
                        Console.WriteLine($"현재 체력: {Program.player.Health}");
                    }
                }
                // 다시 자기 씬
                Console.WriteLine("계속하려면 엔터키...");
                Console.ReadLine();
                Render();
            }
        }
    }

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
            // 상점 아이템 목록 (이미 Shop 클래스에 들어있음)
            // 여기서는 'Shop 메인'이라고 가정하므로, 간단한 메뉴만:
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
                // 아이템 구매 씬으로
                SceneManager.ChangeScene(SceneType.Buy);
            }
            else if (input == "2")
            {
                // 아이템 판매 씬으로
                SceneManager.ChangeScene(SceneType.Sell);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 계속하려면 엔터...");
                Console.ReadLine();
                Render();
            }
        }
    }

    /// <summary>
    /// (4) 아이템 구매 씬
    /// </summary>
    public class SceneBuy : BaseScene
    {
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("===== [아이템 구매] =====");
            Console.WriteLine($"보유 골드: {Program.player.Gold} G");
            Console.WriteLine();
            var shopItems = Program.shop.ShopItems;
            for (int i = 0; i < shopItems.Count; i++)
            {
                Item it = shopItems[i];
                string purchasedMark = it.IsPurchased ? "(구매완료)" : $"{it.Price} G";
                if (it.Type == ItemType.Armor)
                {
                    Console.WriteLine($"{i + 1}. {it.Name} | 방어+{it.Defense} | {it.Description} | {purchasedMark}");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {it.Name} | 공격+{it.Attack} | {it.Description} | {purchasedMark}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 상점 메인으로 돌아가기");
            Console.WriteLine();
            Console.Write("구매할 아이템 번호 입력: ");
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

            if (int.TryParse(input, out int idx))
            {
                var shopItems = Program.shop.ShopItems;
                if (idx >= 1 && idx <= shopItems.Count)
                {
                    Item target = shopItems[idx - 1];
                    if (target.IsPurchased)
                    {
                        Console.WriteLine("이미 구매 완료된 아이템입니다!");
                    }
                    else
                    {
                        if (Program.player.Gold >= target.Price)
                        {
                            // 구매 처리
                            Program.player.Gold -= target.Price;
                            target.IsPurchased = true;
                            // 인벤토리에 추가 (복사본 or 참조)
                            Program.player.Inventory.Add(new Item(
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
            }
            Console.WriteLine("계속하려면 엔터를 누르세요.");
            Console.ReadLine();
            Render(); // 다시 자기화면
        }
    }

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
                // 가정: 판매가는 구매가의 절반
                int sellPrice = it.Price / 2;
                // 만약 상점 구매가가 0(=이미 구매완료 된 free아이템)이면 팔 수 없다고 가정
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
                    // 가정: Price<=0 이면 못팜
                    if (selected.Price > 0)
                    {
                        int gain = selected.Price / 2;  // 판매가는 절반
                        Program.player.Gold += gain;
                        Console.WriteLine($"{selected.Name} 판매 완료! Gold +{gain}");
                        // 실제로 인벤토리에서 제거
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

    // ============================================
    // 5) Program (실제 Game 실행 루프 등)
    // ============================================
    public static class Program
    {
        public static Player player = new Player();
        public static Shop shop = new Shop();

        // 메인 함수
        public static void Main(string[] args)
        {
            // 예시로 초기 인벤토리에 몇 가지
            player.Inventory.Add(new Item("무쇠갑옷", ItemType.Armor, 0, 5, "무쇠로 만들어져 튼튼한 갑옷", 0, true, true));
            player.Inventory.Add(new Item("스파르타의 창", ItemType.Weapon, 7, 0, "스파르타 전설 창", 0, true, true));
            player.Inventory.Add(new Item("낡은 검", ItemType.Weapon, 2, 0, "쉽게 볼 수 있는 낡은 검", 0, true, false));

            // 시작할 때 메인 메뉴가 아닌, 일단 "인벤토리"씬으로 가정
            SceneManager.ChangeScene(SceneType.Inventory);

            // 매 프레임처럼 계속 입력을 받는 루프
            while (true)
            {
                // 현재 Scene의 InputHandle()을 호출
                SceneManager.ProcessInput();
            }
        }

        /// <summary>
        /// “메인 메뉴” 로직을 원한다면, 여기서 별도 구현 (예: 1. 상태보기, 2. 인벤토리, 3. 상점…)
        /// 이 예시에서는 “씬 전환”을 통해 처리하므로,
        /// 만약 0번 입력 시 MainMenuLoop를 호출하도록 사용할 수 있습니다.
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
                    // 종료
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
                    Console.WriteLine("잘못된 입력입니다. 엔터...");
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