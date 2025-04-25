public class EquipmentItem: Item    
{
    public EquipmentItemType EquipmentItemType { get; protected set; }
    public EquipmentItem(string itemName, ItemType itemType, EquipmentItemType equipmentItemType, int itemAbility, string itemDescription, int itemPrice)
    {
        ItemName = itemName;
        ItemType = itemType;
        EquipmentItemType = equipmentItemType;
        ItemAbility = itemAbility;
        ItemDescription = itemDescription;
        ItemPrice = itemPrice;
    }
}

