public class SaveScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.SaveGame;

    public override SceneState InputHandle()
    {
        if (DataManager.instance.SavePlayerData())
        {
            Console.WriteLine("데이터 저장 성공!");
        }
        else 
        {
            Console.WriteLine("데이터 저장 실패..");
        }
        Thread.Sleep(1400);
        return SceneState.Main;
    }

}
