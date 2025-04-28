internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("프로그램 시작...");
            DataManager dataManager = new DataManager();
            Console.WriteLine("데이터 매니저 초기화 완료, 데이터 로드 중...");
            
            if (dataManager.CheckLoadData()) 
            {
                Console.WriteLine("데이터 로드 성공, 게임 매니저 초기화 중...");
                GameManager gameManager = new GameManager();
                Console.WriteLine("게임 매니저 초기화 완료, 게임 시작 중...");
                gameManager.GameStart();
            }
            else
            {
                Console.WriteLine("데이터 로드에 실패했습니다. 프로그램을 종료합니다.");
                Console.ReadLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"프로그램 실행 중 오류가 발생했습니다: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine("엔터 키를 누르면 프로그램이 종료됩니다.");
            Console.ReadLine();
        }
    }
}