public class Monster_GARA
{
    //private BattleScene logger = new BattleScene();
    public string MonsterName;
    public float MonsterBaseHp;
    public float MonsterBaseAttack;
    public int MonsterLevel;

    public float MonsterHp;
    public float MonsterAttack;
    public bool IsDead { get; private set; } = false;


    public Monster_GARA(string name, float baseHp, float baseAttack)
    {
        MonsterName = name;
        MonsterBaseHp = baseHp;
        MonsterBaseAttack = baseAttack;
    }

    public void TakeDamage(float damage)
    {
        MonsterHp -= damage;
        if (MonsterHp <= 0)
        {
            MonsterHp = 0;
            IsDead = true;
        }
    }

    //public void DoSomething()
    //{
    //    logger.FightInformation("몬스터가 행동합니다.");
    //}

    public void PrintMonster_List()
    {
        Console.WriteLine($"Lv.{MonsterLevel} | {MonsterName} | HP:{Math.Round(MonsterHp)} | 공격력:{Math.Round(MonsterAttack)}");
    }

    //public static void PrintMonster_Introduce(List<Monster_GARA> _monsterList)
    //{
    //    for (int i = 0; i < _monsterList.Count; i++)
    //    {
    //        Console.Write($"{_monsterList[i].MonsterName}{_monsterList[i].MonsterLevel}, ");
    //    }
    //}

    public void PrintMonster_Dead()
    {
        //bool MonsterIsDead = false; // 몬스터가 죽었는지 여부를 나타내는 변수 추가
        //MonsterIsDead = MonsterHp <= 0;
        Console.WriteLine($"Lv.{MonsterLevel} | DEAD                                       ");//DEAD 색상 회색으로 변경 추가. 
    }

    public void SetLevel(int level)
    {
        MonsterLevel = Math.Max(1, level); // 최소 레벨 1 보장
        MonsterHp = (float)(MonsterBaseHp * Math.Pow(1.2, MonsterLevel - 1));
        MonsterAttack = (float)(MonsterBaseAttack * Math.Pow(1.2, MonsterLevel - 1));
    }
}
