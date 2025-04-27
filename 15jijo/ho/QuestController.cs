using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15jijo.ho
{
    public class QuestController //퀘스트 씬 출력 및 퀘스트 데이터베이스 세팅 담당 클래스 
    {
        public int selectedQuestIndex = -1; // 퀘스트씬에서 선택되는 퀘스트 
        private List<Quest> questList; // 퀘스트들을 담고있는 Quest배열
        private Monster[]? targetMonsterDB; // 몬스터 DB에서 targetMonster를 랜덤으로 지정
        private List<Quest>? playerReceiveQuest; // 플레이어가 받은 퀘스트 목록
        private string[]? statType; // 플레이어의 능력치를 올리는 퀘스트에 사용되는 능력치 타입


        public QuestController() // 퀘스트 컨트롤러 생성자 
        {
            statType = new string[] { "공격력", "방어력", "레벨" };
            //targetMonsterDB = ;
            questList = SetQuestDB();
            playerReceiveQuest = new List<Quest>();

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

        public void UpdateQuest()
        {
            foreach ( var quest in questList)
            {   
                quest.makeQuestCore();
            }
            foreach (var quest in playerReceiveQuest)
            {
                quest.CompleteQuest(); // 퀘스트 완료 체크
            }
        }
        
        public void ConnectPlayer(Player player)
        {
            foreach (var quest in questList)
            {
                if (quest.GetType() == typeof(PlayerStatQuest))
                {
                    ((PlayerStatQuest)quest).player = player; // 퀘스트에 플레이어 연결
                }
            }
        }

        public void DisplayQuest(Quest quest) // 퀘스트 하나를 출력하는 메서드
        {
            
            quest.ShowQuest();
            if (quest.IsClear)
            {
                Console.WriteLine("1. 퀘스트 완료\n0.나가기");
            }// 퀘스트가 클리어가 아니라면
            else if (quest.IsReceive) // 퀘스트가 진행 중이라면
            {
                Console.WriteLine("0. 나가기");
            }
            else// 퀘스트를 진행 중이지 않다면
            {
                Console.WriteLine("1. 퀘스트 수락\n0. 나가기");
            }
        }

        public void DisplayQuest(int index) // 퀘스트 하나를 출력하는 메서드
        {

            DisplayQuest(questList[index]);
        }

        public int GetQuestCount() // 진행 가능한 퀘스트들의 개수를 리턴
        {
            return questList.Count;
        }

        public void ReceiveQuest(int index)
        {
            //퀘스트 수락 로직
            if (questList[index].IsClear == false) // 퀘스트가 클리어가 아니라면
            {
                playerReceiveQuest.Add(questList[index]);
                questList[index].IsReceive = true; // 퀘스트 수락
            }
            else
            {
               CompleteQuest(questList[index]); // 퀘스트 완료
            }
        }

        
        public void FindMonsterObjectInPlayerQuest(string mstName)
        {
            foreach (Quest quest in playerReceiveQuest)
            {
                if (quest.GetType() == typeof(KillMonsterQuest))
                {
                    ((KillMonsterQuest)quest).CheckTargetMonster(mstName); // 퀘스트에서 몬스터를 찾는 메서드
                }
            }
        }


        public void CompleteQuest(Quest quest)
        {
            if (quest.IsClear)
            {
                GameManager.instance.player.UpdateExp(quest.Reward_Exp); // 플레이어 경험치 획득
                GameManager.instance.player.UpdateGold(quest.Reward_Gold); // 플레이어 골드 획득
                GameManager.instance.havingItems.AddRange(quest.Reward_Items); // 플레이어 아이템 획득
                playerReceiveQuest.Remove(quest); // 퀘스트 삭제
                GameManager.instance.questController.questList.Remove(quest); // 퀘스트 삭제
                selectedQuestIndex = -1; // 퀘스트 인덱스 초기화

            }
        }



    }
}
