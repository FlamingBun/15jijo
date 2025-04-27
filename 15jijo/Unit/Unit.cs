public abstract class Unit
{
    public abstract string? Name { get; protected set; }
    public abstract float CurrentHp { get; protected set; }
    public abstract float CurrentMp { get; protected set; }
    public abstract float CurrentAttackPower { get; set; }
    public abstract float CurrentDefensivePower { get; }
    public abstract List<Skill>? AvailableSkills { get; protected set; }

    public abstract void KillUnit();
    public void TakeDamage(float currentAttackPower)
    {
        if ((currentAttackPower > 0.0f && CurrentHp > float.MaxValue - currentAttackPower) ||
        (currentAttackPower < 0.0f && CurrentHp < float.MinValue - currentAttackPower))
        {
            Console.WriteLine("currentAttackPower값이 유효하지 않습니다.");
            return;
        }

        CurrentHp -= currentAttackPower;
        // 방어력만큼 덜깎이게

        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            KillUnit();
        }
    }
}