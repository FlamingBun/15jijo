using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15jijo.ho
{
    public class QuestController //퀘스트 씬 출력 및 퀘스트 데이터베이스 세팅 담당 클래스 
    {

        private List<Quest> questList; // 퀘스트들을 담고있는 Quest배열
        private Monster[]? targetMonsterDB; // 몬스터 DB에서 targetMonster를 랜덤으로 지정
        private List<Quest>? playerReceiveQuest; // 플레이어가 받은 퀘스트 목록
        private string[]? statType; // 플레이어의 능력치를 올리는 퀘스트에 사용되는 능력치 타입


        public QuestController() // 퀘스트 컨트롤러 생성자 
        {
            statType = new string[] { "공격력", "방어력", "레벨" };
            //targetMonsterDB = ;
            questList = SetQuestDB();

        }

        static List<Quest> SetQuestDB()
        {   /*
                 퀘스트 타입을 랜덤으로 정하여 퀘스트배열 반환하는 메서드
                Quest[] questList = new Quest[6];
                Random rand = new Random();
                for (int i = 0; i < questList.Length; i++)
                {   
                    int r = rand.Next(0,System.Int32.MaxValue);
                    
                }
                
                return questList;
                */
            List<Quest> questList = new List<Quest>();
            foreach (KillMonsterQuest quest in DataManager.instance.monsterQuest.GetDatas())
            {
                questList.Add(quest);
            }
            foreach (PlayerStatQuest quest in DataManager.instance.statQuest.GetDatas())
            {
                quest.player = GameManager.instance.player;
                questList.Add(quest);
            }
            return questList;
        }

        public void DisplayQuestListName()
        {
            foreach (var quest in questList.Select((value,index)=>(value,index)))
            {
                Console.Write($"{quest.index+1}. ");
                quest.value.ShowQuestName();
            }

        }


        public void DisplayQuest(List<Quest> _questList) // 퀘스트 리스트를 출력하는 메서드
        {
            foreach (Quest q in _questList)
            {
                DisplayQuest(q);
            }
        }

        public void DisplayQuest(Quest quest) // 퀘스트 리스트를 출력하는 메서드
        {
            
               quest.ShowQuestName();
               quest.ShowQuest();
        }

        public int GetQuestCount()
        {
            return questList.Count;
        }


        /*
         퀘스트 type에 따라 랜덤생성하는 메서드 
        Quest GenerateQuest(questType type)
        {   Random rand = new Random();
            switch (type)
            {
                case questType.killMonsterQuest:
                    int i = rand.Next(0, targetMonsterDB.Length);
                    new KillMonsterQuest($"내일배움캠프를 위협하는 {targetMonsterDB[i].NAME} 처치", );
                    break;



            }


            return Quest 

        }
        */
        public void CompleteQuest()
        {
            //퀘스트 클리어 로직
        }



    }
}
