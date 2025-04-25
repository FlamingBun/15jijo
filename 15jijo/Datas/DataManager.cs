public class DataManager
{
    public static DataManager instance;

    public bool isDataLoad = false;
    public bool isDataLoadFail = false;

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

        GetDatasFromGoogleSheet();
        DataLoadCheck();
    }

    private async void GetDatasFromGoogleSheet() 
    {
        string spreadsheetId = "143M8AOaELmDidz6b7ohdmbkRB06PO91rb_ITkUzQa34";
        string sheetName = "Monster";
        string apiKey = "AIzaSyCS9EqqmRV_VbRqFGkh0CO01XHvvUXcb8I";
        string range = "A2:E";

        string url = $"https://sheets.googleapis.com/v4/spreadsheets/{spreadsheetId}/values/{sheetName}!{range}?key={apiKey}";

        using HttpClient client = new HttpClient();
        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("받은 데이터:\n" + json);
            isDataLoad = true;
        }
        else
        {
            Console.WriteLine("에러 발생: " + response.StatusCode);
            isDataLoadFail = true;
        }

        Thread.Sleep(2000);
    }

    private void DataLoadCheck()
    {
        int dotCount = 0;
        // 데이터 로드에 성공 또는 실패 시 데이터 로딩중 종료
        while (isDataLoad != true && isDataLoadFail != true)
        {
            Thread.Sleep(1000);
            Console.Clear();
            dotCount++;
            if (dotCount > 3) dotCount = 0;
            Console.Write("데이터 로딩중");
            for (int i = 0; i < dotCount; i++)
            {
                Console.Write(".");
            }
            Thread.Sleep(1000);
        }

        Console.Clear();
        if (isDataLoad) Console.WriteLine("게임에 진입");
        if (isDataLoadFail)Console.WriteLine("데이터 로드에 실패하였습니다.");

        Thread.Sleep(1000);
    }

    public void SaveData() 
    {
    
    }


    private void LoadAllData() 
    {
        // TODO: 모든 DatasKey의 IBaseData에 


    }

    private bool LoadPlayerData() 
    {
        return false;
    }
}

