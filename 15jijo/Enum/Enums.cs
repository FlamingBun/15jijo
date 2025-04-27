public enum SceneState
{
    Status = 1,
    Inventory,
    Shop,
    Relax,
    Quest,
    DungeonEntrance,
    SaveGame,
    ExitGame,
    MakeCharacter,
    Main,
    SelectJob,
    Fitting,
    Eating,
    Buying,
    Selling,
    Battle,
}
public enum questType
{
    killMonsterQuest = 0,
    overStatQuest,

}

public enum questRequireStatName
{
    공격력 = 0,
    방어력,
    레벨

}

public enum Jobs
{
    나영웅매니저님 = 1,
    박찬우매니저님,
    한효승매니저님
}

public enum ItemType
{
    Consumable,
    Equipment
}

public enum EquipmentItemType
{
    Armor,
    Weapon
}

public enum ConsumeItemType
{
    HP,
    MP
}

public enum BuyResult
{
    AlreadyPurchased,
    NotEnoughGold,
    Success
}