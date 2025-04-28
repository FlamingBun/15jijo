class DungeonEntranceScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.DungeonEntrance;
    int clearedLevel;


    public override SceneState InputHandle()
    {
        DrawScene(SceneState.DungeonEntrance);
        selectionCount = 10;

        int inputNumber = -1;
        Console.Write("교육할 조를 입력하세요 (1~10): ");
        string input = Console.ReadLine();
        bool isValidInput = ConsoleHelper.CheckUserInput(input, selectionCount, ref inputNumber);

        if (!isValidInput)
        {
            return SceneState;
        }
        if (inputNumber == 0)
        {
            return SceneState.Main;
        }

        bool isSeLected = SelectDungeon(inputNumber);
        if (!isSeLected)
        {
            return SceneState.DungeonEntrance;
        }
        else
        {
            return SceneState.Battle; // 전투 씬으로 이동
        }
    }
    public bool SelectDungeon(int inputLevel)
    {
        clearedLevel = GameManager.instance.dungeonController.clearedLevel;
        GameManager.instance.dungeonController.selectedLevel = inputLevel;

        if (inputLevel > clearedLevel + 1)
        {
            Console.WriteLine($"\n해당조는 잠겨있습니다. 이전 조를 먼저 교육해주세요. ~ ☆ .☆ ~\n");
            Thread.Sleep(1500);
            return false;
        }
        Console.Write($"\nZEP {inputLevel}조로 진입합니다.");
        Console.Write(".");
        System.Threading.Thread.Sleep(1000);
        Console.Write(".");
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine(".");
        System.Threading.Thread.Sleep(1000);
        //ClearDungeon(selectLevel);//전투씬으로 이동하기로 변경해야 함. 
        return true;
    }
}