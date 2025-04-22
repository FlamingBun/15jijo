public abstract class Unit
{
    public abstract string Name { get; protected set; }
    public abstract int Level { get; protected set; }
    public abstract int SkillGrade { get; protected set; }
    public abstract float BasicHp { get; protected set; }
    public abstract float BasicMp { get; protected set; }
    public abstract float BasicAttackPower { get; protected set; }
    public abstract List<Skill> AvailableSkills { get; protected set; }
    public abstract void TakeDamage();
    public abstract void KillUnit();
    public abstract void GetSkill();
}
