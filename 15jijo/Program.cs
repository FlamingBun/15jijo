using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml.Linq;
namespace _15jijo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            //gameManager.Init();
            gameManager.GameStart();
            
        }

       

     

        enum questType
        {
            killMonsterQuest = 0,
            overStatQuest,
            
        }

        enum questRequireStatName
        {
            공격력 = 0,
            방어력,
            레벨

        }
        abstract class Quest
        {
            protected string? QuestName { get; set; } // 퀘스트 이름
            protected string? QuestDesc { get; set; } // 퀘스트 설명
            protected int Reward_Gold { get; set; } // 보상 골드
            protected float Reward_Exp { get; set; } // 보상 경험치
            protected List<Item>? Reward_Items { get; set; } // 보상 아이템
            public bool IsClear { get; set; } = false;
            public bool IsReceive { get; set; } = false;
            protected string? questCore;

            public abstract void CompleteQuest(); // 퀘스트 유형 별 퀘스트 완료 로직 구현을 오버라이딩 할 추상 메서드
            public abstract void makeQuestCore(); // 퀘스트 유형 별 퀘스트 핵심 출력 로직 구현을 오버라이딩 할 추상 메서드 

            public Quest(string name, string desc, int gold, float exp,List<Item>items)
            {
                this.QuestName = name;
                this.QuestDesc = desc;
                this.Reward_Gold = gold;
                this.Reward_Exp = exp;
                this.Reward_Items = items;
            }

            public virtual void ShowQuestName()
            {
                string? currentQuestState = "";
                if (IsClear)
                {
                    currentQuestState += "(완료)";
                }
                else if (IsReceive)
                {
                    currentQuestState += "(진행중)";
                }
                else
                {
                    // 받은 상태가 아니면 퀘스트 이름 옆에 추가할 string이 있을까?
                }

                Print.print($"{QuestName} {currentQuestState}\n");
            }

            public virtual void ShowQuest()
            {
                ShowQuestName();
                Print.print($"{QuestDesc}\n");
                Print.print(questCore);// 퀘스트 핵심 내용
                Print.print("-보상-");
                int rewardItemCount = Reward_Items == null ? 0 : Reward_Items.Count;
                if (rewardItemCount > 0)
                {
                    
                    //만약 보상 아이템이 존재하면 출력!
                }
                Print.print($"{Reward_Gold} G \n{Reward_Exp} exp\n");
            }
        }

        class KillMonsterQuest : Quest // 몬스터를 잡는 유형 -사냥 퀘스트- 의 퀘스트 구현
        {
            private int killCount = 0;
            private int goalCount = 0;
            private Monster targetMonster;
            public KillMonsterQuest(string name, string desc, int gold, float exp, List<Item> items,Monster targetMonster,int goal):base(name, desc,gold,exp,items)
            {   
                this.targetMonster = targetMonster;
                this.goalCount = goal;
                float bonusReward = 1f + goalCount * 0.1f; // 만약 많이 잡는 퀘스트가 걸리면 추가보상
                Reward_Exp = (Reward_Exp * bonusReward); // 추가보상 경험치, 골드
                Reward_Gold = (int)(Reward_Gold * bonusReward);
                makeQuestCore();

            }
            public override void makeQuestCore()
            {
                questCore = $"-{targetMonster.NAME} {goalCount}마리 처치 ({killCount}/{goalCount})\n";

            }


            public override void CompleteQuest()
            {
                if (killCount >= goalCount)
                {
                    IsClear = true;
                }
            }



            public void GetKillCount()
            {
                killCount++;
                CompleteQuest();

                /*
                 타켓 몬스터를 잡았을 때, killCount가 오르는 것을 구현할 필요가 있다.

                방법 1. Monster가 죽을 때, QuestController에게 Monster의 name을 반환. QuestController는 플레이어가 받은 퀘스트 중
                사냥 퀘스트가 존재하는 지 확인 후 targetMonster의 name이 이와 일치하면 일괄적으로 killCount를 올린다.

                장점. 구현이 편하다, 직관적이다. 
                
                단점. Monster가 죽을 때마다 이를 반환받은 QuestController는 자신의 List에서(플레이어가 받은 퀘스트 목록 리스트) 
                사냥 퀘스트가 존재하는 지 확인하고, 존재하면 이를 targetMonster의 name 과 비교하여 targetMonster가 맞는 지 확인 후
                killCount를 올려야 하므로, 많은 리소스를 소모한다.

                방법 2. QuestController에서 플레이어가 받은 사냥 퀘스트가 생기면 이를 Dictionary<string,int>[]targetName 으로 targetMonster의 Name을 관리
                key로는 QuestController의 string을 받고, value로는 플레이어가 받은 퀘스트 리스트에서 해당 targetName이 존재하는 사냥 퀘스트의 index를 가진다.
                플레이어가 몬스터를 잡으면 -> QuestController에게 해당 몬스터의 이름 반환 -> Dictionary에서 해당 몬스터를 검색 -> 존재하면 playerReceiveQuest[value].getKillCount()

                장점. 성능상의 이점이 있다. QuestController의 List상에서 해당하는 사냥 퀘스트를 하나하나 찾아서 KillCount를 올리는 것 보다 낫다!

                단점. 추가적인 구현이 필요하다. 공간복잡도가 조금 올라간다. 추가적인 변수를 사용해야한다. 
                
                
                QuestController에서 퀘스트 성공 및 실패가 모두 이루어진다면, Player에게 Reward를 제공할 수 있도록 메서드 필요.
                setExp(), setGold(), AddItem()등..
                
                
                 
                 */
            }
        }

        class PlayerStatQuest : Quest
        {

            questRequireStatName statType;
            float goalStatValue;
            float currentStatValue;

            public PlayerStatQuest(string name, string desc, int gold, float exp, List<Item> items,questRequireStatName type,int goal) : base(name, desc, gold, exp, items)
            {
                //int randomStat = new Random().Next(0, statType.Length);
                //targetStatName = statType[randomStat];
                this.goalStatValue = goal;
                this.statType = type;
                GetTargetStatValue();


            }


            public override void makeQuestCore()
            {
                questCore = $"-{statType}을 {goalStatValue}까지 올리기! {currentStatValue}/{goalStatValue})\n";
            }

            public override void CompleteQuest()
            {
                if (currentStatValue >= goalStatValue)
                {
                    IsClear = true;
                }
            }

            public void GetTargetStatValue()
            {
                if (statType == questRequireStatName.공격력)
                {
                    //플레이어의 공격력을 currentStatValue
                }
                else if (statType == questRequireStatName.방어력)
                {
                    //플레이어의 방어력을 currentStatValue
                }
                else if (statType == questRequireStatName.레벨)
                {
                    //플레이어의 레벨을 currentStatValue
                }
            }

            public void SetGoalStatValue()
            {
                // 플레이어의 현재 레벨에 비례해서 증가
            }
        }



        class QuestController //퀘스트 씬 출력 및 퀘스트 데이터베이스 세팅 담당 클래스 
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
                List<Quest> questList = new List<Quest>()
                {
                    new KillMonsterQuest("내일배움캠프를 위협하는 오수호 처치","내일배움캠프를 위협하는 오수호를 처리해주시게",100,100f,new List<Item>(){new Item()},new Slime(),5),
                    new PlayerStatQuest("어쩌라고 공격력 올려와","퀘스트 출력하는 공간인데 어쩌라고",150,200f,new List<Item>(){new Item()},questRequireStatName.공격력,5)
                };
                return questList;
            }

            public void DisplayQuest()
            {
                foreach(Quest q in questList)
                {
                    q.ShowQuest();
                    q.ShowQuestName();
                }


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

        class Print
        {
            public static void print(string str)
            {
               
                Console.WriteLine(str);
                
            }
        }




    }
}
