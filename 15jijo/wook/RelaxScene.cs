public class RelaxScene:BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Relax;

    public override SceneState InputHandle()
    {
        DrawScene(SceneState);
        selectionCount = 1;

        string input = Console.ReadLine();
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

        // TODO: 플레이어 체력 회복
        // GameManager.instance.player.HPPP회복!!
        // GameManager.instance.player.SpendMoney!!
        return SceneState.Relax;

    }

}
