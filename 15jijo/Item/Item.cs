public abstract class Item
{
    public abstract string? ItemName { get; protected set; }
    public abstract ItemType ItemType { get; protected set; }
    public abstract int ItemAbility { get; protected set; }
    public abstract string? ItemDescription { get; protected set; }
    public abstract int ItemPrice { get; protected set; }
}