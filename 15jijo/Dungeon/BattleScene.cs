class BattleScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Battle;
    Random random;
    List<Monster_GARA> selectedMonsters = new List<Monster_GARA>();
    Queue<string> logQueue = new Queue<string>();

    Player player;
    int selectedLevel;
    int clearedLevel;


    public override SceneState InputHandle()
    {
        player = GameManager.instance.player;
        selectedLevel = GameManager.instance.dungeonController.selectedLevel;
        clearedLevel = GameManager.instance.dungeonController.clearedLevel;

        DrawScene(SceneState.Battle);
        Console.SetCursorPosition(0, 0);
        Console.Clear();
        GetMonsterRandomCreate();

        bool isClear = BattleSetting();
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
        else
        {
            FightLog("던전 클리어에 실패했습니다.");
            System.Threading.Thread.Sleep(300);
            FightLog("Enter.던전 입구로 되돌아갑니다.");
            Console.ReadLine();
            return SceneState.DungeonEntrance;
        }
    }

    public bool BattleSetting()
    {
        player = GameManager.instance.player;
        selectedLevel = GameManager.instance.dungeonController.selectedLevel;


        bool isClear = false; // 층 클리어
        bool isPlayerDead = false; //플레이어 사망시
        bool isPlayerRun = true; //플레이어 도망시

        while (true)
        {
            GetMonster();
            isPlayerRun = PlayerTurn();
            isClear = selectedMonsters.All(monster => monster.MonsterHp <= 0);
            if (!isPlayerRun)
            {
                FightLog("플레이어가 도망쳤습니다.");
                return false;
            }
            if (isClear)
            {
                FightLog("플레이어가 몬스터를 모두 처치했습니다!");
                return true;
            }
            MonsterTurn();
            isPlayerDead = player.CurrentHp <= 0;
            if (isPlayerDead)
            {
                FightLog("플레이어가 사망했습니다.");
                System.Threading.Thread.Sleep(300);
                FightLog($"{player.Level} {player.Name}");
                System.Threading.Thread.Sleep(300);
                FightLog($"HP {player.BasicHp} -> 0");
                System.Threading.Thread.Sleep(300);
                return false;
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
        int monsterCount = random.Next(1, 4);


        var allMonsters = MonsterData_GARA.Instance.monsterData_GARA.Values.ToList();

        // 몬스터 목록을 리스트로 가져오기
        for (int i = 0; i < monsterCount; i++)
        {
            Monster_GARA baseMonster = allMonsters[random.Next(allMonsters.Count)];
            Monster_GARA cloned = new Monster_GARA(baseMonster.MonsterName, baseMonster.MonsterBaseHp, baseMonster.MonsterBaseAttack);

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
        foreach (var monster in selectedMonsters.Where(m => m.MonsterHp > 0))
        {
            monster.PrintMonster_List(); // 기본 색상 출력
        }

        // 죽은 몬스터 나중에 출력 (회색 처리)
        foreach (var monster in selectedMonsters.Where(m => m.MonsterHp <= 0))
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            monster.PrintMonster_Dead(); // 죽은 몬스터 출력
            Console.ResetColor();
        }

        Console.WriteLine($"\n\n[{player.Name}] \nLv.1 \n직업:{player.Job} \nHP: {player.CurrentHp}/{player.BasicHp} " +
            $"\nMP:{player.CurrentMp}/{player.BasicMp} \nATK:{player.CurrentAttackPower}\n\n");
        //}

        Console.SetCursorPosition(0, 18);
        Console.WriteLine("========================================================================================================================");
        System.Threading.Thread.Sleep(1000);
    }

    public bool PlayerTurn()
    {
        player = GameManager.instance.player;
        selectedLevel = GameManager.instance.dungeonController.selectedLevel;

        if (player.CurrentHp > 1)
        {
            FightLog($"[{player.Name}]의 턴입니다.");
            FightLog(" ");
            System.Threading.Thread.Sleep(300);
            FightLog("1.공격하기");
            System.Threading.Thread.Sleep(300);
            FightLog("2.스킬");
            System.Threading.Thread.Sleep(300);
            FightLog("3.인벤토리");
            System.Threading.Thread.Sleep(300);
            FightLog("4.탈출하기");
            FightLog(" ");
            System.Threading.Thread.Sleep(300);
            FightLog("원하는 행동을 선택해주세요.");
            System.Threading.Thread.Sleep(300);
            FightLog(">>");
            System.Threading.Thread.Sleep(300);
        }
        return PlayerTurnChoise();

    }

    public bool PlayerTurnChoise()
    {
        var aliveMonsters = selectedMonsters.Where(m => m.MonsterHp > 0).ToList();

        string choise = Console.ReadLine();
        switch (choise)
        {
            case "1":
                if (aliveMonsters.Count == 1)
                {
                    var target = aliveMonsters[0];
                    System.Threading.Thread.Sleep(300);
                    FightLog($"{target.MonsterName}{target.MonsterLevel}을(를) 공격합니다!");
                    System.Threading.Thread.Sleep(300);
                    FightLog($"{target.MonsterName}{target.MonsterLevel}의 HP {player.CurrentAttackPower} 감소합니다.");
                    System.Threading.Thread.Sleep(300);
                    FightLog("");
                    target.MonsterHp -= player.CurrentAttackPower;
                    return true;
                }
                else
                {
                    FightLog("공격할 몬스터를 선택해주세요.");
                    string choise_MonsterAttack = Console.ReadLine();
                    int selected_MonsterHp = int.Parse(choise_MonsterAttack);
                    int selectedMonstersNum = 0;
                    for (int i = 1; i < aliveMonsters.Count; i++)
                    {
                        selectedMonstersNum = i;
                    }
                    if (selectedMonstersNum <= aliveMonsters.Count)
                    {
                        var target = aliveMonsters[selectedMonstersNum];
                        System.Threading.Thread.Sleep(300);
                        FightLog($"{target.MonsterName}{target.MonsterLevel}을(를) 공격합니다!");
                        System.Threading.Thread.Sleep(300);
                        FightLog($"{target.MonsterName}{target.MonsterLevel}의 HP {player.CurrentAttackPower} 감소합니다.");
                        System.Threading.Thread.Sleep(300);
                        FightLog("");

                        target.MonsterHp -= player.CurrentAttackPower;
                        return true;
                    }
                    else
                    {
                        FightLog("잘못된 입력입니다.");
                        return false;
                    }
                }
            case "2":
                FightLog("스킬을 사용합니다.");
                System.Threading.Thread.Sleep(10000);
                return true;
            case "3":
                FightLog("인벤토리");
                System.Threading.Thread.Sleep(10000);
                return true;
            case "4":
                FightLog("던전 입구로 도망칩니다.");
                FightLog($"몬스터의 공격으로 {player.Name}의 HP {(int)(player.BasicHp * 0.2f)}이 감소합니다.");
                player.TakeDamage((int)(player.BasicHp * 0.2f));
                FightLog(">>");
                FightLog("던전 입구로 이동합니다.");
                System.Threading.Thread.Sleep(1000);
                break;
            default:
                FightLog("잘못된 입력입니다.");
                FightLog("원하는 행동을 다시 입력해주세요.");
                System.Threading.Thread.Sleep(10000);
                return true;
        }
        return false;
    }

    public bool MonsterTurn()
    {


        player = GameManager.instance.player;

        if (player.CurrentHp <= 0)
        {
            return true;
        }
        foreach (var monster in selectedMonsters.Where(m => m.MonsterHp > 0))
        {
            GetMonster();
            System.Threading.Thread.Sleep(300);
            FightLog("몬스터들이 공격을 시작합니다.");
            FightLog("");

            System.Threading.Thread.Sleep(300);
            FightLog($"[{monster.MonsterName}{monster.MonsterLevel}]이 {player.Name}를 공격했습니다.");
            System.Threading.Thread.Sleep(300);
            FightLog($"Player의 HP가 {Math.Round(monster.MonsterAttack)} 감소했습니다.");
            FightLog("");
            System.Threading.Thread.Sleep(300);

            player.TakeDamage((int)(monster.MonsterAttack));
            FightLog("");
        }
        return true;
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
}

