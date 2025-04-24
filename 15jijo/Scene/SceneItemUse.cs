using System;


    public class SceneItemUse : BaseScene
    {
        public override SceneState SceneState { get; protected set; } = SceneState.ItemUse;

        public override SceneState InputHandle()
        {
            DrawScene(SceneState);

            Console.WriteLine("===== [아이템 사용] =====");
            Console.WriteLine("(체력 회복 등 가정)\n");

            var inventory = GameManager.player.Inventory;
            for(int i=0; i<inventory.Count; i++)
            {
                Console.WriteLine($"{i+1}. {inventory[i].Name} ({inventory[i].Description})");
            }
            Console.WriteLine("\n0. 인벤토리로 돌아가기\n");

            selectionCount = inventory.Count; // 0 ~ 인벤토리 갯수
            Console.Write("사용할 아이템 번호: ");
            string input = Console.ReadLine();

            int num=-1;
            bool valid = ConsoleHelper.CheckUserInput(input, selectionCount, ref num);
            if(!valid)
                return SceneState.ItemUse;
            if(num == 0)
                return SceneState.Inventory;

            // 예시: 아이템 사용 -> 체력+10
            var selItem = inventory[num-1];
            Console.WriteLine($"'{selItem.Name}' 을(를) 사용 -> 체력 10 회복!");
            GameManager.player.Health += 10;
            Console.WriteLine($"현재 체력: {GameManager.player.Health}");

            Console.WriteLine("\n계속하려면 엔터...");
            Console.ReadLine();
            return SceneState.ItemUse;
        }
    }

