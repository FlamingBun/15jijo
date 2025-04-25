using System.Linq.Expressions;

public class MakeCharacterScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.MakeCharacter;

    private string? inputName;
    private Jobs selectedJob;
    private bool isNameSaved;
    private bool isJobSelected;

    public override SceneState InputHandle()
    {
        isNameSaved = InputUserName();
        isJobSelected = SelectJob();

        if (isNameSaved && isJobSelected)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.player = new Player(inputName, selectedJob);
            }
            return SceneState.Main;
        }
        else 
        {
            return SceneState;
        }
    }

    private bool InputUserName()
    {
        while (true)
        {
            selectionCount = 1;

            DrawScene(SceneState);
            inputName = Console.ReadLine();

            Console.WriteLine($"\n입력하신 이름은 {inputName} 입니다.");
            Console.WriteLine($"\n1. 저장");
            Console.WriteLine($"0. 취소\n");
            Console.Write($"원하시는 행동을 입력해주세요.\n>>");

            string? input = Console.ReadLine();
            int inputNumber = -1;

            bool isValidInput = ConsoleHelper.CheckUserInput(input, selectionCount, ref inputNumber);
            if (!isValidInput)
            {
                continue;
            }

            if (inputNumber == 0)
            {
                continue;
            }
            else
            {
                return true;
            }
        }


    }

    private bool SelectJob()
    {
        while (true)
        {
            DrawScene(SceneState.SelectJob);

            string? input = Console.ReadLine();
            int inputNumber = -1;

            // 직업 개수
            selectionCount = 3;

            bool isValidInput = ConsoleHelper.CheckUserInputNoZero(input, selectionCount, ref inputNumber);
            if (!isValidInput)
            {
                continue;
            }


            selectedJob = (Jobs)inputNumber;

            return true;
        }
    }

}
