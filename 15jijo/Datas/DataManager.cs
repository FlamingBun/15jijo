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
        while (!isDataLoad && !isDataLoadFail)
        {

        }

        if (isDataLoad)
        {
            Console.Clear();
            Console.WriteLine("Enter를 입력해주세요!");
            Console.ReadLine();
            return true;
        }
        if (isDataLoadFail)
        {
            Console.WriteLine("로딩 실패 게임을 다시 시작해주세요..");
            Console.ReadLine();
        }
        return false;
    }

    private async void LoadAllData() 
    {
        var loadingTask = ShowConsoleLoading();

        var monsters = await LoadWithConsoleLoading("Monster", "A1:E", row => new Monster(
            row[0]?.ToString() ?? "",
            int.Parse(row[1]?.ToString() ?? "1"),
            int.Parse(row[2]?.ToString() ?? "1"),
            int.Parse(row[3]?.ToString() ?? "1"),
            int.Parse(row[4]?.ToString() ?? "1")
        ));

        var equipItems = await LoadWithConsoleLoading("EquipmentItem", "A1:F", row => new EquipmentItem(
            row[0]?.ToString() ?? "",
            (ItemType)int.Parse(row[1]?.ToString() ?? "1"),
            (EquipmentItemType)int.Parse(row[2]?.ToString() ?? "1"),
            int.Parse(row[3]?.ToString() ?? "1"),
            row[4]?.ToString() ?? "1",
            int.Parse(row[5]?.ToString() ?? "1")
            )
        );

        var consumeItems = await LoadWithConsoleLoading("ConsumeItem", "A1:F", row => new ConsumeItem(
            row[0]?.ToString() ?? "",
            (ItemType)int.Parse(row[1]?.ToString() ?? "1"),
            (ConsumeItemType)int.Parse(row[2]?.ToString() ?? "1"),
            int.Parse(row[3]?.ToString() ?? "1"),
            row[4]?.ToString() ?? "1",
            int.Parse(row[5]?.ToString() ?? "1")
            )
        );

        monsterDatas = monsters;
        foreach (var _item in equipItems.GetDatas())
        {
            itemDatas.AddData(_item);
        }

        foreach (var _item in consumeItems.GetDatas())
        {
            itemDatas.AddData(_item);
        }

        var killMonsterQuests = await LoadWithConsoleLoading("KillMonsterQuest", "A1:G", row => new KillMonsterQuest(
            row[0]?.ToString() ?? "", // name
            row[1]?.ToString() ?? "", // desc
            int.Parse(row[2]?.ToString() ?? "1"), // gold
            int.Parse(row[3]?.ToString() ?? "1"), // exp 
            itemDatas.GetData(row[4]?.ToString() ?? "0"), // item
            monsterDatas.GetData(int.Parse(row[5]?.ToString() ?? "0")), // monster
            int.Parse(row[6]?.ToString() ?? "1")) // goal


        );

        var playerStatQuest = await LoadWithConsoleLoading("KillMonsterQuest", "A1:G", row => new PlayerStatQuest(
            row[0]?.ToString() ?? "",
            row[1]?.ToString() ?? "",
            int.Parse(row[2]?.ToString() ?? "1"),
            int.Parse(row[3]?.ToString() ?? "1"),
            itemDatas.GetData(row[4]?.ToString() ?? "0"),
            (questRequireStatName)int.Parse(row[5]?.ToString() ?? "1"),
            int.Parse(row[6]?.ToString() ?? "1")
           
            )


        );
        monsterQuest = killMonsterQuests;
        statQuest = playerStatQuest;




        isLoading = false; // 위에 데이터를 전부 받아오면 그때 isLoading을 false로 변경 -> 애니메이션은 Task를 반환
        await loadingTask; // 애니메이션 Task가 완전히 끝날 때까지 대기

        Console.Clear();
        Console.WriteLine("데이터 로딩 완료!");
        await Task.Delay(1000);


        //Console.WriteLine("\n\n");
        //Console.WriteLine("=====Monster=====");
        //foreach (var monster in monsterDatas.GetDatas())
        //{
        //    Console.WriteLine($"{monster.Name} {monster.TotalHp} {monster.TotalMp} {monster.CurrentAttackPower} {monster.CurrentDefensivePower}");
        //}

        //Console.WriteLine("\n");
        //Console.WriteLine("=====Items=====");
        //foreach (var item in itemDatas.GetDatas())
        //{
        //    Console.WriteLine($"{item.ItemName} | {item.ItemType}  | {item.ItemAbility} | {item.ItemDescription}  | {item.ItemPrice}");
        //}

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
                dotCount=0;
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
        }

        var purchasedInventoryToken = LoadJsonFromFile("purchasedInventoryInfo.txt");

        // C#의 패턴 매칭으로
        // inventoryToken이 JArray 타입인지 검사 -> 타입이 맞으면 inventoryArray로 캐스팅해 준다.
        if (purchasedInventoryToken is JArray purchasedInventoryArray)
        {
            List<Item> items = new List<Item>();

            foreach (var item in purchasedInventoryArray)
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

            GameManager.instance.purchasedItems = items;
        }

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
            if (GameManager.instance.havingItems.Count > 0)
            {
                if (SaveJsonToFile("inventoryInfo.txt", GameManager.instance.havingItems))
                {
                    isSaved = true;
                }
            }

            // 구매한 아이템 목록 저장
            if (GameManager.instance.purchasedItems.Count > 0)
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

