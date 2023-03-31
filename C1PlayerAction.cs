using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1PlayerAction : MonoBehaviour
{   // [1] Player Move
    public float speed;
    float h;
    float v;
    Rigidbody2D rigid;
    // [2] Cross Move
    bool isHorizonMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        PlayerMove();
    }

    void FixedUpdate()
    {   // [2] Cross Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);

        // [1] Player Move
        rigid.velocity = moveVec * speed;
    }

    void PlayerMove()
    {
        // [1] Player Move
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // [2] Cross Move
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
    }
}
