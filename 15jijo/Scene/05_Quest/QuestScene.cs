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
            qc.ConnectPlayer(GameManager.instance.player);
            qc.UpdateQuest();
            selectionCount = qc.GetQuestCount() + 1;
            int inputNumber = -1;
            if (qc.selectedQuestIndex == -1)
            {
                DrawScene(SceneState);
                string? input = Console.ReadLine();
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
                    Console.Clear();
                    if (qc.selectedQuestIndex == -1)
                    {
                        qc.selectedQuestIndex = inputNumber - 1; // 퀘스트 인덱스 설정
                    }

                    qc.DisplayQuest(qc.selectedQuestIndex); // 퀘스트 출력

                    input = Console.ReadLine();
                    isValidInput = ConsoleHelper.CheckUserInput(input, 1, ref inputNumber);

                    if (!isValidInput)
                    {
                        return SceneState;
                    }
                    else if (inputNumber == 0) // 나가기
                    {

                        qc.selectedQuestIndex = -1;
                        return SceneState;
                    }
                    else // 퀘스트 수락
                    {
                        Console.Clear();
                        qc.DisplayQuest(qc.selectedQuestIndex); // 퀘스트 출력
                        qc.ReceiveQuest(qc.selectedQuestIndex); // 퀘스트 수락
                        return SceneState; // 퀘스트 화면으로 돌아가기
                    }

                }
            }
            else
            {   Console.Clear();
                qc.DisplayQuest(qc.selectedQuestIndex); // 퀘스트 출력
                string? input = Console.ReadLine();
                bool isValidInput = ConsoleHelper.CheckUserInput(input, 1, ref inputNumber);

                if (!isValidInput)
                {
                    return SceneState;
                }
                else if (inputNumber == 0) // 나가기
                {

                    qc.selectedQuestIndex = -1;
                    return SceneState;
                }
                else // 퀘스트 수락
                {
                    Console.Clear();
                    qc.DisplayQuest(qc.selectedQuestIndex); // 퀘스트 출력
                    qc.ReceiveQuest(qc.selectedQuestIndex); // 퀘스트 수락
                    return SceneState; // 퀘스트 화면으로 돌아가기
                }


            }




        }
    }
}
