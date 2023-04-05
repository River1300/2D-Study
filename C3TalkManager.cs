using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C3TalkManager : MonoBehaviour
{
    // [7] Talk Manager
    Dictionary<int, string[]> talkData;
    // [10] Portrait : 2) id에 따라 각기 다른 초상화를 저장한다.
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    // [7] Talk Manager : 1) id별로 대사를 추가한다.
    void GenerateData()
    {
        // [10] Portrait : 4) 구분자를 넣어 준다. 
        talkData.Add(1000, new string[] {"Hello:2", "It's a Beatiful day, Out side:2", "Good Bye:0"});
        talkData.Add(2000, new string[] {"Who are you?:3", "Your Sick of me:3", "I can Hear You:2"});
        talkData.Add(3000, new string[] {"It just Desk", "SO Dirty"});
        talkData.Add(4000, new string[] {"Simple Box", "Maybe i can Steel Some Things"});

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);

        // [11] Quest Structure : 7) 기존 NPCID에 퀘스트 인덱스를 더한 값을 key로 지정하여 대사를 저장한다.
        talkData.Add(10 + 1000, new string[] {"길잃은 아이야.:0", "너의 길은 호수에 밝혀저 있다.:0"});
        talkData.Add(11 + 2000, new string[] {"아낙 쑤나문!:3", "나의 아낙 쑤나문을 잃어 버렸어...:1", "그대는?!?!:3"});
        
        talkData.Add(20 + 2000, new string[] {"그녀라면 나의 아낙 쑤나문이 어디있는지 알거야.:3"});
        talkData.Add(21 + 1000, new string[] {"아낙 쑤나문?:3", "그게 뭐지?:1", "아! 혹시 행운에 동전을 말하는 걸까?:2"});
        talkData.Add(22 + 5000, new string[] {"금화를 찾았다!"});
        talkData.Add(23 + 2000, new string[] {"오! 나의 아낙 쑤나문!!!:2"});
    }

    // [8] Talk : 1) 문자열을 배열로 넣었으므로 배열의 인덱스를 통해 원하는 대사를 반환한다.
    // [8] Talk : 2) 해당 오브젝트의 대사를 반환하기 위해 해당 오브젝트의 ID를 받는다.
    public string GetTalk(int id, int talkIndex)
    {
        // [14] Exception : 1) 매개 변수로 전달받은 id로 Dictionary에 key가 있는지 확인하고 해당 key에 대한 value가 없다면 퀘스트 id를 제거한 오브젝트 id만 전달하여 이전 퀘스트 대화문을 다시 출력한다. 
        if(!talkData.ContainsKey(id))
        {
            // [14] Exception : 2) 그런데 만약 퀘스트 대화문이 없는 오브젝트라면? 일반 대화문을 출력한다.
            if(!talkData.ContainsKey(id - id % 10))
            {
                if(talkIndex == talkData[id - id %100].Length) return null;
                else return GetTalk(id - id % 100, talkIndex);
            }
            else
            {   
                if(talkIndex == talkData[id - id %10].Length) return null;
                else return GetTalk(id - id % 10, talkIndex);
            }
        }

        // [9] Long Talk : 1) 게임 매니저로 부터 받은 인덱스가 대화 배열의 갯수와 같게된다면 대화를 마친다.
        if(talkIndex == talkData[id].Length)
        {
            return null;
        } 
        else
        {
            return talkData[id][talkIndex];
        }
    }

    // [10] Portrait : 3) 대화문에 맞는 초상화를 반환한다. key값 오브젝트 id + 초상화 인덱스는 value값 초상화 스프라이트를 가지고 있다.
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
