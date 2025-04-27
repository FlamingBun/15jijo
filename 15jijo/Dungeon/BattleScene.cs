class BattleScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Battle;
    Random random;
    List<Monster> selectedMonsters = new List<Monster>();
    Queue<string> logQueue = new Queue<string>();

    Player player;
    int selectedLevel;
    int clearedLevel;

    bool isClear = false; // 층 클리어
    bool isPlayerDead = false; //플레이어 사망시
    bool isPlayerRun = false; //플레이어 도망시
    bool isPlayerTurnPreserve = false; // 플레이어턴 유지

    public override SceneState InputHandle()
    {
        player = GameManager.instance.player;
        selectedLevel = GameManager.instance.dungeonController.selectedLevel;
        clearedLevel = GameManager.instance.dungeonController.clearedLevel;

        BattleSetting();
        DrawScene(SceneState.Battle);
        Console.SetCursorPosition(0, 0);
        Console.Clear();
        GetMonsterRandomCreate();
        BattleStart();

        if (isClear)
        {
            if (GameManager.instance.dungeonController.clearedLevel < GameManager.instance.dungeonController.maxFloor)
            {
                GameManager.instance.dungeonController.clearedLevel++;
            }
            FightLog($"던전 {clearedLevel + 1}층을 클리어했습니다. ");
            System.Threading.Thread.Sleep(300);
            FightLog("Enter.던전 입구로 되돌아갑니다.");
            Console.ReadLine();
            return SceneState.DungeonEntrance;
        }
        else if (isPlayerDead)
        {
            FightLog("던전 클리어에 실패했습니다.");
            System.Threading.Thread.Sleep(300);
            FightLog("Enter를 누르면 던전 입구로 되돌아갑니다.");
            Console.ReadLine();
            return SceneState.DungeonEntrance;
        }
        else if (isPlayerRun)
        {
            FightLog("던전 입구로 도망칩니다.");
            FightLog($"몬스터의 공격으로 {player.Name}의 HP {(int)(player.BasicHp * 0.2f)}이 감소합니다.");
            player.TakeDamage((int)(player.BasicHp * 0.2f));
            System.Threading.Thread.Sleep(300);
            FightLog("Enter를 누르면 던전 입구로 되돌아갑니다.");
            Console.ReadLine();
            return SceneState.DungeonEntrance;
        }

        return SceneState.DungeonEntrance;
    }
    public void BattleSetting()
    {
        player = GameManager.instance.player;
        selectedLevel = GameManager.instance.dungeonController.selectedLevel;

        isClear = false; // 층 클리어
        isPlayerDead = false; //플레이어 사망시
        isPlayerRun = false; //플레이어 도망시
        isPlayerTurnPreserve = false;

    }

    public void BattleStart()
    {
        while (true)
        {
            GetMonster();
            PlayerTurn();
            isClear = selectedMonsters.All(monster => monster.CurrentHp <= 0);
            if (isPlayerRun)
            {
                FightLog("플레이어가 도망쳤습니다.");
                return;

            }
            if (isClear)
            {
                FightLog("플레이어가 몬스터를 모두 처치했습니다!");
                return;
            }
            if (isPlayerTurnPreserve)
            {
                continue;
            }
            MonsterTurn();
            if (isPlayerDead)
            {
                FightLog("플레이어가 사망했습니다.");
                System.Threading.Thread.Sleep(300);
                FightLog($"{player.Level} {player.Name}");
                System.Threading.Thread.Sleep(300);
                FightLog($"HP {player.BasicHp} -> 0");
                System.Threading.Thread.Sleep(300);
                return;
            }
        }
    }


    public void GetMonsterRandomCreate()
    {
        player = GameManager.instance.player;
        selectedLevel = GameManager.instance.dungeonController.selectedLevel;

        logQueue.Clear();
        random = new Random();
        selectedMonsters.Clear();
        int monsterCount = random.Next(2, 4);


        var allMonsters = DataManager.instance.monsterDatas.GetDatas();

        // 몬스터 목록을 리스트로 가져오기
        for (int i = 0; i < monsterCount; i++)
        {
            Monster baseMonster = allMonsters[random.Next(selectedLevel-1, selectedLevel+2)]; // TODO:: 수정
            Monster cloned = new Monster(baseMonster.Name, baseMonster.CurrentHp, baseMonster.CurrentAttackPower);

            int randomLevel = random.Next(Math.Max(1, selectedLevel - 2), selectedLevel + 3);
            cloned.SetLevel(randomLevel);
            selectedMonsters.Add(cloned);
        }
    }

    public void GetMonster()
    {
        selectedLevel = GameManager.instance.dungeonController.selectedLevel;


        Console.SetCursorPosition(0, 0);
        Console.WriteLine($"BATTLE START!!!!\n");
        Console.WriteLine("\n[몬스터정보]\n");

        //isClear = (selectedMonsters.MonsterHp <= 0) ? true : false;

        // 살아있는 몬스터 먼저 출력
        foreach (var (monster, index) in selectedMonsters
            .Select((value, index) => (value, index))
            .Where(x => x.value.CurrentHp > 0))
        {
            Console.Write($"{index}. ");  // 인덱스 먼저 출력
            monster.PrintMonster_List();   // 몬스터 출력
        }

        // 죽은 몬스터 출력
        foreach (var (monster, index) in selectedMonsters
            .Select((value, index) => (value, index))
            .Where(x => x.value.CurrentHp <= 0))
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{index}. ");  // 인덱스 먼저 출력
            monster.PrintMonster_Dead();
            Console.ResetColor();
        }

        Console.WriteLine($"\n\n[{player.Name}] \nLv.1 \n직업:{player.Job} \nHP: {player.CurrentHp}/{player.BasicHp} " +
            $"\nMP:{player.CurrentMp}/{player.BasicMp} \nATK:{player.CurrentAttackPower}\n\n");
        //}

        Console.SetCursorPosition(0, 18);
        Console.WriteLine("========================================================================================================================");
        System.Threading.Thread.Sleep(1000);
    }

    public void PlayerTurn()
    {
        player = GameManager.instance.player;
        selectedLevel = GameManager.instance.dungeonController.selectedLevel;
        FightLog($"[{player.Name}]의 턴입니다.");
        FightLog(" ");
        FightLog("1.공격하기");
        //FightLog("2.스킬");
        //FightLog("3.인벤토리");
        FightLog("2.탈출하기");
        FightLog(" ");
        FightLog("원하는 행동을 선택해주세요.");
        System.Threading.Thread.Sleep(300);
        FightLog(">>",0);
        System.Threading.Thread.Sleep(300);
        PlayerTurnChoise();
    }

    public void PlayerTurnChoise()
    {
        var aliveMonsters = selectedMonsters.Where(m => m.CurrentHp > 0).ToList();
        isPlayerTurnPreserve = false;
        string choise = Console.ReadLine();
        switch (choise)
        {
            case "1":
                while (true)
                {
                    isPlayerTurnPreserve = true;
                    FightLog("공격할 몬스터를 선택해주세요.");
                    FightLog("0. 취소");
                    FightLog(">>",0);
                    string choise_MonsterAttack = Console.ReadLine();
                    int selectedMonstersNum = -1;
                    bool isCould = ConsoleHelper.CheckUserInput(choise_MonsterAttack, aliveMonsters.Count, ref selectedMonstersNum, true);
                    if (isCould && selectedMonstersNum != 0)
                    {
                        var target = aliveMonsters[selectedMonstersNum - 1];
                        System.Threading.Thread.Sleep(300);
                        FightLog($"{target.Name}{target.MonsterLevel}을(를) 공격합니다!");
                        System.Threading.Thread.Sleep(300);
                        FightLog($"{target.Name}{target.MonsterLevel}의 HP {player.CurrentAttackPower} 감소합니다.");
                        System.Threading.Thread.Sleep(300);
                        FightLog("");
                        target.TakeDamage(player.CurrentAttackPower);
                        isPlayerTurnPreserve = false;
                        break;

                    }
                    else if (selectedMonstersNum == 0)
                    {
                        return;
                    }
                    else
                    {
                        FightLog("잘못된 입력입니다.");
                        FightLog("공격할 대상을 선택해주세요.");
                        System.Threading.Thread.Sleep(1000);
                        break;
                    }
                }

                break;
            //case "2":
            //    FightLog("스킬을 사용합니다.");
            //    System.Threading.Thread.Sleep(10000);
            //    break;
            //case "3":
            //    FightLog("인벤토리");
            //    System.Threading.Thread.Sleep(10000);
            //    break;
            case "2":
                FightLog("던전 입구로 도망칩니다.");
                FightLog($"몬스터의 공격으로 {player.Name}의 HP {(int)(player.BasicHp * 0.2f)}이 감소합니다.");
                player.TakeDamage((int)(player.BasicHp * 0.2f));
                isPlayerRun = true;
                FightLog(">>",0);
                FightLog("던전 입구로 이동합니다.");
                System.Threading.Thread.Sleep(1000);
                break;
            default:
                FightLog("잘못된 입력입니다.");
                FightLog("원하는 행동을 다시 입력해주세요.");
                isPlayerTurnPreserve = true;
                System.Threading.Thread.Sleep(1000);
                break;

        }

    }


    public void PlayerAttack() 
    {
        bool isValidInput = false;
        int inputNumber = -1;
        while (!isValidInput)
        {
            FightLog("공격할 몬스터를 선택해주세요.");
            string? input = Console.ReadLine();
            selectionCount = selectedMonsters.Count;
            isValidInput = ConsoleHelper.CheckUserInputNoZero(input, selectionCount, ref inputNumber);

            if (inputNumber <= selectedMonsters.Count)
            {
                
            }
            else
            {
                FightLog("잘못된 입력입니다.");
            }
            
            selectionCount = selectedMonsters.Count;
            isValidInput = ConsoleHelper.CheckUserInput(input, selectionCount, ref inputNumber);
        }
        // inputNumber가 0이면 다시 선택
        if (inputNumber != 0) 
        {
            AttakMonster();
        }
    }

    public void AttakMonster() 
    {
        
    }


    public void MonsterTurn()
    {
        foreach (var monster in selectedMonsters.Where(m => m.CurrentHp > 0))
        {
            GetMonster();
            System.Threading.Thread.Sleep(300);
            FightLog("몬스터들이 공격을 시작합니다.");
            FightLog("");

            System.Threading.Thread.Sleep(300);
            FightLog($"[LV.{monster.MonsterLevel} {monster.Name}]이 {player.Name}를 공격했습니다.");
            System.Threading.Thread.Sleep(300);
            FightLog($"Player의 HP가 {Math.Round(monster.CurrentAttackPower)} 감소했습니다.");
            FightLog("");
            System.Threading.Thread.Sleep(300);

            player.TakeDamage((int)(monster.CurrentAttackPower));
            if (player.CurrentHp <= 0)
            {
                isPlayerDead = true;
                FightLog("");
                return;
            }
            FightLog("");
        }
        return;
    }


    public void FightLog(string message)
    {
        if (logQueue.Count >= 9)
        {
            logQueue.Dequeue(); // 가장 오래된 문장 삭제
        }

        logQueue.Enqueue(message);

        int startX = 0;
        int startY = 19; // 로그를 표시할 시작 위치

        foreach (var log in logQueue)
        {
            Console.SetCursorPosition(startX, startY);
            Console.Write(new string(' ', Console.WindowWidth)); // 기존 줄 지우기
            Console.SetCursorPosition(startX, startY);
            Console.WriteLine(log);
            startY++;
        }
    }

    public void FightLog(string message, int a)
    {
        if (logQueue.Count >= 9)
        {
            logQueue.Dequeue(); // 가장 오래된 문장 삭제
        }

        logQueue.Enqueue(message);

        int startX = 0;
        int startY = 19; // 로그를 표시할 시작 위치

        foreach (var log in logQueue)
        {
            Console.SetCursorPosition(startX, startY);
            Console.Write(new string(' ', Console.WindowWidth)); // 기존 줄 지우기
            Console.SetCursorPosition(startX, startY);
            Console.Write(log);
            startY++;
        }
    }
}

