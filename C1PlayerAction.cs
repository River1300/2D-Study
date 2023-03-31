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

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {   
        PlayerMove();
    }

    void FixedUpdate()
    {   // [2] Cross Move : 2) 플래그 값에 따라서 참이면 수평 / 거짓이면 수직 이동 값만을 방향으로 배정
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);

        // [1] Player Move
        rigid.velocity = moveVec * speed;
    }

    void PlayerMove()
    {
        // [1] Player Move
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // [2] Cross Move : 3) 입력받은 버튼을 저장하고 그 값에 따라 수평값이 참인지 거짓인지 배정
        // [3] Animation : 2) 동시에 키 입력이 있을 경우 방향을 잡지 못하는 경우가 발생한다. 버튼을 때었을 때 수평 값이 있는지 없는지를 확인한다.
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonDown("Horizontal");
        bool vUp = Input.GetButtonDown("Vertical");
        if(hDown)
            isHorizonMove = true;
        else if(vDown)
            isHorizonMove = false;
        else if(hUp || vUp)
            isHorizonMove = h != 0;

        // [3] Animation : 1) 중복되는 값을 계속해서 전달하지 않도록 제어
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
    }
}
