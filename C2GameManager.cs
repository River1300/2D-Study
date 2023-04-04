using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C2GameManager : MonoBehaviour
{
    // [5] Object Name
    public GameObject scanObject;
    public Text talkText;
    // [6] Talk Panel
    public GameObject talkPanel;
    public bool isAction;
    // [8] Talk
    public C3TalkManager talkManager;
    public int talkIndex;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        // [8] Talk : 3) 스캔한 오브젝트로 부터 오브젝트 속성을 받아온뒤 Talk()함수의 매개변수로 id와 NPC bool을 전달한다.
        C3ObjData objData = scanObject.GetComponent<C3ObjData>();
        Talk(objData.id, objData.isNPC);

        talkPanel.SetActive(isAction);
    }

    // [8] Talk : 4) 대사를 반환 받으면 문자열로 저장한다.
    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        // [9] Long Talk : 2) 대사를 반환받지 못하고 null을 전환 받았다면 해당 오브젝트와의 대화가 끝났다는 의미
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        if(isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }
        isAction = true;

        // [9] Long Talk : 3) 대화가 진행되기 위해 대화문을 한 번 출력할 때마다 인덱스도 증가 시킨다.
        talkIndex++;
    }
}
