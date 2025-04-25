public class Skill
{
    public Jobs Job { get; private set; }
    public int Grade { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public float Damage { get; private set; }
    public float ConsumedMp { get; private set; }

    public Skill(Jobs jobs, int grade, string name, string description, float damage, float consumedMp)
    {
        Job = jobs;
        Grade = grade;
        Name = name;
        Description = description;
        Damage = damage;
        ConsumedMp = consumedMp;
    }
}