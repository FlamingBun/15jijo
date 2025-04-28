using _15jijo.ho;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DataManager
{
    public static DataManager instance;

    private bool isLoading = true;
    public bool isDataLoad = false;
    public bool isDataLoadFail = false;

    public Datas<Monster> monsterDatas;
    public Datas<Item> itemDatas;
    public Datas<KillMonsterQuest> monsterQuest;
    public Datas<PlayerStatQuest> statQuest;
    public DataManager()
    {
        Init();
    }

    public void Init()
    {
        if (instance == null)
        {
            instance = this;
        }

        monsterDatas = new Datas<Monster>();
        itemDatas = new Datas<Item>();
        monsterQuest = new Datas<KillMonsterQuest>();
        statQuest = new Datas<PlayerStatQuest>();
        isLoading = true;
        LoadAllData();
    }

    public bool CheckLoadData()
    {
        // 로딩이 끝날 때까지 대기
        int timeoutCounter = 0;
        const int MAX_TIMEOUT = 100; // 50초
        
        Console.WriteLine("데이터 로딩 중...");
        
        while (!isDataLoad && !isDataLoadFail)
        {
            // 50초 후에도 로드되지 않으면 타임아웃 처리
            timeoutCounter++;
            if (timeoutCounter > MAX_TIMEOUT) // 500ms * 100 = 50초
            {
                Console.WriteLine("데이터 로딩 시간이 초과되었습니다.");
                isDataLoadFail = true;
                break;
            }
            
            // 잠시 대기
            System.Threading.Thread.Sleep(500);
            
            // 현재 진행 상태를 표시
            if (timeoutCounter % 10 == 0) // 5초마다
            {
                Console.Write(".");
            }
        }

        Console.WriteLine(); // 새 줄 추가

        if (isDataLoad)
        {
            Console.Clear();
            Console.WriteLine("데이터 로딩이 완료되었습니다.");
            Console.WriteLine("Enter를 눌러 게임을 시작하세요!");
            Console.ReadLine(); // 사용자 입력 대기
            return true;
        }
        
        if (isDataLoadFail)
        {
            Console.WriteLine("데이터 로딩에 실패했습니다.");
            Console.WriteLine("게임을 다시 시작해주세요.");
            Console.ReadLine(); // 사용자 입력 대기
        }
        
        return false;
    }

    private async void LoadAllData()
    {
        var loadingTask = ShowConsoleLoading();

        // 기본 몬스터 데이터 생성
        var monsters = new Datas<Monster>();
        monsters.AddData(new Monster("슬라임", 10, 5, 2, 1));
        monsters.AddData(new Monster("고블린", 15, 8, 3, 2));
        monsters.AddData(new Monster("오크", 25, 10, 5, 3));
        monsterDatas = monsters;

        // 기본 장비 아이템 생성
        var equipItems = new Datas<EquipmentItem>();
        equipItems.AddData(new EquipmentItem("낡은 검", ItemType.Equipment, EquipmentItemType.Weapon, 2, "공격력이 2 증가합니다.", 300));
        equipItems.AddData(new EquipmentItem("낡은 갑옷", ItemType.Equipment, EquipmentItemType.Armor, 2, "방어력이 2 증가합니다.", 300));
        
        // 기본 소비 아이템 생성
        var consumeItems = new Datas<ConsumeItem>();
        consumeItems.AddData(new ConsumeItem("체력 물약", ItemType.Consumable, ConsumeItemType.HP, 10, "체력을 10 회복합니다.", 100));
        
        // 아이템 데이터에 추가
        foreach (var _item in equipItems.GetDatas())
        {
            itemDatas.AddData(_item);
        }

        foreach (var _item in consumeItems.GetDatas())
        {
            itemDatas.AddData(_item);
        }

        // 기본 퀘스트 생성
        var killMonsterQuests = new Datas<KillMonsterQuest>();
        // 아이템 리스트 생성
        var questRewardItems = new List<Item> { itemDatas.GetDatas()[0] };
        
        killMonsterQuests.AddData(new KillMonsterQuest(
            "슬라임 퇴치", 
            "슬라임 1마리 퇴치하기", 
            100, 
            50, 
            questRewardItems, 
            monsterDatas.GetDatas()[0], 
            1));
        
        var playerStatQuest = new Datas<PlayerStatQuest>();
        playerStatQuest.AddData(new PlayerStatQuest(
            "공격력 강화", 
            "공격력 5 이상 달성하기", 
            200, 
            100, 
            questRewardItems, 
            questRequireStatName.공격력, 
            5));
        
        monsterQuest = killMonsterQuests;
        statQuest = playerStatQuest;

        isLoading = false; // 위에 데이터를 전부 받아오면 그때 isLoading을 false로 변경 -> 애니메이션은 Task를 반환
        await loadingTask; // 애니메이션 Task가 완전히 끝날 때까지 대기

        Console.Clear();
        Console.WriteLine("데이터 로딩 완료!");
        Console.WriteLine("계속하려면 Enter를 누르세요...");

        isDataLoad = true;
    }

    private async Task<Datas<T>> LoadWithConsoleLoading<T>(string sheetName, string range, Func<JToken, T> parseFunc)
    {

        var dataTask = GetDataFromGoogleSheet<T>(sheetName, range, parseFunc);

        var data = await dataTask;

        return data;
    }


    private async Task ShowConsoleLoading()
    {
        int dotCount = 0;

        while (isLoading)
        {
            Console.Clear();
            Console.Write("데이터 로딩중");
            for (int i = 0; i < dotCount; i++)
            {
                Console.Write(".");
            }

            if (++dotCount > 3)
            {
                dotCount = 0;
            }

            await Task.Delay(500);
        }
    }


    // 비동기로 작업할 때 async함수가 void가 아니면, Task<T>를 반환한다.
    // values[i]의 타입이 JToken이고 JToken은 JSON의 모든 요소를 포괄하는 타입
    private async Task<Datas<T>> GetDataFromGoogleSheet<T>(string sheetName, string range, Func<JToken, T> parseFunc)
    {
        string spreadsheetId;
        string apiKey;
        // AppDomain.CurrentDomain.BaseDirectory는 .exe파일의 위치를 가져온다.
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

        // using 블록을 사용하면 파일을 읽고 난 뒤 자동으로 Stream을 닫는다.
        using (StreamReader file = File.OpenText(path))
        {
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject _json = (JObject)JToken.ReadFrom(reader);


                spreadsheetId = _json["SpreadsheetId"].ToString();
                apiKey = _json["ApiKey"].ToString();
            }
        }

        string url = $"https://sheets.googleapis.com/v4/spreadsheets/{spreadsheetId}/values/{sheetName}!{range}?key={apiKey}";

        using HttpClient client = new HttpClient();
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("에러 발생: " + response.StatusCode);
            isDataLoadFail = true;
            return new Datas<T>();
        }

        string json = await response.Content.ReadAsStringAsync();
        var data = JObject.Parse(json);
        var values = data["values"];
        var result = new Datas<T>();

        for (int i = 1; i < values.Count(); i++) // 첫 줄은 헤더
        {
            var row = values[i];
            result.AddData(parseFunc(row));
        }

        return result;
    }


    public bool LoadPlayerData()
    {
        var playerToken = LoadJsonFromFile("playerInfo.txt");
        if (playerToken == null)
        {
            return false;
        }

        Player player = new Player((JObject)playerToken);
        GameManager.instance.player = player;

        var inventoryToken = LoadJsonFromFile("inventoryInfo.txt");

        // C#의 패턴 매칭으로
        // inventoryToken이 JArray 타입인지 검사 -> 타입이 맞으면 inventoryArray로 캐스팅해 준다.
        if (inventoryToken is JArray inventoryArray)
        {
            List<Item> items = new List<Item>();

            foreach (var item in inventoryArray)
            {
                if (item["EquipmentItemType"] != null)
                {
                    items.Add(item.ToObject<EquipmentItem>());
                }
                else if (item["ConsumeItemType"] != null)
                {
                    items.Add(item.ToObject<ConsumeItem>());
                }
            }
            GameManager.instance.havingItems = items;
            GameManager.instance.purchasedItems = GameManager.instance.havingItems.Where(item => item.ItemType == ItemType.Equipment).ToList();
            if (GameManager.instance.player.equippedAttackPowerItem != null &&
                GameManager.instance.havingItems.Any(item => item.ItemName == GameManager.instance.player.equippedAttackPowerItem.ItemName))
            {
                GameManager.instance.player.SetEquippedAttackPowerItem(
                    (EquipmentItem)GameManager.instance.havingItems.First(item => item.ItemName == GameManager.instance.player.equippedAttackPowerItem.ItemName)
                );
            }
            if (GameManager.instance.player.equippedDefensivePowerItem != null &&
                GameManager.instance.havingItems.Any(item => item.ItemName == GameManager.instance.player.equippedDefensivePowerItem.ItemName))
            {
                GameManager.instance.player.SetEquippedDefensivePowerItem(
                    (EquipmentItem)GameManager.instance.havingItems.First(item => item.ItemName == GameManager.instance.player.equippedDefensivePowerItem.ItemName)
                );
            }
        }

        var purchasedInventoryToken = LoadJsonFromFile("purchasedInventoryInfo.txt");

        //if (purchasedInventoryToken is JArray purchasedInventoryArray)
        //{
        //    List<Item> items = new List<Item>();

        //    foreach (var item in purchasedInventoryArray)
        //    {
        //        if (item["EquipmentItemType"] != null)
        //        {
        //            items.Add(item.ToObject<EquipmentItem>());
        //        }
        //        else if (item["ConsumeItemType"] != null)
        //        {
        //            items.Add(item.ToObject<ConsumeItem>());
        //        }
        //    }
        //    GameManager.instance.purchasedItems = items;
        //}
        return true;
    }

    private JToken? LoadJsonFromFile(string fileName)
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        if (!File.Exists(path))
            return null;

        using (StreamReader file = File.OpenText(path))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            return JToken.ReadFrom(reader);
        }
    }

    public bool SavePlayerData()
    {
        bool isSaved = false;
        try
        {
            // player 정보  저장
            if (SaveJsonToFile("playerInfo.txt", GameManager.instance.player))
            {
                isSaved = true;
            }

            // 갖고있는 아이템 목록 저장
            if (GameManager.instance.havingItems.Count >= 0)
            {
                if (SaveJsonToFile("inventoryInfo.txt", GameManager.instance.havingItems))
                {
                    isSaved = true;
                }
            }

            // 구매한 아이템 목록 저장
            if (GameManager.instance.purchasedItems.Count >= 0)
            {
                if (SaveJsonToFile("purchasedInventoryInfo.txt", GameManager.instance.purchasedItems))
                {
                    isSaved = true;
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"파일 처리 오류: {ex.Message}");
        }
        return isSaved;
    }

    private bool SaveJsonToFile(string fileName, object data)
    {
        try
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(json);
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"파일 저장 실패: {ex.Message}");
            return false;
        }

    }
}