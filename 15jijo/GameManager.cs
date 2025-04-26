using _15jijo.ho;

public class GameManager
{
    public static GameManager? instance;
    private Dictionary<SceneState, BaseScene>? scenes;
    public Player? player;
    public Skills? skills;
    public List<Item>? havingItems;
    public List<Item>? purchasedItems;
    public QuestController? questController;
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
        skills = new();
        havingItems = new()
        {
            
        };
        purchasedItems= new();
        questController = new();


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
        scenes.Add(SceneState.Shop, new ShopScene());
        scenes.Add(SceneState.Buying, new BuyingScene());
        scenes.Add(SceneState.Selling, new SellingScene());
        scenes.Add(SceneState.Inventory, new Inventory());
        scenes.Add(SceneState.Fitting, new FittingScene());
        scenes.Add(SceneState.Eating, new EatingScene());
        scenes.Add(SceneState.SaveGame, new SaveScene());
        scenes.Add(SceneState.Quest, new QuestScene());
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


