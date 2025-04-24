using System;
using System.Collections.Generic;

namespace TextRPG
{
    public class Shop
    {
        // 상점에서 판매(또는 이미 구매 완료)하는 아이템 목록
        public List<Item> ShopItems = new List<Item>();

        public Shop()
        {
            // 예시상 "구매완료" 상태를 반영하여 아이템 초기화
            ShopItems.Add(new Item("수련자 갑옷", ItemType.Armor, 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000, purchased: false));
            ShopItems.Add(new Item("무쇠갑옷", ItemType.Armor, 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, purchased: true)); // 구매완료
            ShopItems.Add(new Item("스파르타의 갑옷", ItemType.Armor, 0, 15, "스파르타 전설의 갑옷입니다.", 3500, purchased: false));
            ShopItems.Add(new Item("낡은 검", ItemType.Weapon, 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600, purchased: false));
            ShopItems.Add(new Item("청동 도끼", ItemType.Weapon, 5, 0, "어디선가 사용됐던 도끼입니다.", 1500, purchased: false));
            ShopItems.Add(new Item("스파르타의 창", ItemType.Weapon, 7, 0, "스파르타의 전설적인 창입니다.", 0, purchased: true)); // 구매완료
        }
    }
}
