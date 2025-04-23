using System.Runtime.CompilerServices;

public class Player : Unit
{
    public override string Name { get; protected set; }
    public override int Level { get; protected set; }
    public override float CurrentHp { get; protected set; }
    public override float CurrentMp { get; protected set; }
    public override float CurrentAttackPower { get; protected set; }
    public override float CurrentDefensivePower { get; protected set; }
    public override List<Skill> AvailableSkills { get; protected set; }
    public override void KillUnit()
    {
        UpdateGold(-5000); // Update(-Gold * 0.2f) 하고싶었는데 Gold는 int형
        UpdateExp(-100); // UpdateExp(-RequiredExp * 0.2f)
    }
    public Jobs Job { get; private set; }
    public int Exp { get; private set; }
    public int RequiredExp { get; private set; }
    public int SkillGrade { get; private set; }
    public float BasicHp { get; private set; }
    public float AdditionalHp { get; private set; }
    public float TotalHp => BasicHp + AdditionalHp;
    public float BasicMp { get; private set; }
    public float AdditionalMp { get; private set; }
    public float TotalMp => BasicMp + AdditionalMp;
    public float BasicAttackPower { get; private set; }
    public float AdditionalAttackPower { get; private set; }
    public float TotalAttackPower => BasicAttackPower + AdditionalAttackPower;
    public float BasicDefensivePower { get; private set; }
    public float AdditionalDefensivePower { get; private set; }
    public float TotalDefensivePower => BasicDefensivePower + AdditionalDefensivePower;
    public int Gold { get; private set; }

    public Player(string _inputName, int _selectedJob)
    {
        Name = _inputName;
        Job = (Jobs)_selectedJob;
        Level = 1;
        Exp = 0;
        RequiredExp = 10;
        Gold = 0;
        SkillGrade = 0;
        AvailableSkills = new List<Skill>();
        switch (Job)
        {
            case Jobs.전사:
                AvailableSkills.Add(new Skill(Jobs.전사, 0, "알파 스트라이크", "공격력 * 2 로 하나의 적을 공격합니다.", CurrentAttackPower * 2, 10));
                break;
            case Jobs.궁수:
                AvailableSkills.Add(new Skill(Jobs.궁수, 0, "파이어 샷", "공격력 * 2 로 하나의 적을 공격합니다.", CurrentAttackPower * 2, 10));
                break;
            case Jobs.법사:
                AvailableSkills.Add(new Skill(Jobs.법사, 0, "에너지 볼", "공격력 * 2 로 하나의 적을 공격합니다.", CurrentAttackPower * 2, 10));
                break;
        } // 임시용 스킬넣기
        BasicHp = 100f;
        AdditionalHp = 0f;
        CurrentHp = TotalHp;
        BasicMp = 50f;
        AdditionalMp = 0f;
        CurrentMp = TotalMp;
        BasicAttackPower = 10f;
        AdditionalAttackPower = 0f;
        CurrentAttackPower = TotalAttackPower;
        BasicDefensivePower = 5f;
        AdditionalDefensivePower = 0f;
        CurrentDefensivePower = TotalDefensivePower;
    }

    public void GetSkill(Jobs jobs, int skillGrade)
    {
        
    }
    public void LevelUp()
    {
        ++Level;
        BasicAttackPower += 0.5f;
        BasicDefensivePower += 1f;
    }
    public void UpdateExp(int value)
    {
        if ((value > 0 && Exp > int.MaxValue - value) ||
        (value < 0 && Exp < int.MinValue - value))
        {
            Console.WriteLine("Exp값이 유효하지 않습니다.");
            return;
        }

        Exp += value;

        if (Exp < 0)
        {
            Exp = 0;
        }
        else if (Exp >= RequiredExp)
        {
            LevelUp();
            Exp %= RequiredExp;
            RequiredExp += 5 * (Level + 3);
        }
    }    //한번에 레벨업됐을때 한번고려
    public void UpdateGold(int value)
    {
        if ((value > 0 && Gold > int.MaxValue - value) ||
        (value < 0 && Gold < int.MinValue - value))
        {
            Console.WriteLine("Gold값이 유효하지 않습니다.");
            return;
        }

        Gold += value;

        if (Gold < 0)
        {
            Gold = 0;
        }
    }
    public void UpdateBasicHp(float value)
    {
        if ((value > 0 && BasicHp > float.MaxValue - value) ||
        (value < 0 && BasicHp < float.MinValue - value))
        {
            Console.WriteLine("BasicHp값이 유효하지 않습니다.");
            return;
        }

        BasicHp += value;
    }
    public void UpdateAdditionalHp(float value)
    {
        if ((value > 0 && AdditionalHp > float.MaxValue - value) ||
        (value < 0 && AdditionalHp < float.MinValue - value))
        {
            Console.WriteLine("AdditionalHp값이 유효하지 않습니다.");
            return;
        }

        AdditionalHp += value;
    }
    public void UpdateCurrentHp(float value)
    {
        if ((value > 0 && CurrentHp > float.MaxValue - value) ||
        (value < 0 && CurrentHp < float.MinValue - value))
        {
            Console.WriteLine("CurrentHp값이 유효하지 않습니다.");
            return;
        }

        CurrentHp += value;

        if (CurrentHp > TotalHp)
        {
            CurrentHp = TotalHp;
        }
        else if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            KillUnit();
        }
    }
    public void UpdateBasicMp(float value)
    {
        if ((value > 0 && BasicMp > float.MaxValue - value) ||
        (value < 0 && BasicMp < float.MinValue - value))
        {
            Console.WriteLine("BasicMp값이 유효하지 않습니다.");
            return;
        }

        BasicMp += value;
    }
    public void UpdateAdditionalMp(float value)
    {
        if ((value > 0 && AdditionalMp > float.MaxValue - value) ||
        (value < 0 && AdditionalMp < float.MinValue - value))
        {
            Console.WriteLine("AdditionalMp값이 유효하지 않습니다.");
            return;
        }

        AdditionalMp += value;
    }
    public void UpdateCurrentMp(float value)
    {
        if ((value > 0 && CurrentMp > float.MaxValue - value) ||
        (value < 0 && CurrentMp < float.MinValue - value))
        {
            Console.WriteLine("CurrentMp값이 유효하지 않습니다.");
            return;
        }

        CurrentMp += value;
    } // 여기 MP가 0보다작을때 넣어주기
    public void UpdateBasicAttackPower(float value)
    {
        if ((value > 0 && BasicAttackPower > float.MaxValue - value) ||
        (value < 0 && BasicAttackPower < float.MinValue - value))
        {
            Console.WriteLine("BasicAttackPower값이 유효하지 않습니다.");
            return;
        }

        BasicAttackPower += value;
    }
    public void UpdateAdditionalAttackPower(float value)
    {
        if ((value > 0 && AdditionalAttackPower > float.MaxValue - value) ||
        (value < 0 && AdditionalAttackPower < float.MinValue - value))
        {
            Console.WriteLine("AdditionalAttackPower값이 유효하지 않습니다.");
            return;
        }

        AdditionalAttackPower += value;
    }
    public void UpdateCurrentAttackPower(float value)
    {
        if ((value > 0 && CurrentAttackPower > float.MaxValue - value) ||
        (value < 0 && CurrentAttackPower < float.MinValue - value))
        {
            Console.WriteLine("CurrentAttackPower값이 유효하지 않습니다.");
            return;
        }

        CurrentAttackPower += value;
    }
    public void UpdateBasicDefensivePower(float value)
    {
        if ((value > 0 && BasicDefensivePower > float.MaxValue - value) ||
        (value < 0 && BasicDefensivePower < float.MinValue - value))
        {
            Console.WriteLine("BasicDefensivePower값이 유효하지 않습니다.");
            return;
        }

        BasicDefensivePower += value;
    }
    public void UpdateAdditionalDefensivePower(float value)
    {
        if ((value > 0 && AdditionalDefensivePower > float.MaxValue - value) ||
        (value < 0 && AdditionalDefensivePower < float.MinValue - value))
        {
            Console.WriteLine("AdditionalDefensivePower값이 유효하지 않습니다.");
            return;
        }

        AdditionalDefensivePower += value;
    }
    public void UpdateCurrentDefensivePower(float value)
    {
        if ((value > 0 && CurrentDefensivePower > float.MaxValue - value) ||
        (value < 0 && CurrentDefensivePower < float.MinValue - value))
        {
            Console.WriteLine("CurrentDefensivePower값이 유효하지 않습니다.");
            return;
        }

        CurrentDefensivePower += value;
    }
}