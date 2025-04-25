public class ConsumeItem:Item
{
    public ConsumeItemType ConsumeItemType { get; protected set; }

    public ConsumeItem(string itemName, ItemType itemType, ConsumeItemType consumeItemType, int itemAbility, string itemDescription, int itemPrice)
    {
        ItemName = itemName;
        ItemType = itemType;
        ConsumeItemType = consumeItemType;
        ItemAbility = itemAbility;
        ItemDescription = itemDescription;
        ItemPrice = itemPrice;
    }
}
