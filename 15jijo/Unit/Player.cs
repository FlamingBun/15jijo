using Newtonsoft.Json.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

public class Player : Unit
{
    public override string? Name { get; protected set; }
    public override float CurrentHp { get; protected set; }
    public override float CurrentMp { get; protected set; }
    public override float CurrentAttackPower { get { return TotalAttackPower; } set { } }
    public override float CurrentDefensivePower => TotalDefensivePower;
    public override List<Skill>? AvailableSkills { get; protected set; }
    public override void KillUnit()
    {
        UpdateGold(-(int)(Gold * 0.2f));
        UpdateExp(-(int)(RequiredExp * 0.2f));
        //씬변환?
    }
    public Jobs Job { get; private set; }
    public int Level { get; private set; }
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
    public EquipmentItem? equippedAttackPowerItem { get; private set; }
    public EquipmentItem? equippedDefensivePowerItem { get; private set; }
    public EquipmentItem? equippedHpItem { get; private set; }
    public EquipmentItem? equippedMpItem { get; private set; }

    // 캐릭터 처음 생성 시 호출
    public Player(string? _inputName, Jobs _selectedJob)
    {
        Name = _inputName;
        Job = _selectedJob;
        Level = 1;
        Exp = 0;
        RequiredExp = 10;
        Gold = 50000;
        SkillGrade = 0;
        BasicHp = 100f;
        AdditionalHp = 0f;
        CurrentHp = TotalHp;
        BasicMp = 50f;
        AdditionalMp = 0f;
        CurrentMp = TotalMp;
        BasicAttackPower = 10f;
        AdditionalAttackPower = 0f;
        BasicDefensivePower = 5f;
        AdditionalDefensivePower = 0f;
        equippedAttackPowerItem = null;
        equippedDefensivePowerItem = null;
        equippedHpItem = null;
        equippedMpItem = null;
        AvailableSkills = new();
        switch (Job)
        {
            case Jobs.나영웅매니저님:
                if (GameManager.instance != null && GameManager.instance.skills != null)
                {
                    AvailableSkills.Add(GameManager.instance.skills.playerSkills[0]);
                }
                break;
            case Jobs.박찬우매니저님:
                if (GameManager.instance != null && GameManager.instance.skills != null)
                {
                    AvailableSkills.Add(GameManager.instance.skills.playerSkills[1]);
                }
                break;
            case Jobs.한효승매니저님:
                if (GameManager.instance != null && GameManager.instance.skills != null)
                {
                    AvailableSkills.Add(GameManager.instance.skills.playerSkills[2]);
                }
                break;
        }

    }

    // 저장된 플레이어 데이터가 있을 때 사용
    public Player(JObject json)
    {
        Name = json["Name"]?.ToString();
        CurrentHp = json["CurrentHp"]?.ToObject<float>() ?? 0;
        CurrentMp = json["CurrentMp"]?.ToObject<float>() ?? 0;
        AvailableSkills = json["AvailableSkills"]?.ToObject<List<Skill>>();
        Job = json["Job"]?.ToObject<Jobs>() ?? Jobs.나영웅매니저님;
        Level = json["Level"]?.ToObject<int>() ?? 1;
        Exp = json["Exp"]?.ToObject<int>() ?? 0;
        RequiredExp = json["RequiredExp"]?.ToObject<int>() ?? 10;
        SkillGrade = json["SkillGrade"]?.ToObject<int>() ?? 0;

        BasicHp = json["BasicHp"]?.ToObject<float>() ?? 0;
        AdditionalHp = json["AdditionalHp"]?.ToObject<float>() ?? 0;
        BasicMp = json["BasicMp"]?.ToObject<float>() ?? 0;
        AdditionalMp = json["AdditionalMp"]?.ToObject<float>() ?? 0;
        BasicAttackPower = json["BasicAttackPower"]?.ToObject<float>() ?? 0;
        AdditionalAttackPower = json["AdditionalAttackPower"]?.ToObject<float>() ?? 0;
        BasicDefensivePower = json["BasicDefensivePower"]?.ToObject<float>() ?? 0;
        AdditionalDefensivePower = json["AdditionalDefensivePower"]?.ToObject<float>() ?? 0;

        Gold = json["Gold"]?.ToObject<int>() ?? 0;

        equippedAttackPowerItem = json["equippedAttackPowerItem"]?.ToObject<EquipmentItem>();
        equippedDefensivePowerItem = json["equippedDefensivePowerItem"]?.ToObject<EquipmentItem>();
        equippedHpItem = json["equippedHpItem"]?.ToObject<EquipmentItem>();
        equippedMpItem = json["equippedMpItem"]?.ToObject<EquipmentItem>();
    }
    public void LevelUp()
    {
        ++Level;
        BasicAttackPower += 0.5f;
        BasicDefensivePower += 1f;
        CurrentHp = TotalHp;
        CurrentMp = TotalMp;
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

        while (Exp >= RequiredExp)
        {
            LevelUp();
            Exp -= RequiredExp;
            RequiredExp += 5 * (Level + 3);
        }
    }
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
    public bool SpendGold(int value)
    {
        if ((value > 0 && Gold > int.MaxValue - value) || (value < 0 && Gold < int.MinValue - value))
        {
            Console.WriteLine("Gold값이 유효하지 않습니다.");
            return false;
        }

        Gold -= value;

        if (Gold < 0)
        {
            Gold += value;
            return false;
        }

        return true;
    }
    public void UpdateBasicHp(float value)
    {
        if ((value > 0.0f && BasicHp > float.MaxValue - value) ||
        (value < 0.0f && BasicHp < float.MinValue - value))
        {
            Console.WriteLine("BasicHp값이 유효하지 않습니다.");
            return;
        }

        BasicHp += value;
    }
    public void UpdateAdditionalHp(float value)
    {
        if ((value > 0.0f && AdditionalHp > float.MaxValue - value) ||
        (value < 0.0f && AdditionalHp < float.MinValue - value))
        {
            Console.WriteLine("AdditionalHp값이 유효하지 않습니다.");
            return;
        }

        AdditionalHp += value;
    }
    public void Heal(float value)
    {
        if ((value > 0.0f && CurrentHp > float.MaxValue - value) ||
        (value < 0.0f && CurrentHp < float.MinValue - value))
        {
            Console.WriteLine("CurrentHp값이 유효하지 않습니다.");
            return;
        }

        CurrentHp += value;

        if (CurrentHp > TotalHp)
        {
            CurrentHp = TotalHp;
        }
    }
    public void UpdateBasicMp(float value)
    {
        if ((value > 0.0f && BasicMp > float.MaxValue - value) ||
        (value < 0.0f && BasicMp < float.MinValue - value))
        {
            Console.WriteLine("BasicMp값이 유효하지 않습니다.");
            return;
        }

        BasicMp += value;
    }
    public void UpdateAdditionalMp(float value)
    {
        if ((value > 0.0f && AdditionalMp > float.MaxValue - value) ||
        (value < 0.0f && AdditionalMp < float.MinValue - value))
        {
            Console.WriteLine("AdditionalMp값이 유효하지 않습니다.");
            return;
        }

        AdditionalMp += value;
    }
    public bool ConsumedMp(float value)
    {
        if ((value > 0.0f && CurrentMp > float.MaxValue - value) ||
        (value < 0.0f && CurrentMp < float.MinValue - value))
        {
            Console.WriteLine("CurrentMp값이 유효하지 않습니다.");
            return false;
        }

        CurrentMp -= value;

        if (CurrentMp < 0.0f)
        {
            CurrentMp += value;
            return false;
        }

        return true;
    }
    public void RecoveryMp(float value)
    {
        if ((value > 0.0f && CurrentMp > float.MaxValue - value) ||
        (value < 0.0f && CurrentMp < float.MinValue - value))
        {
            Console.WriteLine("CurrentMp값이 유효하지 않습니다.");
            return;
        }

        CurrentMp += value;

        if (CurrentMp > TotalMp)
        {
            CurrentMp = TotalMp;
        }
    }
    public void UpdateBasicAttackPower(float value)
    {
        if ((value > 0.0f && BasicAttackPower > float.MaxValue - value) ||
        (value < 0.0f && BasicAttackPower < float.MinValue - value))
        {
            Console.WriteLine("BasicAttackPower값이 유효하지 않습니다.");
            return;
        }

        BasicAttackPower += value;
    }
    public void UpdateAdditionalAttackPower(float value)
    {
        if ((value > 0.0f && AdditionalAttackPower > float.MaxValue - value) ||
        (value < 0.0f && AdditionalAttackPower < float.MinValue - value))
        {
            Console.WriteLine("AdditionalAttackPower값이 유효하지 않습니다.");
            return;
        }

        AdditionalAttackPower += value;
    }
    //public void UpdateCurrentAttackPower(float value)
    //{
    //    if ((value > 0.0f && CurrentAttackPower > float.MaxValue - value) ||
    //    (value < 0.0f && CurrentAttackPower < float.MinValue - value))
    //    {
    //        Console.WriteLine("CurrentAttackPower값이 유효하지 않습니다.");
    //        return;
    //    }

    //    CurrentAttackPower += value;
    //} 버프스킬용
    public void UpdateBasicDefensivePower(float value)
    {
        if ((value > 0.0f && BasicDefensivePower > float.MaxValue - value) ||
        (value < 0.0f && BasicDefensivePower < float.MinValue - value))
        {
            Console.WriteLine("BasicDefensivePower값이 유효하지 않습니다.");
            return;
        }

        BasicDefensivePower += value;
    }
    public void UpdateAdditionalDefensivePower(float value)
    {
        if ((value > 0.0f && AdditionalDefensivePower > float.MaxValue - value) ||
        (value < 0.0f && AdditionalDefensivePower < float.MinValue - value))
        {
            Console.WriteLine("AdditionalDefensivePower값이 유효하지 않습니다.");
            return;
        }

        AdditionalDefensivePower += value;
    }
    //public void UpdateCurrentDefensivePower(float value)
    //{
    //    if ((value > 0.0f && CurrentDefensivePower > float.MaxValue - value) ||
    //    (value < 0.0f && CurrentDefensivePower < float.MinValue - value))
    //    {
    //        Console.WriteLine("CurrentDefensivePower값이 유효하지 않습니다.");
    //        return;
    //    }

    //    float tempBuffedAttack = CurrentAttackPower * 1.2f;
    //} 버프스킬용

    public void OnWeapon(EquipmentItem equipmentItem)
    {
        if (this.equippedAttackPowerItem != null)
        {
            OffWeapon(this.equippedAttackPowerItem);
        }
        this.equippedAttackPowerItem = equipmentItem;
        this.AdditionalAttackPower += equipmentItem.ItemAbility;
    }
    public void OnArmor(EquipmentItem equipmentItem)
    {
        if (this.equippedDefensivePowerItem != null)
        {
            OffWeapon(this.equippedDefensivePowerItem);
        }
        this.equippedDefensivePowerItem = equipmentItem;
        this.AdditionalDefensivePower += equipmentItem.ItemAbility;
    }
    public void OffWeapon(EquipmentItem equipmentItem)
    {
        this.equippedAttackPowerItem = null;
        this.AdditionalAttackPower -= equipmentItem.ItemAbility;
    }
    public void OffArmor(EquipmentItem equipmentItem)
    {
        this.equippedDefensivePowerItem = null;
        this.AdditionalDefensivePower -= equipmentItem.ItemAbility;
    }
    public void SetEquippedAttackPowerItem(EquipmentItem item)
    {
        equippedAttackPowerItem = item;
    }
    public void SetEquippedDefensivePowerItem(EquipmentItem item)
    {
        equippedDefensivePowerItem = item;
    }
}