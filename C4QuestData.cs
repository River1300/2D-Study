using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4QuestData
{
    public string questName;
    public int[] npcId;

    // [11] Quest Structure : 1) 생성자를 통해 구조체의 값을 초기화 한다.
    public C4QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
