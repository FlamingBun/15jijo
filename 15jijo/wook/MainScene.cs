public class MainScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Main;

    public override SceneState InputHandle()
    {
        DrawScene(SceneState);
        selectionCount = 8;

        string? input = Console.ReadLine();
        int inputNumber = -1;
        bool isValidInput = ConsoleHelper.CheckUserInput(input, selectionCount, ref inputNumber);

        if (!isValidInput) 
        {
            return SceneState;
        }

        return (SceneState)inputNumber;
    }

}
