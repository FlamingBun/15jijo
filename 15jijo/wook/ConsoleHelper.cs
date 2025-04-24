public static class ConsoleHelper
{
    public static void ShowScene(SceneState _sceneState) 
    {
        switch (_sceneState) 
        {
            case SceneState.MakeCharacter:
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n\n");
                Console.WriteLine("원하시는 이름을 설정해주세요.");
                Console.Write(">>");
                break;
            case SceneState.Main:
                // TODO: 현재 레벨 받아오기
                int dungeonClearLevel = 1;
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n");
                Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");
                Console.WriteLine("1. 상태보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 휴식하기");
                Console.WriteLine("5. 퀘스트");
                Console.WriteLine($"6. 던전 입장(현재 진행: {dungeonClearLevel}층)");
                Console.WriteLine("7. 게임 저장하기");
                Console.WriteLine("8.게임 종료하기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.\n");
                Console.Write(">>");
                break;
            case SceneState.Relax:
                // TODO: Gold 받아오기
                // int gold = GameManager.instance.player.GetGold();
                int gold = 0;
                Console.WriteLine("휴식하기\n");
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다.(보유 골드 : {gold})\n");
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.\n");
                Console.Write(">>");
                break;
            case SceneState.SelectJob:
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n");
                Console.WriteLine("원하시는 직업을 선택해주세요.\n");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 궁수");
                Console.WriteLine("3. 법사\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.\n");
                Console.Write(">>");
                break;
        }
    }

    public static void ShowText() 
    {
    
    }

    public static bool CheckUserInput(string _input, int _selectionCount,ref int _inputNumber) 
    {
        if (!int.TryParse(_input, out int inputNumber)) 
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

        if (inputNumber.ToString() != _input)
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1500);
            return false;
        }

        _inputNumber = inputNumber;
        return true;
    }

    public static bool CheckUserInputNoZero(string _input, int _selectionCount, ref int _inputNumber)
    {
        if (!int.TryParse(_input, out int inputNumber))
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

        if (inputNumber.ToString() != _input)
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1500);
            return false;
        }

        _inputNumber = inputNumber;
        return true;
    }
}

