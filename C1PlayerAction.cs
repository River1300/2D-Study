using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1PlayerAction : MonoBehaviour
{   // [1] Player Move
    public float speed;
    float h;
    float v;
    Rigidbody2D rigid;
    // [2] Cross Move : 1) 수평 이동에 대한 플래그 생성
    bool isHorizonMove;
    // [3] Animation
    Animator anim;
    // [4] Ray : 1) Ray를 발사할 방향 Vector
    Vector3 dirVec;
    GameObject scanObject;
    // [5] Object Name
    public C2GameManager manager;
    // [23] Button Move
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;
    bool up_Down;
    bool down_Down;
    bool left_Down;
    bool right_Down;
    bool up_Up;
    bool down_Up;
    bool left_Up;
    bool right_Up;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {   
        PlayerMove();

        // [4] Ray : 5) 스페이스바 입력을 받으면 오브젝트와 상호 작용한다.
        if(Input.GetButtonDown("Jump") && scanObject != null)
        {
            // [5] Object Name
            manager.Action(scanObject);
        }

        // [23] Button Move : 5) 프레임이 넘어가면 변수값 초기화
        up_Down = false;
        down_Down = false;
        left_Down = false;
        right_Down = false;
        up_Up = false;
        down_Up = false;
        left_Up = false;
        right_Up = false;
    }

    void FixedUpdate()
    {   // [2] Cross Move : 2) 플래그 값에 따라서 참이면 수평 / 거짓이면 수직 이동 값만을 방향으로 배정
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);

        // [1] Player Move
        rigid.velocity = moveVec * speed;

        // [4] Ray : 3) Ray를 게임 씬에 시각화
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        // [4] Ray : 4) Ray를 통해 오직 Object 레이어의 정보만을 받는다.
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));
        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }

    void PlayerMove()
    {
        // [1] Player Move
        // [23] Button Move : 3) 정수형 변수를 이동 값에 더해 준다.
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + right_Value + left_Value;
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical") + up_Value + down_Value;

        // [2] Cross Move : 3) 입력받은 버튼을 저장하고 그 값에 따라 수평값이 참인지 거짓인지 배정
        // [3] Animation : 2) 동시에 키 입력이 있을 경우 방향을 잡지 못하는 경우가 발생한다. 버튼을 때었을 때 수평 값이 있는지 없는지를 확인한다.
        // [23] Button Move : 4) bool 형 변수의 참 거짓을 or로 전달 한다.
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical") || up_Up || down_Up;
        if(hDown)
            isHorizonMove = true;
        else if(vDown)
            isHorizonMove = false;
        else if(hUp || vUp)
            isHorizonMove = h != 0;

        // [3] Animation : 1) 중복되는 값을 계속해서 전달하지 않도록 제어
        // [3] Animation : 3) 수평 버튼을 누른 상태에서 수직 버튼을 누를 경우 이동은 하지만 애니매이션은 계속 수평 방향이다. 애니메이터 -> 트랜지션 -> Can Transition To Self를 해제한다.
        if(anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isMove", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isMove", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }else
        {
            anim.SetBool("isMove", false);
        }

        // [4] Ray : 2) Ray Vector3의 방향을 배정
        if(anim.GetBool("isMove"))
        {
            if(v == 1)
                dirVec = Vector3.up;
            else if(v == -1)
                dirVec = Vector3.down;
            else if(h == -1)
                dirVec = Vector3.left;
            else if(h == 1)
                dirVec = Vector3.right;
        }
    }

    // [23] Button Move : 1) 방향키가 4개 이므로 문자로 구분하여 움직이도록 한다.
    public void ButtonDown(string type)
    {
        switch(type)
        {
            // [23] Button Move : 2) 버튼 입력에 따라 변수에 정수, bool 값을 넣어 준다.
            case "U":
                up_Value = 1;
                up_Down = true;
                break;
            case "D":
                down_Value = -1;
                down_Down = true;
                break;
            case "L":
                left_Value = -1;
                left_Down = true;
                break;
            case "R":
                right_Value = 1;
                right_Down = true;
                break;
            // [24] Action Key : 1) 게임 매니저의 함수 호출
            case "A":
                if(scanObject != null)
                    manager.Action(scanObject);
                break;
            case "C":
                manager.SubMenuActive();
                break;
        }
    }

    public void ButtonUp(string type)
    {
        switch(type)
        {
            case "U":
                up_Value = 0;
                up_Up = true;
                break;
            case "D":
                down_Value = 0;
                down_Up = true;
                break;
            case "L":
                left_Value = 0;
                left_Up = true;
                break;
            case "R":
                right_Value = 0;
                right_Up = true;
                break;
        }
    }
}
