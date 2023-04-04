using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4QuestManager : MonoBehaviour
{
    // [11] Quest Structure
    public int questId;
    public int questActionIndex;
    Dictionary<int, C4QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, C4QuestData>();
        GenerateData();
    }

    void GenerateData()
    {   // [11] Quest Structure : 4) npcID를 배열로 넣으면 인덱스로 구분짓게 된다. 그 순서대로 퀘스트가 진행될 수 있게 이 배열의 인덱스가 변수로 필요하다.
        questList.Add(10, new C4QuestData("첫 마을 방문", new int[] {1000, 2000}));
        questList.Add(20, new C4QuestData("루도의 동전 찾아주기.", new int[] { 5000, 2000 }));
    }

    // [11] Quest Structure : 1) NPC의 id를 받은 뒤 퀘스트 번호를 반환한다.
    public int GetQuestTalkIndex(int id)
    {
        // [11] Quest Structure : 5) 퀘스트 진행 인덱스는 대화를 한 번 할 때마다 증가한다.
        return questId + questActionIndex;
    }

    public void CheckQuest(int id)
    {   // [11] Quest Structure : 8) 퀘스트 대화문을 출력할 때만 퀘스트 인덱스가 증가하도록 한다.
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++;
    }

    // [12] Next Quest
    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
}
