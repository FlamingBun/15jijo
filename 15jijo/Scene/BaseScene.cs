public abstract class BaseScene
{
    public int selectionCount = 0;
    public abstract SceneState SceneState { get; protected set; }
    public void DrawScene(SceneState sceneState) 
    {
        Console.Clear();
        ConsoleHelper.ShowScene(sceneState);
    }

    public abstract SceneState InputHandle();
}