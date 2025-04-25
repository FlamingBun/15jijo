internal class Program
{
    static void Main(string[] args)
    {
        DataManager dataManager = new DataManager();
        if (dataManager.CheckLoadData()) 
        {
            GameManager gameManager = new GameManager();
            gameManager.GameStart();
        }
    }
}