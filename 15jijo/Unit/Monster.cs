
public class Monster : Unit
{
    public int MonsterLevel;
    public override string? Name { get; protected set; }
    public override float CurrentHp { get; protected set; }
    public override float CurrentMp { get; protected set; }
    public override float CurrentAttackPower { get { return TotalAttackPower; } set { } }
    public override float CurrentDefensivePower => TotalDefensivePower;

    public float TotalAttackPower;
    public float TotalDefensivePower;
    public float TotalMp { get; private set; }

    public bool isDead = false;
    public override List<Skill>? AvailableSkills { get; protected set; }

    public Monster(string _name, float _currentHP, float _totalMP, float _attackPower, float _defensivePower) 
    {
        Name = _name;

        
        CurrentHp = _currentHP;

        TotalMp = _totalMP;
        CurrentMp = TotalMp;

        TotalAttackPower = _attackPower;
        TotalDefensivePower = _defensivePower;
    }

    public Monster(string _name, float _currentHP, float _attackPower)
    {
        Name = _name;

        CurrentHp = _currentHP;

        TotalAttackPower = _attackPower;
        
    }



    public override void KillUnit()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        CurrentHp -= damage;
        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            GameManager.instance.questController.FindMonsterObjectInPlayerQuest(Name);
            isDead = true;
        }
    }


    public void PrintMonster_List()
    {
        Console.WriteLine($"Lv.{MonsterLevel} | {Name} | HP:{Math.Round(CurrentHp)} | 공격력:{Math.Round(CurrentAttackPower)}    ");
    }


    public void PrintMonster_Dead()
    {
        Console.WriteLine($"Lv.{MonsterLevel} | {Name}  | DEAD                                     ");//DEAD 색상 회색으로 변경 추가. 
    }

    public void SetLevel(int level)
    {
        MonsterLevel = Math.Max(1, level); // 최소 레벨 1 보장
        CurrentHp = (float)(CurrentHp * Math.Pow(1.2, MonsterLevel - 1));
        CurrentAttackPower = (float)(CurrentAttackPower * Math.Pow(1.2, MonsterLevel - 1));
    }
}

