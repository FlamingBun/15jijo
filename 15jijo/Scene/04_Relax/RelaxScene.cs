public class RelaxScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Relax;

    public override SceneState InputHandle()
    {
        DrawScene(SceneState);
        selectionCount = 1;

        string? input = Console.ReadLine();
        int inputNumber = -1;
        bool isValidInput = ConsoleHelper.CheckUserInput(input, selectionCount, ref inputNumber);

        if (!isValidInput) 
        {
            return SceneState;
        }

        if (inputNumber == 0)
        {
            return SceneState.Main;
        }

        if (GameManager.instance != null)
        {
            Player? player = GameManager.instance.player;

            if (player != null)
            {
                if (player.SpendGold(500))
                {
                    player.Heal(player.TotalHp);
                    Console.WriteLine("휴식을 완료하였습니다.");
                    Thread.Sleep(1500);
                }
                else
                {
                    Console.WriteLine("Gold 가 부족합니다.");
                    Thread.Sleep(1500);
                }
            }
        }
        return SceneState.Relax;
    }
}
