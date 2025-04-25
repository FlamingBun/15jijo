
public class Monster : Unit
{
    public override string? Name { get; protected set; }
    public override float CurrentHp { get; protected set; }
    public override float CurrentMp { get; protected set; }
    public override float CurrentAttackPower { get; protected set; }
    public override float CurrentDefensivePower { get; protected set; }
    public override List<Skill>? AvailableSkills { get; protected set; }

    public float TotalHp { get; private set; }
    public float TotalMp { get; private set; }

    public bool isDead = false;

    public Monster(string _name, float _totalHP, float _totalMP, float _attackPower, float _defensivePower) 
    {
        Name = _name;

        TotalHp = _totalHP;
        CurrentHp = TotalHp;

        TotalMp = _totalMP;
        CurrentMp = TotalMp;

        CurrentAttackPower = _attackPower;
        CurrentDefensivePower = _defensivePower;
    }


    public override void KillUnit()
    {
        throw new NotImplementedException();
    }
}

