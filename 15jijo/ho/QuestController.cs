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
        { 
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

        public void DisplayQuestListName() // 퀘스트 리스트의 이름만 출력하는 메서드
        {
            foreach (var quest in questList.Select((value,index)=>(value,index)))
            {
                Console.Write($"{quest.index+1}. ");
                quest.value.ShowQuestName();
            }

        }


        public void DisplayQuest(Quest quest) // 퀘스트 하나를 출력하는 메서드
        {
            
               quest.ShowQuestName();
               quest.ShowQuest();
        }

        public void DisplayQuest(int index) // 퀘스트 하나를 출력하는 메서드
        {

            questList[index].ShowQuestName();
            questList[index].ShowQuest();
        }

        public int GetQuestCount() // 진행 가능한 퀘스트들의 개수를 리턴
        {
            return questList.Count;
        }

        



        public void CompleteQuest()
        {
            //퀘스트 클리어 로직
        }



    }
}
