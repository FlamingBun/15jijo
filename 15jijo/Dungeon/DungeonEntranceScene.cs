class DungeonEntranceScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.DungeonEntrance;

    public override SceneState InputHandle()
    {
        DrawScene(SceneState.DungeonEntrance);
        DungeonEntrance dungeonEntrance = new DungeonEntrance();
        selectionCount = 20;

        //while (true)
        //{
        int minNumber = -1;
        dungeonEntrance.DungeonClearLevel();
        Console.Write("입장할 층수를 입력하세요 (1~20): ");
        string input = Console.ReadLine();
        if (ConsoleHelper.CheckUserInput(input, selectionCount, ref minNumber))
        {
            if (int.Parse(input) == 0)
            {
                return SceneState.Main;
            }
            else if (int.TryParse(input, out int floor))
            {
                dungeonEntrance.SelectDungeon(floor);
                System.Threading.Thread.Sleep(3000);
                return SceneState.DungeonEntrance;//나중에 던전 전투 씬으로 이동해야함
            }
            else
            {
                return SceneState.DungeonEntrance;
            }
        }
        else
        {
            return SceneState.DungeonEntrance;
        }
        //}
    }

}

    public class DungeonEntrance
    {
        //Dungeon dungeon = new Dungeon();
        private const int MaxFloor = 20; // 총 20층
        private int clearedFloor = 0; // 처음엔 0층 클리어 상태 (1층 입장 가능)


        public void SelectDungeon(int selectFloor)
        {
            if (selectFloor < 1 || selectFloor > MaxFloor)
            {
                Console.WriteLine("\n존재하지 않는 층입니다.");
                return;
            }

            if (selectFloor > clearedFloor + 1)
            {
                Console.WriteLine($"\n{clearedFloor + 1}층 클리어해야 다음층으로 넘어갈 수 있습니다.\n");
                return;
            }

            Console.WriteLine($"\n{selectFloor}층으로 진입합니다...\n");
            ClearDungeon(selectFloor);
        }

        private void ClearDungeon(int floor)
        {
            //dungeon.GetMonsterRandomAdd(floor);
            //// 여기에 몬스터 생성/전투/보상 등 게임 진행중
            Console.WriteLine($"{floor}층 전투 중... (예시)");

            // 전투 성공했다고 가정
            ClearFloor(floor);
        }


        // 클리어한 층을 확인용 나중에 옮길게요 ㅜㅠ 
        private void ClearFloor(int floor)
        {
            if (floor > clearedFloor)
            {
                clearedFloor = floor;
                Console.WriteLine($"{floor}층 클리어! 다음 층이 열렸습니다.\n");
            }
            else
            {
                Console.WriteLine($"{floor}층은 이미 클리어한 상태입니다.\n");
            }
        }

        public void DungeonClearLevel()
        {
            Console.WriteLine($"현재까지 클리어한 층: {clearedFloor}층");
        }
    }