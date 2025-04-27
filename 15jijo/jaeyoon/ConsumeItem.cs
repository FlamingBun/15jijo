public class ConsumeItem : Item
{
    public override string? ItemName { get; protected set; }
    public override ItemType ItemType { get; protected set; }
    public override int ItemAbility { get; protected set; }
    public override string? ItemDescription { get; protected set; }
    public override int ItemPrice { get; protected set; }
    public int ItemCount { get; protected set; }
    public ConsumeItemType ConsumeItemType { get; protected set; }
    public ConsumeItem(string? itemName, ItemType itemType, ConsumeItemType consumeItemType, int itemAbility, string? itemDescription, int itemPrice)
    {
        ItemName = itemName;
        ItemType = itemType;
        ConsumeItemType = consumeItemType;
        ItemAbility = itemAbility;
        ItemDescription = itemDescription;
        ItemPrice = itemPrice;
    }
    public void Use()
    {
        if (this.ItemCount == 1)
        {
            GameManager.instance.havingItems.Remove(this);
        }
        else
        {
            --this.ItemCount;
        }
        GameManager.instance.player.Heal((float)this.ItemAbility);
    }
    public void Buy()
    {
        ++this.ItemCount;
    }
    public void Sell()
    {
        --this.ItemCount;
    }
}