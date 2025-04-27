using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _15jijo.ho
{
    public abstract class Quest
    {
        protected string? QuestName { get; set; } // 퀘스트 이름
        protected string? QuestDesc { get; set; } // 퀘스트 설명
        public int Reward_Gold { get; protected set; } // 보상 골드
        public int Reward_Exp { get; protected set; } // 보상 경험치
        public List<Item>? Reward_Items { get; protected set; } // 보상 아이템
        public bool IsClear { get; set; } = false;
        public bool IsReceive { get; set; } = false;
        protected string? questCore;

        public abstract void CompleteQuest(); // 퀘스트 유형 별 퀘스트 완료 로직 구현을 오버라이딩 할 추상 메서드
        public abstract void makeQuestCore(); // 퀘스트 유형 별 퀘스트 핵심 출력 로직 구현을 오버라이딩 할 추상 메서드 

        public Quest(string name, string desc, int gold, int exp, List<Item> items) // 퀘스트 생성자
        {
            this.QuestName = name;
            this.QuestDesc = desc;
            this.Reward_Gold = gold;
            this.Reward_Exp = exp;
            this.Reward_Items = items;
            QuestDesc = QuestDesc.Replace(",", "\r\n");
            
        }

        public virtual void ShowQuestName() // 퀘스트 이름을 출력하는 메서드. 맨 오른쪽에 진행상황에 따라 다른 문구를 출력.
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

            Console.Write($"{QuestName} {currentQuestState}\n");
        }

        

        public virtual void ShowQuest() // 퀘스트의 전체정보를 출력하는 메서드. 퀘스트 이름, 설명, 보상, 퀘스트 핵심 내용 출력
        {
            ShowQuestName();
            Console.WriteLine($"{QuestDesc}\n");
            Console.WriteLine(questCore);// 퀘스트 핵심 내용
            Console.WriteLine("-보상-");
            int rewardItemCount = Reward_Items == null ? 0 : Reward_Items.Count;
            if (rewardItemCount > 0)
            {
                foreach(Item item in Reward_Items)
                {
                    if (item.ItemType == ItemType.Consumable)
                    {
                        ConsumeItem ci = (ConsumeItem)item;
                        Console.WriteLine($"{item.ItemName} x {ci.ItemCount}");
                    }
                    else
                    {
                        Console.WriteLine($"{item.ItemName} x 1 ");

                    }
                        
                }
                //만약 보상 아이템이 존재하면 출력!
            }
            Console.WriteLine($"{Reward_Gold} G \n{Reward_Exp} exp\n");
        }
    }

    public class KillMonsterQuest : Quest // 몬스터를 잡는 유형 -사냥 퀘스트- 의 퀘스트 구현
    {
        private int killCount = 0;
        private int goalCount = 0;
        private Monster targetMonster;
        public KillMonsterQuest(string name, string desc, int gold, int exp, List<Item> items, Monster targetMonster, int goal) : base(name, desc, gold, exp, items)
        {
            this.targetMonster = targetMonster;
            this.goalCount = goal;
            float bonusReward = 1f + goalCount * 0.1f; // 만약 많이 잡는 퀘스트가 걸리면 추가보상
            Reward_Exp = (int)(Reward_Exp * bonusReward); // 추가보상 경험치, 골드
            Reward_Gold = (int)(Reward_Gold * bonusReward);
            makeQuestCore();
            

        }
        public override void makeQuestCore()    // 사냥퀘스트의 핵심내용을 string으로 만들어서 questCore에 저장하는 메서드
        {
            questCore = $"-{targetMonster.Name} {goalCount}마리 처치 ({killCount}/{goalCount})\n";

        }

        public void CheckTargetMonster(string name) // 해당 몬스터가 죽었을 때, 몬스터의 이름을 받아서 퀘스트의 몬스터와 비교하는 메서드
        {
            if (name == targetMonster.Name)
            {
                GetKillCount();
                
            }
        }


        public override void CompleteQuest() // 퀘스트 완료 체크 메서드
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

    public class PlayerStatQuest : Quest
    {
        public Player? player;
        questRequireStatName statType;

        float goalStatValue;
        float currentStatValue;

        public PlayerStatQuest(string name, string desc, int gold, int exp, List<Item> items, questRequireStatName type, int goal) : base(name, desc, gold, exp, items)
        {

            this.goalStatValue = goal;
            this.statType = type;
            float bonusReward = 1f + goalStatValue * 0.01f; // 만약 많이 잡는 퀘스트가 걸리면 추가보상
            Reward_Exp = (int)(Reward_Exp * bonusReward); // 추가보상 경험치, 골드
            Reward_Gold = (int)(Reward_Gold * bonusReward);
            makeQuestCore();    // 퀘스트 핵심 정보 요약문 생성
            

        }


        public override void makeQuestCore() // 플레이어 스텟 퀘스트의 핵심내용을 string으로 만들어서 questCore에 저장하는 메서드
        {
            GetTargetStatValue(); // 플레이어 객체로부터 해당 퀘스트의 목표 스텟과 일치하는 항목의 스텟값을 받아오는 메서드
            questCore = $"-{statType}을 {goalStatValue}까지 올리기! ({currentStatValue}/{goalStatValue})\n";
        }

        public override void CompleteQuest() // 퀘스트 완료 체크 메서드
        {
            if (currentStatValue >= goalStatValue)
            {
                IsClear = true;
            }
        }

        public void GetTargetStatValue()    // 플레이어 객체로부터 해당 퀘스트의 목표 스텟과 일치하는 항목의 스텟값을 받아오는 메서드
        {
            if (player != null)
            {


            if (statType == questRequireStatName.공격력)
            {
                currentStatValue = player.TotalAttackPower;
            }
            else if (statType == questRequireStatName.방어력)
            {
                currentStatValue = player.TotalDefensivePower;
            }
            else if (statType == questRequireStatName.레벨)
            {
                currentStatValue = player.Level;
            }


            }
            else
            {
                return;
            }

        }




    }






}
