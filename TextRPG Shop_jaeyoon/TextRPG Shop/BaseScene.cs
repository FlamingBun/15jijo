using System;

namespace TextRPG
{
    public enum SceneType
    {
        None,
        Inventory,   // 인벤토리
        ItemUse,     // 아이템 사용
        Shop,        // 상점
        Buy,         // 아이템 구매
        Sell         // 아이템 판매
    }

    /// <summary>
    /// 모든 Scene이 상속받을 추상 클래스
    /// </summary>
    public abstract class BaseScene
    {
        // Scene마다 UI를 갱신(출력)할 때 호출
        public abstract void Render();

        // Scene마다 사용자 입력을 처리할 때 호출
        public abstract void InputHandle();
    }
}
