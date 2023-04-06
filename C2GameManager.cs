using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C2GameManager : MonoBehaviour
{
    // [5] Object Name
    public GameObject scanObject;
    // [16] Text Anim : 10) Text 변수를 스크립트로 전환하여 함수를 호출하도록 한다.
    public C5TypeEffect talk;
    // [6] Talk Panel
    public Animator talkPanel;
    public bool isAction;
    // [8] Talk
    public C3TalkManager talkManager;
    public int talkIndex;
    // [10] Portrait : 1) 알파값을 조절할 UI 이미지
    public Image portraitImg;
    // [11] Quest Structure
    public C4QuestManager questManager;
    // [15] Portrait Anim
    public Animator portraitAnim;
    public Sprite prevPortrait;
    // [19] Menu Set
    public GameObject menuSet;
    // [20] Title
    public Text questText;
    // [22] Save
    public GameObject player;

    void Start()
    {
        // [22] Save
        GameLoad();
        // [20] Title
        questText.text = questManager.CheckQuest();
    }

    void Update()
    {
        // [19] Menu Set : 1) ESC 버튼을 누를 경우 매뉴 창을 띄운다.
        if(Input.GetButtonDown("Cancel"))
        {
            SubMenuActive();
        }
    }

    public void SubMenuActive()
    {
        // [19] Menu Set : 2) ESC 버튼을 누를 경우 메뉴창이 띄워저 있는지 확인하여 활성화 비활성화 한다.
        if(menuSet.activeSelf)
        {
            menuSet.SetActive(false);
        }
        else
            menuSet.SetActive(true);
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        // [8] Talk : 3) 스캔한 오브젝트로 부터 오브젝트 속성을 받아온뒤 Talk()함수의 매개변수로 id와 NPC bool을 전달한다.
        C3ObjData objData = scanObject.GetComponent<C3ObjData>();
        Talk(objData.id, objData.isNPC);

        talkPanel.SetBool("isShow", isAction);
    }

    // [8] Talk : 4) 대사를 반환 받으면 문자열로 저장한다.
    void Talk(int id, bool isNpc)
    {
        // [18] Text Complete : 1) 대사 중 스페이스바를 누르면
        int questTalkIndex = 0;
        string talkData = "";

        // [18] Text Complete : 2) 애니매이션이 실행 중이라면 반환
        if(talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else        // [18] Text Complete : 3) 애니매이션이 실행 중이 아니라면 그대로 다음 대사를 진행
        {
            // [11] Quest Structure : 2) 해당 오브젝트의 퀘스트 아이디를 받아온다.
            // [11] Quest Structure : 3) 대화문을 반환받을 때 매개 변수로 퀘스트 번호를 더한 값을 보낸다.
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }

        // [9] Long Talk : 2) 대사를 반환받지 못하고 null을 전환 받았다면 해당 오브젝트와의 대화가 끝났다는 의미
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            // [11] Quest Structure : 6) 대화가 끝나면 퀘스트 인덱스가 증가한다.
            // [20] Title
            questText.text = questManager.CheckQuest(id);
            return;
        }

        if(isNpc)
        {
            // [10] Portrait : 5) 대화문의 문자열을 구분자로 나누고 구분자 앞은 출력, 뒤는 초상화 인덱스로 활용한다.
            // [16] Text Anim
            talk.SetMsg(talkData.Split(':')[0]);
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));

            // [10] Portrait
            portraitImg.color = new Color(1, 1, 1, 1);

            // [15] Portrait Anim : 1) 이전의 초상화와 현재의 초상화가 다른 이미지라면 트리거 발동
            if(prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        }
        else
        {
            talk.SetMsg(talkData);

            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;

        // [9] Long Talk : 3) 대화가 진행되기 위해 대화문을 한 번 출력할 때마다 인덱스도 증가 시킨다.
        talkIndex++;
    }

    // [22] Save
    public void GameSave()
    {
        // Player x/y
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        // Quest ID
        PlayerPrefs.SetInt("QuestID", questManager.questId);
        // Quest Action Indeex
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        // [22] Save : 1) 한 번도 세이브 한 적이 없다면 반환
        if(!PlayerPrefs.HasKey("PlayerX"))
        {
            return;
        }

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestID");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        // [22] Save
        questManager.ControlObject();
    }

    // [21] QUIT
    public void GameExit()
    {
        Application.Quit();
    }
}
