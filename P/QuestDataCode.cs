using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDataCode
{   // [10]QuestStructure : 퀘스트가 무엇인지, 퀘스트에 대한 정보, 퀘스트에 대한 속성을 담아 둔 구조체
    //                    : 속성과 생성자를 가지고 있다.
    public strint questName;
    public int[] npcId;

    // [10]QuestStructure : 1) 퀘스트에 대해 관리하는 퀘스트 매니저에서 퀘스트 정보가 넘어오면 해당 정보를 구조체로 저장한다. -> QuestManager
    public QuestDataCode(string title, int[] id)
    {
        questName = title;
        npcId = id;
    }
}
