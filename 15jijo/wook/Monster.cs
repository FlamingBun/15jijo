
public class Monster : Unit
{
    public override string? Name { get; protected set; }
    public override float CurrentHp { get; protected set; }
    public override float CurrentMp { get; protected set; }
    public override float CurrentAttackPower => TotalAttackPower;
    public override float CurrentDefensivePower => TotalDefensivePower;

    public float TotalAttackPower;
    public float TotalDefensivePower;
    public float TotalHp { get; private set; }
    public float TotalMp { get; private set; }

    public bool isDead = false;
    public override List<Skill>? AvailableSkills { get; protected set; }

    public Monster(string _name, float _totalHP, float _totalMP, float _attackPower, float _defensivePower) 
    {
        Name = _name;

        TotalHp = _totalHP;
        CurrentHp = TotalHp;

        TotalMp = _totalMP;
        CurrentMp = TotalMp;

        TotalAttackPower = _attackPower;
        TotalDefensivePower = _defensivePower;
    }


    public override void KillUnit()
    {
        throw new NotImplementedException();
    }
}

