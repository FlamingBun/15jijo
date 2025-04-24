namespace TextRPG
{
    /// <summary>
    /// Scene 전환을 관리
    /// </summary>
    public static class SceneManager
    {
        private static BaseScene currentScene = null;
        public static SceneType currentSceneType = SceneType.None;

        public static void ChangeScene(SceneType next)
        {
            // 씬 전환
            currentSceneType = next;

            // 현재 Scene 객체를 새 Scene 객체로 교체
            switch (next)
            {
                case SceneType.Inventory:
                    currentScene = new SceneInventory();
                    break;
                case SceneType.ItemUse:
                    currentScene = new SceneItemUse();
                    break;
                case SceneType.Shop:
                    currentScene = new SceneShop();
                    break;
                case SceneType.Buy:
                    currentScene = new SceneBuy();
                    break;
                case SceneType.Sell:
                    currentScene = new SceneSell();
                    break;
                default:
                    currentScene = null;
                    break;
            }

            if (currentScene != null)
            {
                // 씬에 진입하자마자 한 번 화면을 출력
                currentScene.Render();
            }
        }

        /// <summary>
        /// 현재 씬의 InputHandle()을 호출
        /// </summary>
        public static void ProcessInput()
        {
            if (currentScene != null)
            {
                currentScene.InputHandle();
            }
        }
    }
}
