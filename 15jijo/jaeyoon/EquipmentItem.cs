public class EquipmentItem : Item
{
    public override string? ItemName { get; protected set; }
    public override ItemType ItemType { get; protected set; }
    public override int ItemAbility { get; protected set; }
    public override string? ItemDescription { get; protected set; }
    public override int ItemPrice { get; protected set; }
    public EquipmentItemType EquipmentItemType { get; protected set; }
    public EquipmentItem(string? itemName, ItemType itemType, EquipmentItemType equipmentItemType, int itemAbility, string? itemDescription, int itemPrice)
    {
        ItemName = itemName;
        ItemType = itemType;
        EquipmentItemType = equipmentItemType;
        ItemAbility = itemAbility;
        ItemDescription = itemDescription;
        ItemPrice = itemPrice;
    }
}