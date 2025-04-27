using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class MonsterData_GARA
{
    public static MonsterData_GARA Instance { get; } = new MonsterData_GARA();
    public Dictionary<string, Monster_GARA> monsterData_GARA = new Dictionary<string, Monster_GARA>();

    private MonsterData_GARA()
    {
        monsterData_GARA = new Dictionary<string, Monster_GARA>();
        monsterData_GARA.Add("고블린", new Monster_GARA("고블린", 30, 5));
        monsterData_GARA.Add("오크", new Monster_GARA("오크", 20, 10));
        monsterData_GARA.Add("드래곤", new Monster_GARA("드래곤", 50, 10));
        monsterData_GARA.Add("늑대인간", new Monster_GARA("늑대인간", 50, 10));
        monsterData_GARA.Add("스켈레톤", new Monster_GARA("스켈레톤", 80, 10));
        monsterData_GARA.Add("슬라임", new Monster_GARA("슬라임", 50, 10));
    }
}
