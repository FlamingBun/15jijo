public class StatusScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Status;

    public override SceneState InputHandle()
    {
        DrawScene(SceneState);
        selectionCount = 0;

        string? input = Console.ReadLine();
        int inputNumber = -1;
        bool isValidInput = ConsoleHelper.CheckUserInput(input, selectionCount, ref inputNumber);

        if (!isValidInput)
        {
            return SceneState;
        }

        return SceneState.Main;
    }
}