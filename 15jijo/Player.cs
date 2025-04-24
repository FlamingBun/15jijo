using System;
using System.Collections.Generic;


    public class Player
    {
        public string Name = "Chad";
        public string Job = "전사";
        public int Level = 1;

        // 기본 능력치
        public int BaseAttack = 10;
        public int BaseDefense = 10;
        public int Health = 100;

        // 보유 Gold
        public int Gold = 1500;

        // 플레이어가 소유한 아이템 목록(인벤토리)
        public List<Item> Inventory = new List<Item>();

        // 현재 착용 중인 아이템들의 능력치를 합산해서 반환
        public int GetTotalAttack()
        {
            int totalAttack = BaseAttack;
            foreach (var item in Inventory)
            {
                if (item.IsEquipped)
                {
                    totalAttack += item.Attack;
                }
            }
            return totalAttack;
        }

        public int GetTotalDefense()
        {
            int totalDefense = BaseDefense;
            foreach (var item in Inventory)
            {
                if (item.IsEquipped)
                {
                    totalDefense += item.Defense;
                }
            }
            return totalDefense;
        }
    }

