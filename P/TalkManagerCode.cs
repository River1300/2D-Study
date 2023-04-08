using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManagerCode : MonoBehaviour
{
    // [7]Talk System : 필요 속성(오브젝트마다 보유하고 있는 대화문장을 저장할 사전)
    Dictionary<int, string[]> talkData;
    // [9]Portrait : 필요 속성(id마다 보유하고 있는 초상화를 저장할 사전, 초상화를 받아올 스프라이트 배열 변수)
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr;

    void Awake()
    {   // [7]Talk System : 1) 사전을 초기화 시켜준다.
        talkData = new Dictionary<int, string[]>();
        // [9]Portrait : 3) 사전을 초기화 시켜준다.
        portraitData = new Dictionary<int, Sprite>();
        // [7]Talk System : 2) 사전에 오브젝트 별 문장을 저장할 함수를 호출한다.
        GenerateData();
    }
    
    void GenerateData()
    {
        talkData.Add(1000, new string[] {"Hello:2", "It's a Beatiful day, Out side:2", "Good Bye:0"});
        talkData.Add(2000, new string[] {"Who are you?:3", "Your Sick of me:3", "I can Hear You:2"});
        talkData.Add(3000, new string[] {"It just Desk", "SO Dirty"});
        talkData.Add(4000, new string[] {"Simple Box", "Maybe i can Steel Some Things"});

        // [9]Portrait : 4) 현재 NPC ID는 1000, 2000이다. 1000 ~ 2000, 2000 ~ 3000 사이의 값으로 ID를 설정하고 초상화를 등록한다.
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    // [8]Talk : 1) Dictionary<>에 저장한 대사를 반환한다.
    public string GetTalk(int id, int talkIndex)
    {   // [8]Talk : 10) 게임 매니저로 부터 받은 인덱스가 대화 배열의 갯수와 같게된다면 null을 반환한다.
        if(talkIndex == talkData[id].Length)
        {
            return null;
        } 
        else
        {   // [8]Talk : 2) id를 Key로 넘겨주고 배열로 나온 Value 중에서 인덱스를 전달하여 해당 대사를 반환한다. -> GameManager
            return talkData[id][talkIndex];
        }
    }

    // [9]Portrait : 5) 대화문에 맞는 초상화를 반환한다. key값 오브젝트 id + 초상화 인덱스는 value값 초상화 스프라이트를 가지고 있다. -> GameManager
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
