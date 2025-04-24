public class GameManager
{
    public static GameManager? instance;

    private Dictionary<SceneState, BaseScene>? scenes;

    public Player? player;

    public Skills? skills;

    private bool hasData;

    private SceneState currentSceneState;

    public GameManager() 
    {
        Init();
    }

    public void Init() 
    {
        if (instance == null) 
        {
            instance = this;
        }
        // TODO: 게임에 필요한 객체들 생성 및 초기화
        
        // 만약 데이터가 없으면 false -> 캐릭터 생성씬
        hasData = false;
        InitAllScenes();


        if (hasData)
        {
            currentSceneState = SceneState.Main;
        }
        else
        {
            currentSceneState = SceneState.MakeCharacter;
        }
    }

    private void InitAllScenes() 
    {
        scenes = new Dictionary<SceneState, BaseScene>();
        scenes.Add(SceneState.MakeCharacter, new MakeCharacterScene());
        scenes.Add(SceneState.Main, new MainScene());
        scenes.Add(SceneState.Relax, new RelaxScene());
        scenes.Add(SceneState.DungeonEntrance, new DungeonEntranceScene());
        scenes.Add(SceneState.Status, new StatusScene());
            // [jaeyoon] 상점/인벤토리 관련
        //scenes.Add(SceneState.Inventory, new SceneInventory());
        //scenes.Add(SceneState.ItemUse, new SceneItemUse());
        //scenes.Add(SceneState.Shop, new SceneShop());
        //scenes.Add(SceneState.Buy, new SceneBuy());
        //scenes.Add(SceneState.Sell, new SceneSell());
    }

    public void GameStart()
    {
        while (currentSceneState != SceneState.ExitGame) 
        {
            if (scenes != null && scenes.ContainsKey(currentSceneState))
            {
                currentSceneState = scenes[currentSceneState].InputHandle();
            }
        }
        Console.WriteLine("게임이 종료되었습니다.");
        Thread.Sleep(1500);
    }
}


