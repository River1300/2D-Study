using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{   // [1]PlayerMove : 필요 속성(리지드바디,속도,수평값,수직값)
    Rigidbody2D rigid;
    public float speed;
    float h;
    float v;
    // [2]CrossMove : 필요 속성(수평이동에 대한 bool, 버튼 입력에 대한 bool)
    bool isHorizonMove;
    // [3]Animation : 필요 속성(애니매이터)
    Animator anim;

    void Awake()
    {   // [1]PlayerMove : 1) Player가 가지고 있는 리지드바디 컴포넌트를 받아온다.
        rigid = GetComponent<Rigidbody2D>();
        // [3]Animation : 1) Player가 가지고 있는 애니매이터 컴포넌트를 받아온다.
        anim = GetComponent<Animator>();
    }

    void Update()
    {   // [1]PlayerMove : 2) 매 프레임마다 입력을 감지하여 변수에 저장한다.
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        // [2]CrossMove : 2) bool 버튼 변수에 입력받은 버튼을 참/거짓으로 저장하고 그 값에 따라 수평값이 참인지 거짓인지 배정한다.
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");
        if(hDown)
            isHorizonMove = true;
        else if(vDown)
            isHorizonMove = false;
        else if(hUp || vUp)
            isHorizonMove = h != 0;
        // [3]Animation : 3) 중복되는 값을 계속해서 전달하면 애니매이션이 시작만 하고 진행이 되지 않는다. 그러므로 중복되는 값을 전달하려 할 경우 막는 제어문을 만든다.
        if(anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isMove", true);
            // [3]Animation : 2) 애니매이터 파라미터에 값을 전달한다.
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isMove", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isMove", false);
        }   // [3]Animation : 4) 수평 버튼을 누른 상태에서 수직 버튼을 누를 경우 이동은 하지만 애니매이션은 계속 수평 방향이다. 애니메이터 -> 트랜지션 -> Can Transition To Self를 해제한다.
    }

    void FixedUpdate()
    {   // [2]CrossMove : 1) 플래그 값에 따라서 참이면 수평/거짓이면 수직 이동 값만을 방향으로 배정하는 변수를 만든다.
        Vector2 dir = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        // [1]PlayerMove : 3) 속도를 매 프레임마다 지정해 준다.
        rigid.velocity = dir * speed;
    }
}
