using System;

namespace TextRPG
{
    public enum ItemType
    {
        Weapon,
        Armor
    }

    public class Item
    {
        public string Name;
        public ItemType Type;
        public int Attack;
        public int Defense;
        public string Description;
        public int Price;        // 상점에서 구매할 때 필요한 금액
        public bool IsPurchased; // 상점에서 구매 완료 여부
        public bool IsEquipped;  // 인벤토리에서 장착 여부

        public Item(string name, ItemType type, int attack, int defense,
                    string desc, int price = 0,
                    bool purchased = false, bool equipped = false)
        {
            Name = name;
            Type = type;
            Attack = attack;
            Defense = defense;
            Description = desc;
            Price = price;
            IsPurchased = purchased;
            IsEquipped = equipped;
        }
    }
}
