using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15jijo.ho
{
    public class QuestScene : BaseScene
    {
        QuestController qc = GameManager.instance.questController;


        public override SceneState SceneState { get; protected set; } = SceneState.Quest;

        public override SceneState InputHandle()
        {
            DrawScene(SceneState);
            selectionCount = qc.GetQuestCount() + 1;
            string? input = Console.ReadLine();
            int inputNumber = -1;
            bool isValidInput = ConsoleHelper.CheckUserInput(input, selectionCount, ref inputNumber);
            if (!isValidInput)
            {
                return SceneState;
            }
            else if (inputNumber == 0) 
            {
                return SceneState.Main;


            }
            else
            {





                return SceneState;
            }



                
        }
    }
}
