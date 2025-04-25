using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15jijo.ho
{
    public class QuestScene : BaseScene
    {
        QuestController qc = new QuestController();

        public override SceneState SceneState { get; protected set; } = SceneState.Quest;

        public override SceneState InputHandle()
        {
            DrawScene(SceneState);
            



            return SceneState;
        }
    }
}
