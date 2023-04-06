using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C5TypeEffect : MonoBehaviour
{
    // [16] Text Anim
    public int CharPerSeconds;
    string targetMsg;
    // [16] Text Anim : 2) 공백의 문자열에서 부터 한글자씩 입력되는 애니매이션을 만들기 위한 공백 문자열
    Text msgText;
    // [16] Text Anim : 4) 0에서 부터 문자열 끝까지 한글자씩 입력된다.
    int index;
    float interval;
    // [16] Text Anim : 9) 모든 대사가 타이핑 되면 커서 활성화
    public GameObject EndCursor;
    // [17] Text Sound
    AudioSource audioSource;
    // [18] Text Complete
    public bool isAnim;

    void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    // [16] Text Anim : 1) 문자열을 받아서 저장하고 애니메이션을 실행 시킨다.
    public void SetMsg(string msg)
    {
        // [18] Text Complete
        if(isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        // [16] Text Anim : 3) 공백에서부터 시작한다.
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        // [16] Text Anim : 6) interval 초에 한 번씩 타이핑을 진행한다.
        interval = 1.0f / CharPerSeconds;

        // [18] Text Complete
        isAnim = true;

        // [16] Text Anim
        Invoke("Effecting", interval);
    }

    void Effecting()
    {   // [16] Text Anim : 8) 문자를 하나씩 입력해 나가다가 모든 문자열을 완성하면 애니매이션이 종료된다.
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        // [16] Text Anim : 7) 한글자씩 입력한다.
        msgText.text += targetMsg[index];

        // [17] Text Sound : 1) 사운드를 플레이 하되, 스페이스바나 점에서는 플레이 하지 않는다.
        if(targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();

        // [16] Text Anim : 5) 문자열이 한글자씩 증가한다.
        index++;
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        // [18] Text Complete
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
