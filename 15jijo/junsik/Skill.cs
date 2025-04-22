public class Skill
{
    private int Job { get; }
    private int Grade { get; }
    private string Name { get; }
    private string Description { get; }
    private float Damage { get; }
    private int ConsumedMp { get; }

    private Skill(Jobs jobs, int grade, string name, string description, float damage, int consumedMp)
    {
        Job = (int)jobs;
        Grade = grade;
        Name = name;
        Description = description;
        Damage = damage;
        ConsumedMp = consumedMp;
    }

    private List<Skill> list = new List<Skill>()
    {
        new Skill(Jobs.전사, 0, "알파 스트라이크", "공격력 * 2 로 하나의 적을 공격합니다.", 1 * 2, 10),//player.BasicAttackPower * 2
        new Skill(Jobs.궁수, 0, "파이어 샷", "공격력 * 2 로 하나의 적을 공격합니다.", 1 * 2, 10),
        new Skill(Jobs.마법사, 0, "에너지 볼", "공격력 * 2 로 하나의 적을 공격합니다.", 1 * 2, 10)
    };
}