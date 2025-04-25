public enum SceneState 
{
    Status =1,
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
    DungeonFight,
    
    //jaeyoon
    ItemUse,
    Buy,
    Sell
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