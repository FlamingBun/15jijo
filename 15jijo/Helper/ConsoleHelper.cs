using System.Buffers.Text;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;

public static class ConsoleHelper
{
    public static void ShowScene(SceneState _sceneState)
    {
        int clearedLevel = GameManager.instance.dungeonController.clearedLevel;
        switch (_sceneState)
        {
            case SceneState.MakeCharacter:
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n\n");
                Console.WriteLine("원하시는 이름을 설정해주세요.");
                Console.Write(">>");
                break;
            case SceneState.Main:
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");
                Console.WriteLine("1. 상태보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 휴식하기");
                Console.WriteLine("5. 퀘스트");
                Console.WriteLine($"6. ZEP 입장(현재 진행: {clearedLevel}조)");
                Console.WriteLine("7. 게임 저장하기");
                Console.WriteLine("8. 게임 종료하기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.\n");
                Console.Write(">>");
                break;
            case SceneState.Relax:
                if (GameManager.instance != null)
                {
                    Player? player = GameManager.instance.player;
                    if (player != null)
                    {
                        Console.WriteLine("휴식하기\n");
                        Console.WriteLine($"500 Gold 를 내면 체력을 회복할 수 있습니다.(보유 Gold : {player.Gold})\n");
                        Console.WriteLine("1. 휴식하기");
                        Console.WriteLine("0. 나가기\n");
                        Console.WriteLine("원하시는 행동을 입력해주세요.\n");
                        Console.Write(">>");
                    }
                }
                break;
            case SceneState.SelectJob:
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n");
                Console.WriteLine("원하시는 직업을 선택해주세요.\n");
                Console.WriteLine("1. 나영웅 매니저님");
                Console.WriteLine("2. 박찬우 매니저님");
                Console.WriteLine("3. 한효승 매니저님\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.\n");
                Console.Write(">>");
                break;
            case SceneState.DungeonEntrance:
                Console.WriteLine("ZEP에 입장하셨습니다.");
                Console.WriteLine($"현재까지 교육한 조: {clearedLevel}조");
                for (int i = 1; i <= 10; i++)
                {
                    Console.OutputEncoding = Encoding.UTF8;
                    if (i <= clearedLevel + 1)
                    {
                        Console.WriteLine($"▶ {i}조");
                    }
                    else
                    {
                        Console.WriteLine($"ꗃ {i}조");
                    }
                }
                break;
            case SceneState.Battle:
                //TextReaderingHint = TextReaderingHint.AntiAlias;
                int selectedLevel = GameManager.instance.dungeonController.selectedLevel;
                
                Console.SetCursorPosition(0, 7);
                Console.WriteLine("     ██████╗  █████╗ ████████╗████████╗██╗     ███████╗    ███████╗████████╗ █████╗ ██████╗ ████████╗██╗██╗██╗██╗██╗  ");
                Console.WriteLine("     ██╔══██╗██╔══██╗╚══██╔══╝╚══██╔══╝██║     ██╔════╝    ██╔════╝╚══██╔══╝██╔══██╗██╔══██╗╚══██╔══╝██║██║██║██║██║  ");
                Console.WriteLine("     ██████╔╝███████║   ██║      ██║   ██║     █████╗      ███████╗   ██║   ███████║██████╔╝   ██║   ██║██║██║██║██║  ");
                Console.WriteLine("     ██╔══██╗██╔══██║   ██║      ██║   ██║     ██╔══╝      ╚════██║   ██║   ██╔══██║██╔══██╗   ██║   ╚═╝╚═╝╚═╝╚═╝╚═╝  ");
                Console.WriteLine("     ██████╔╝██║  ██║   ██║      ██║   ███████╗███████╗    ███████║   ██║   ██║  ██║██║  ██║   ██║   ██╗██╗██╗██╗██╗  ");
                Console.WriteLine("     ╚═════╝ ╚═╝  ╚═╝   ╚═╝      ╚═╝   ╚══════╝╚══════╝    ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝╚═╝╚═╝╚═╝╚═╝  ");
                Console.SetCursorPosition(0, 18);
                Console.WriteLine("========================================================================================================================");
                Console.WriteLine($"BATTLE START!!!!                     ");
                Console.WriteLine($"ZEP {selectedLevel}조에 진입했습니다. ");
                System.Threading.Thread.Sleep(1500);
                break;
            case SceneState.Status:
                if (GameManager.instance != null)
                {
                    Player? player = GameManager.instance.player;
                    if (player != null)
                    {
                        int level = player.Level;
                        string? name = player.Name;
                        Jobs job = player.Job;
                        float totalAttackPower = player.TotalAttackPower;
                        float additionalAttackPower = player.AdditionalAttackPower;
                        float totalDefensivePower = player.TotalDefensivePower;
                        float additionalDefensivePower = player.AdditionalDefensivePower;
                        float currentHp = player.CurrentHp;
                        float totalHp = player.TotalHp;
                        float additionalHp = player.AdditionalHp;
                        float totalMp = player.TotalMp;
                        float additionalMp = player.AdditionalMp;
                        int gold = player.Gold;
                        Console.WriteLine("상태보기");
                        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n\n");
                        Console.WriteLine($"Lv. {level:D2}");
                        Console.WriteLine($"{name} ({job})");
                        switch(job)
                        {
                            case Jobs.나영웅매니저님:
                                Console.WriteLine($"공격력 : {totalAttackPower}{(additionalAttackPower > 0 ? $" (+{additionalAttackPower})" : "")}");
                                break;
                            case Jobs.박찬우매니저님:
                                Console.WriteLine($"민첩성 : {totalAttackPower}{(additionalAttackPower > 0 ? $" (+{additionalAttackPower})" : "")}");
                                break;
                            case Jobs.한효승매니저님:
                                Console.WriteLine($"주문력 : {totalAttackPower}{(additionalAttackPower > 0 ? $" (+{additionalAttackPower})" : "")}");
                                break;
                            default:
                                Console.WriteLine("오류입니다.");
                                break;
                        }
                        Console.WriteLine($"방어력 : {totalDefensivePower}{(additionalDefensivePower > 0 ? $" (+{additionalDefensivePower})" : "")}");
                        Console.WriteLine($"체력 : {currentHp} / {totalHp}{(additionalHp > 0 ? $" (+{additionalHp})" : "")}");
                        Console.WriteLine($"Gold : {gold} G\n");
                        Console.WriteLine("0. 나가기");
                        Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                    }
                }
                break;
            case SceneState.Shop:
                if (GameManager.instance != null &&
                    GameManager.instance.player != null &&
                    DataManager.instance.itemDatas != null &&
                    GameManager.instance.purchasedItems != null)
                {
                    List<Item> items = DataManager.instance.itemDatas.GetDatas();
                    Player? player = GameManager.instance.player;
                    List<Item>? purchasedItems = GameManager.instance.purchasedItems;
                    List<Item>? equipmentItems = items.Where(item => item.ItemType == ItemType.Equipment).ToList();
                    List<Item>? consumableItems = items.Where(item => item.ItemType == ItemType.Consumable).ToList();
                    Console.WriteLine("상점");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine(player.Gold + " G");
                    Console.WriteLine("\n[장비 아이템 목록]");
                    for (int i = 0; i < equipmentItems.Count(); ++i)
                    {
                        Item item = equipmentItems[i];
                        string abillityType = GetItemTypeString(item);
                        string priceDisplay = purchasedItems.Any(p => p.ItemName == item.ItemName) ? "구매완료" : $"{item.ItemPrice} G";
                        Console.WriteLine($"- {item.ItemName} | {abillityType} +{item.ItemAbility} | {item.ItemDescription} | {priceDisplay}");
                    }
                    Console.WriteLine("\n[소비 아이템 목록]");
                    for (int i = 0; i < consumableItems.Count(); ++i)
                    {
                        Item item = consumableItems[i];
                        string abillityType = GetItemTypeString(item);
                        Console.WriteLine($"- {item.ItemName} | {abillityType} +{item.ItemAbility} | {item.ItemDescription} | {item.ItemPrice} G");
                    }
                    Console.WriteLine("\n1. 아이템 구매");
                    Console.WriteLine("2. 아이템 판매\n");
                    Console.WriteLine("0. 나가기");
                    Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                }
                break;
            case SceneState.Buying:
                if (GameManager.instance != null &&
                    GameManager.instance.player != null &&
                    DataManager.instance.itemDatas != null &&
                    GameManager.instance.purchasedItems != null)
                {
                    Player? player = GameManager.instance.player;
                    List<Item> items = DataManager.instance.itemDatas.GetDatas();
                    List<Item>? purchasedItems = GameManager.instance.purchasedItems;
                    List<Item>? equipmentItems = items.Where(item => item.ItemType == ItemType.Equipment).ToList();
                    List<Item>? consumableItems = items.Where(item => item.ItemType == ItemType.Consumable).ToList();
                    Console.WriteLine("상점 - 아이템 구매");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine(player.Gold + " G");
                    Console.WriteLine("\n[장비 아이템 목록]");
                    for (int i = 0; i < equipmentItems.Count(); ++i)
                    {
                        Item item = equipmentItems[i];
                        string abillityType = GetItemTypeString(item);
                        string priceDisplay = purchasedItems.Any(p => p.ItemName == item.ItemName) ? "구매완료" : $"{item.ItemPrice} G";
                        Console.WriteLine($"- {i + 1} {item.ItemName} | {abillityType} +{item.ItemAbility} | {item.ItemDescription} | {priceDisplay}");
                    }
                    Console.WriteLine("\n[소비 아이템 목록]");
                    for (int i = 0; i < consumableItems.Count(); ++i)
                    {
                        Item item = consumableItems[i];
                        string abillityType = GetItemTypeString(item);
                        Console.WriteLine($"- {i + 1 + equipmentItems.Count()} {item.ItemName} | {abillityType} +{item.ItemAbility} | {item.ItemDescription} | {item.ItemPrice} G");
                    }
                    Console.WriteLine("\n0. 나가기");
                    Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                }
                break;
            case SceneState.Selling:
                if (GameManager.instance != null &&
                    GameManager.instance.player != null &&
                    GameManager.instance.havingItems != null)
                {
                    Player? player = GameManager.instance.player;
                    List<Item>? items = GameManager.instance.havingItems;
                    List<Item>? equipmentItems = items.Where(item => item.ItemType == ItemType.Equipment).ToList();
                    List<Item>? consumableItems = items.Where(item => item.ItemType == ItemType.Consumable).ToList();
                    Console.WriteLine("상점 - 아이템 판매");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine(player.Gold + " G");
                    Console.WriteLine("\n[장비 아이템 목록]");
                    if (!equipmentItems.Any())
                    {
                        Console.WriteLine("판매 가능한 장비 아이템이 없습니다.");
                    }
                    else
                    {
                        for (int i = 0; i < equipmentItems.Count; ++i)
                        {
                            Item item = equipmentItems[i];
                            string abillityType = GetItemTypeString(item);
                            Console.WriteLine($"- {i + 1} {item.ItemName} | {abillityType} +{item.ItemAbility} | {item.ItemDescription} | {(int)(item.ItemPrice * 0.85f)}");
                        }
                    }
                    Console.WriteLine("\n[소비 아이템 목록]");
                    if (!consumableItems.Any())
                    {
                        Console.WriteLine("판매 가능한 소비 아이템이 없습니다.");
                    }
                    else
                    {
                        for (int i = 0; i < consumableItems.Count; ++i)
                        {
                            ConsumeItem item = (ConsumeItem)consumableItems[i];
                            string abillityType = GetItemTypeString(item);
                            Console.WriteLine($"- {i + 1 + equipmentItems.Count()} {item.ItemName} X{item.ItemCount} | {abillityType} +{item.ItemAbility} | {item.ItemDescription} | {(int)(item.ItemPrice * 0.85f)}");
                        }
                    }
                    Console.WriteLine("\n0. 나가기\n");
                    Console.Write("원하시는 행동을 입력해주세요.\n>>");
                }
                break;
            case SceneState.Inventory:
                if (GameManager.instance != null &&
                    GameManager.instance.player != null &&
                    GameManager.instance.havingItems != null)
                {
                    List<Item>? items = GameManager.instance.havingItems;
                    List<Item>? equipmentItems = items.Where(item => item.ItemType == ItemType.Equipment).ToList();
                    List<Item>? consumableItems = items.Where(item => item.ItemType == ItemType.Consumable).ToList();
                    Console.WriteLine("인벤토리");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                    Console.WriteLine("[장비 아이템 목록]");
                    if (!equipmentItems.Any())
                    {
                        Console.WriteLine("장비 아이템이 없습니다.");
                    }
                    else
                    {
                        for (int i = 0; i < equipmentItems.Count; ++i)
                        {
                            Item item = equipmentItems[i];
                            string abillityType = GetItemTypeString(item);
                            string? equippedDisplay = (GameManager.instance.player.equippedAttackPowerItem != null && GameManager.instance.player.equippedAttackPowerItem.ItemName == item.ItemName) ||
                                                      (GameManager.instance.player.equippedDefensivePowerItem != null && GameManager.instance.player.equippedDefensivePowerItem.ItemName == item.ItemName) ? "[E]" : null;
                            Console.WriteLine($"- {equippedDisplay}{item.ItemName} | {abillityType} +{item.ItemAbility} | {item.ItemDescription}");
                        }
                    }
                    Console.WriteLine("\n[소비 아이템 목록]");
                    if (!consumableItems.Any())
                    {
                        Console.WriteLine("소비 아이템이 없습니다.");
                    }
                    else
                    {
                        for (int i = 0; i < consumableItems.Count; ++i)
                        {
                            ConsumeItem item = (ConsumeItem)consumableItems[i];
                            string abillityType = GetItemTypeString(item);
                            Console.WriteLine($"- {item.ItemName} X{item.ItemCount} | {abillityType} +{item.ItemAbility} | {item.ItemDescription}");
                        }
                    }
                    Console.WriteLine("\n1. 장착관리");
                    Console.WriteLine("2. 소비 아이템 사용\n");
                    Console.WriteLine("0. 나가기");
                    Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                }
                break;
            case SceneState.Fitting:
                if (GameManager.instance != null &&
                    GameManager.instance.player != null &&
                    GameManager.instance.havingItems != null)
                {
                    List<Item>? items = GameManager.instance.havingItems;
                    List<Item>? equipmentItems = items.Where(item => item.ItemType == ItemType.Equipment).ToList();
                    Console.WriteLine("인벤토리 - 장착관리");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                    Console.WriteLine("[장비 아이템 목록]");
                    if (!equipmentItems.Any())
                    {
                        Console.WriteLine("장착 가능한 장비가 없습니다.");
                    }
                    else
                    {
                        for (int i = 0; i < equipmentItems.Count; ++i)
                        {
                            Item item = equipmentItems[i];
                            string abillityType = GetItemTypeString(item);
                            string? equippedDisplay = (GameManager.instance.player.equippedAttackPowerItem != null && GameManager.instance.player.equippedAttackPowerItem.ItemName == item.ItemName) ||
                                                      (GameManager.instance.player.equippedDefensivePowerItem != null && GameManager.instance.player.equippedDefensivePowerItem.ItemName == item.ItemName) ? "[E]" : null;
                            Console.WriteLine($"- {i + 1} {equippedDisplay}{item.ItemName} | {abillityType} +{item.ItemAbility} | {item.ItemDescription}");
                        }
                    }
                    Console.WriteLine("\n0. 나가기");
                    Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                }
                break;
            case SceneState.Eating:
                if (GameManager.instance != null &&
                    GameManager.instance.havingItems != null)
                {
                    List<Item>? items = GameManager.instance.havingItems;
                    List<Item>? consumableItems = items.Where(item => item.ItemType == ItemType.Consumable).ToList();
                    Console.WriteLine("인벤토리 - 소비 아이템 사용");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                    Console.WriteLine("[소비 아이템 목록]");
                    if (!consumableItems.Any())
                    {
                        Console.WriteLine("사용 가능한 아이템이 없습니다.");
                    }
                    else
                    {
                        for (int i = 0; i < consumableItems.Count; ++i)
                        {
                            ConsumeItem item = (ConsumeItem)consumableItems[i];
                            string abillityType = GetItemTypeString(item);
                            Console.WriteLine($"- {i + 1} {item.ItemName} X{item.ItemCount} | {abillityType} +{item.ItemAbility} | {item.ItemDescription}");
                        }
                    }
                    Console.WriteLine("\n0. 나가기");
                    Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                }
                break;
            case SceneState.Quest:
                Console.WriteLine("Quest!!\n");
                GameManager.instance.questController.DisplayQuestListName();
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                break;
    
        }
    }

    public static void ShowText()
    {

    }

    public static bool CheckUserInput(string? _input, int _selectionCount, ref int _inputNumber)
    {
        if (!int.TryParse(_input, out int inputNumber) || inputNumber.ToString() != _input)
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1500);
            return false;
        }

        // 입력한 번호가 선택지 개수보다 크거나 0보다 작으면 false
        if (_selectionCount < inputNumber || inputNumber < 0)
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1500);
            return false;
        }

        _inputNumber = inputNumber;
        return true;
    }

    public static bool CheckUserInputNoZero(string? _input, int _selectionCount, ref int _inputNumber)
    {
        if (!int.TryParse(_input, out int inputNumber) || inputNumber.ToString() != _input)
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1500);
            return false;
        }

        // 입력한 번호가 선택지 개수보다 크거나 0보다 작으면 false
        if (_selectionCount < inputNumber || inputNumber <= 0)
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1500);
            return false;
        }

        _inputNumber = inputNumber;
        return true;
    }

    public static bool CheckUserInput(string? _input, int _selectionCount, ref int _inputNumber, bool isFight)
    {
        if (!int.TryParse(_input, out int inputNumber) || inputNumber.ToString() != _input)
        {

            Thread.Sleep(1500);
            return false;
        }

        // 입력한 번호가 선택지 개수보다 크거나 0보다 작으면 false
        if (_selectionCount < inputNumber ||  inputNumber < 0)
        {

            Thread.Sleep(1500);
            return false;
        }

        _inputNumber = inputNumber;
        return true;
    }

    public static string GetItemTypeString(Item item)
    {
        string abillityType = "";
        switch (item.ItemType)
        {
            case ItemType.Equipment:
                EquipmentItem equipmentItem = (EquipmentItem)item;
                switch (equipmentItem.EquipmentItemType)
                {
                    case EquipmentItemType.Weapon:
                        abillityType = "공격력";
                        break;
                    case EquipmentItemType.Armor:
                        abillityType = "방어력";
                        break;
                }
                break;
            case ItemType.Consumable:
                ConsumeItem consumeItem = (ConsumeItem)item;
                switch (consumeItem.ConsumeItemType)
                {
                    case ConsumeItemType.HP:
                        abillityType = "HP";
                        break;
                    case ConsumeItemType.MP:
                        abillityType = "MP";
                        break;
                }
                break;
        }
        return abillityType;
    }

}

