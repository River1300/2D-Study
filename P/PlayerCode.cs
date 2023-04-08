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
    // [4]Ray : 필요 속성(Ray를 발사할 방향 벡터, Ray에 충돌한 오브젝트를 저장할 게임 오브젝트)
    Vector3 rayDir;
    GameObject scanObject;
    // [5]Object Name : 2) Player 가 게임 매니저의 함수를 호출하기 위해 게임 매니저가 필요하다.
    public GameManagerCode manager;

    void Awake()
    {   // [1]PlayerMove : 1) Player가 가지고 있는 리지드바디 컴포넌트를 받아온다.
        rigid = GetComponent<Rigidbody2D>();
        // [3]Animation : 1) Player가 가지고 있는 애니매이터 컴포넌트를 받아온다.
        anim = GetComponent<Animator>();
    }

    void Update()
    {   // [1]PlayerMove : 2) 매 프레임마다 입력을 감지하여 변수에 저장한다.
        // [5]Object Name : 4) 대화창이 켜저 있을 때는 Player 가 움직이면 않된다. -> ObjectData
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");
        // [2]CrossMove : 2) bool 버튼 변수에 입력받은 버튼을 참/거짓으로 저장하고 그 값에 따라 수평값이 참인지 거짓인지 배정한다.
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");
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

        // [4]Ray : 3) Player 애니매이션에 싱크를 맞추어 Ray 도 즉각적으로 방향을 전환시키기 위해 애니매이션의 파라미터를 받아와서 제어한다.
        if(anim.GetBool("isMove"))
        {   // [4]Ray : 2) 현재 Player의 움직이는 방향 값에 따라서 Ray의 방향을 지정해 준다.
            if(v == 1) rayDir = Vector3.up;
            else if(v == -1) rayDir = Vector3.down;
            else if(h == -1) rayDir = Vector3.left;
            else if(h == 1) rayDir = Vector3.right;
        }

        // [4] Ray : 7) 스페이스바 입력을 받으면 오브젝트와 상호 작용한다.
        if(Input.GetButtonDown("Jump") && scanObject != null)
        {   // [5]Object Name : 3) 게임 매니저에서 대화창을 띄우는 함수를 호출한다. -> GameManager
            manager.Action(scanObject);
        }
    }

    void FixedUpdate()
    {   // [2]CrossMove : 1) 플래그 값에 따라서 참이면 수평/거짓이면 수직 이동 값만을 방향으로 배정하는 변수를 만든다.
        Vector2 dir = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        // [1]PlayerMove : 3) 속도를 매 프레임마다 지정해 준다.
        rigid.velocity = dir * speed;
        // [4] Ray : 4) Ray를 게임 씬에서 볼 수 있도록 미리 그려 준다.
        Debug.DrawRay(rigid.position, rayDir * 0.7f, new Color(0, 1, 0));
        // [4] Ray : 5) Physics2D를 통해 Ray를 발사하고 접촉한 오브젝트 정보를 RaycastHit2D로 받는다.
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, rayDir, 0.7f, LayerMask.GetMask("Object"));
        // [4] Ray : 7) Ray가 항상 Object 레이어가 씌워진 게임 오브젝트와 접촉할 수는 없다. GameObject에 저장하기 전에 Ray에 Object 정보가 들어 왔는지 검사한다.
        if(rayHit.collider != null)
        {
            // [4] Ray : 6) 오브젝트 정보를 받았다면 활용하기 위해 미리 만들어둔 GameObject에 저장한다.
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}
