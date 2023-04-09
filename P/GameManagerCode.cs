using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerCode : MonoBehaviour
{   // [5]Object Name : 게임에 대화창을 표시하는 명령은 게임 매니저가 진행한다. 
    //                : 대화의 주체가될 오브젝트의 정보는 Player가 가지고 있다. 그러므로 Player로 부터 오브젝트 정보를 받고 저장해서 사용해야 한다.
    GameObject scanObject;
    public Animator talkPanel;
    public bool isAction;
    // [8]Talk : 필요 속성(Talk매니저, 대화문 배열의 인덱스)
    public Text talkText;
    public TalkManagerCode talkManager;
    public int talkIndex;
    // [9]Portrait : 필요 속성(UI 이미지)
    public Image portraitImg;

    // [5]Object Name : 1) Player가 게임 매니저의 함수를 호출하면서 오브젝트 정보를 매개 변수로 보낸다. -> Player
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        // [8]Talk : 3) 이제 게임 오브젝트에서 대화창에 오브젝트의 대사를 띄울 예정이다.
        //         : Talk매니저의 GetTalk()함수는 오브젝트 ID와 대화목록 인덱스를 보내야 하는데 ID는 ObjectData를 받아와야 한다.
        ObjectDataCode objData = scanObject.GetComponent<ObjectDataCode>();
        // [8]Talk : 4) 대화를 하는 함수를 출력한다. 이때 오브젝트 데이타를 전달한다.
        Talk(objData.id, objData.isNpc);
        // [5]Object Name : 3) 숨겨진 대화창을 띄운다. -> Player
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNPC)
    {
        // [8]Talk : 5) { TalkManager.GetTalk() } 함수를 호출하여 대사를 반환 받으면 문자열로 저장한다.
        string talkData = talkManager.GetTalk(id, talkIndex);

        // [8]Talk : 9) 대사를 반환받지 못하고 null을 전환 받았다면 해당 오브젝트와의 대화가 끝났다는 의미이다. -> TalkManager
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        if(isNPC)
        {   // [8]Talk : 6) talkData에 저장된 대사를 텍스트로 출력한다.
            talkText.text = talkData.Split(':')[0];
            // [9]Portrait : 6) 대화문의 문자열을 구분자로 나누고 구분자 앞은 출력, 뒤는 초상화 인덱스로 활용한다. -> QuestData
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            // [9]Portrait : 1) NPC는 초상화가 있으므로 알파값을 1로 설정한다.
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            // [9]Portrait : 2) 일반 오브젝트는 초상화가 없으므로 알파값을 0으로 설정한다. -> TalkManager
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        // [8]Talk : 7) 대사를 출력하려면 당연히 대화창이 띄워져 있어야 한다.
        isAction = true;
        // [8]Talk : 8) 오브젝트가 보유한 대화는 1개 이상일 수가 있다. 1개 이상의 대화를 받으려면 문자열의 인덱스가 증가해야만 한다.
        talkIndex++;
    }
}
