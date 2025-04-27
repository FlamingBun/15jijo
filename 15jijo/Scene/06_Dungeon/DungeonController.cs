using System.Threading;

public class DungeonController
{
    //public DungeonController Instance { get; private set; }

    public int clearedLevel; // 처음엔 0층 클리어 상태 (1층 입장 가능)
    public int selectedLevel; // 선택한 층수
    public int maxFloor = 10; // 총 10층, 추후 20층으로 변경 할 수 있음. 



    public DungeonController()
    {
        clearedLevel = 0;
        selectedLevel = 1;
    }   

    //public void GetDungeon(BattleScene gameClear)
    //{
    //    clearLevel = gameClear;
    //}

}
