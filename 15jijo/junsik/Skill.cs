public class Skill
{
    public Jobs Job { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public float ConsumedMp { get; private set; }

    public Skill(Jobs jobs, string name, string description, float consumedMp)
    {
        Job = jobs;
        Name = name;
        Description = description;
        ConsumedMp = consumedMp;
    }
}