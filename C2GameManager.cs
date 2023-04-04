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
    // [10] Portrait : 1) 알파값을 조절할 UI 이미지
    public Image portraitImg;

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
            // [10] Portrait : 5) 대화문의 문자열을 구분자로 나누고 구분자 앞은 출력, 뒤는 초상화 인덱스로 활용한다.
            talkText.text = talkData.Split(':')[0];
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));

            // [10] Portrait
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;

        // [9] Long Talk : 3) 대화가 진행되기 위해 대화문을 한 번 출력할 때마다 인덱스도 증가 시킨다.
        talkIndex++;
    }
}
