public class Skills
{
    public List<Skill> playerSkills = new()
    {
        new Skill(Jobs.전사, 0, "알파 스트라이크", "공격력 * 2 로 하나의 적을 공격합니다.", 1 * 2.0f, 10.0f),
        new Skill(Jobs.궁수, 0, "파이어 샷", "공격력 * 2 로 하나의 적을 공격합니다.", 1 * 2.0f, 10.0f),
        new Skill(Jobs.법사, 0, "에너지 볼", "공격력 * 2 로 하나의 적을 공격합니다.", 1 * 2.0f, 10.0f)
    };

    public List<Skill> bossSkills = new List<Skill>()
    {

    };
}