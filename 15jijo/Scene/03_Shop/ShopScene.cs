public class ShopScene : BaseScene
{
    public override SceneState SceneState { get; protected set; } = SceneState.Shop;

    public override SceneState InputHandle()
    {
        DrawScene(SceneState);
        selectionCount = 2;

        string? input = Console.ReadLine();
        int inputNumber = -1;
        bool isValidInput = ConsoleHelper.CheckUserInput(input, selectionCount, ref inputNumber);

        if (!isValidInput)
        {
            return SceneState;
        }

        switch (inputNumber)
        {
            case 0:
                return SceneState.Main;
            case 1:
                return SceneState.Buying;
            case 2:
                return SceneState.Selling;
            default:
                Console.WriteLine("로직오류입니다.");
                return SceneState;
        }
    }
}