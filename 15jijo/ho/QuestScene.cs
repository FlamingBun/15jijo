using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15jijo.ho
{
    public class QuestScene : BaseScene
    {
        

        public override SceneState SceneState { get; protected set; } = SceneState.Quest;

        public override SceneState InputHandle()
        {
            DrawScene(SceneState);
            //selectionCount = GameManager.instance.questController.QuestList.Count + 1;
            GameManager.instance.questController.DisplayQuest();




            return SceneState;
        }
    }
}
