using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;
namespace _15jijo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.Init();
            gameManager.GameStart();
        }

        class Player
        {
            private float attack;
            public float ATTACK
            {
                get { return attack; }
                set
                {
                    attack = value;
                }
            }

            public float GetTotalAttack()
            {
                return ATTACK;
            }




            public void HuntMonster(Monster monseter)
            {

            }
        }

        class Item
        {
            private Player? _player;
            public void GetStatFromItem()
            {
                if (_player != null)
                    _player.ATTACK += 10;

            }

        }
        abstract class Quest
        {
            private string? questName; // 퀘스트 이름
            protected string? QUESTNAME { get; set; }
            private string? questDesc; // 퀘스트 설명
            protected string? QUESTDESC { get; set; }
            private int reward_Gold; // 보상 골드
            protected int REWARD_GOLD { get; set; }
            private float reward_exp; // 보상 경험치
            protected float REWARD_EXP { get; set; }
            private List<Item>? Reward_Items; // 보상 아이템 리스트
            protected List<Item>? REWARD_ITEMS { get; set; }
            private bool isClear = false; // 이미 클리어 한 퀘스트 인가?
            public bool ISCLEAR { get; set; }
            private bool isReceive = false; // 이미 받은 퀘스트 인가?
            public bool ISRECEIVE { get; set; }
            public string? questCore;

            public abstract void CompleteQuest(); // 퀘스트 유형 별 퀘스트 완료 로직 구현을 오버라이딩 할 추상 메서드
            public abstract void makeQuestCore(); // 퀘스트 유형 별 퀘스트 핵심 출력 로직 구현을 오버라이딩 할 추상 메서드 

            public virtual void ShowQuestName()
            {
                string? currentQuestState = "";
                if (ISCLEAR)
                {
                    currentQuestState += "(완료)";
                }
                else if (ISRECEIVE)
                {
                    currentQuestState += "(진행중)";
                }
                else
                {
                    // 받은 상태가 아니면 퀘스트 이름 옆에 추가할 string이 있을까?
                }

                Print.print($"{QUESTNAME} {currentQuestState}\n");
            }

            public virtual void ShowQuest()
            {
                ShowQuestName();
                Print.print($"{QUESTDESC}\n");
                Print.print(questCore);// 퀘스트 핵심 내용
                Print.print("-보상-");
                int rewardItemCount = REWARD_ITEMS == null ? 0 : REWARD_ITEMS.Count;
                if (rewardItemCount > 0)
                {
                    //만약 보상 아이템이 존재하면 출력!
                }
                Print.print($"{REWARD_GOLD} G \n{REWARD_EXP} exp\n");
            }
        }

        class KillMonsterQuest : Quest // 몬스터를 잡는 유형 -사냥 퀘스트- 의 퀘스트 구현
        {
            private int killCount = 0;
            private int goalCount = 0;
            private Monster targetMonster;
            public KillMonsterQuest(Monster targetMonster)
            {
                this.targetMonster = targetMonster;
                goalCount = new Random().Next(3, 10); // 랜덤으로 몇 마리의 몬스터를 잡을 지 퀘스트화
                float bonusReward = 1f + killCount * 0.1f; // 만약 많이 잡는 퀘스트가 걸리면 추가보상
                REWARD_EXP *= bonusReward; // 추가보상 경험치, 골드
                REWARD_GOLD = (int)(REWARD_GOLD * bonusReward);
                makeQuestCore();

            }
            public override void makeQuestCore()
            {
                questCore = $"{targetMonster.GetMonsterName()} {killCount}마리 처치 ({killCount}/{goalCount})\n";
                
            }


            public override void CompleteQuest()
            {
                if (killCount >= goalCount)
                {
                    ISCLEAR = true;
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

        

        class QuestController //퀘스트 씬 출력 및 퀘스트 데이터베이스 세팅 담당 클래스 
        {
            private Quest[]? questDB;
            private List<Quest>? playerReceiveQuest;
            

            static List<Quest> SetQuestDB()
            {
                List<Quest> questList = new List<Quest>
                {



                };
                return questList;
            }

            public void ClearQuest()
            {
                //퀘스트 클리어 로직
            } 
            
           

        }

        class Print
        {
            public static void print(string str)
            {
                int age = 1;
                Console.WriteLine(str);
                Console.WriteLine($"{age}");
            }
        }


        abstract class Monster
        {
            string? name;
            protected string? NAME { get; set; }
            int level;
            protected int LEVEL { get; set; }

            public string GetMonsterName()
            {
                return name;
            }



        }

        class Slime : Monster
        {
            Slime()
            {
                NAME = "slime";
            }




        }

    }
}
