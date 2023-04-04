using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C3TalkManager : MonoBehaviour
{
    // [7] Talk Manager
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    // [7] Talk Manager : 1) id별로 대사를 추가한다.
    void GenerateData()
    {
        talkData.Add(1000, new string[] {"Hello", "It's a Beatiful day, Out side", "Good Bye"});
        talkData.Add(2000, new string[] {"Who are you?", "Your Sick of me", "I can Hear You"});
        talkData.Add(3000, new string[] {"It just Desk", "SO Dirty"});
        talkData.Add(4000, new string[] {"Simple Box", "Maybe i can Steel Some Things"});
    }

    // [8] Talk : 1) 문자열을 배열로 넣었으므로 배열의 인덱스를 통해 원하는 대사를 반환한다.
    // [8] Talk : 2) 해당 오브젝트의 대사를 반환하기 위해 해당 오브젝트의 ID를 받는다.
    public string GetTalk(int id, int talkIndex)
    {
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
}
