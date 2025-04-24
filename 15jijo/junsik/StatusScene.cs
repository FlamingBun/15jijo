public class StatusScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Status;

    public override SceneState InputHandle()
    {
        DrawScene(SceneState);

        string? input = Console.ReadLine();

        if (int.TryParse(input, out int inputNumber) && inputNumber.ToString() == input
            && inputNumber == 0)
        {
            return SceneState.Main;
        }

        return SceneState;
    }
}