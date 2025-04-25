public class Items
{
    public List<Item> all = new()    // 존재하는 모든 아이템
    {
            new Item("수련자 갑옷", ItemValue.DefensivePower, 5, "수련에 도움을 주는 갑옷입니다.", 1000),
            new Item("무쇠갑옷", ItemValue.DefensivePower, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000),
            new Item("스파르타의 갑옷", ItemValue.DefensivePower, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500),
            new Item("낡은 검", ItemValue.AttackPower, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600),
            new Item("청동 도끼", ItemValue.AttackPower, 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500),
            new Item("스파르타의 창", ItemValue.AttackPower, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3100),
            new Item("엑스칼리버", ItemValue.AttackPower, 1000, "전설로만 내려오는 영웅 아서왕의 성검이다.", 1234557890),
            new Item("아테나의 방패", ItemValue.DefensivePower, 1000, "전설로만 내려오는 여신 아테나의 방패이다.", 1234557890),
            new Item("워모그의 심장", ItemValue.Hp, 1000, "리그오브레전드에 등장하는 아이템이다.", 1234557890)
    };

    public List<Item> shop = new()    // shop에서만 파는 아이템
    {
        new Item("수련자 갑옷", ItemValue.DefensivePower, 5, "수련에 도움을 주는 갑옷입니다.", 1000),
            new Item("무쇠갑옷", ItemValue.DefensivePower, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000),
            new Item("스파르타의 갑옷", ItemValue.DefensivePower, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500),
            new Item("낡은 검", ItemValue.AttackPower, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600),
            new Item("청동 도끼", ItemValue.AttackPower, 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500),
            new Item("스파르타의 창", ItemValue.AttackPower, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3100),
            new Item("엑스칼리버", ItemValue.AttackPower, 1000, "전설로만 내려오는 영웅 아서왕의 성검이다.", 1234557890),
            new Item("아테나의 방패", ItemValue.DefensivePower, 1000, "전설로만 내려오는 여신 아테나의 방패이다.", 1234557890),
            new Item("워모그의 심장", ItemValue.Hp, 1000, "리그오브레전드에 등장하는 아이템이다.", 1234557890)
    };
}