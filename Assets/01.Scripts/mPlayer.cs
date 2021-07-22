using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mPlayer : Player
{

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Init();
    }

    

    void Init()
    {
        speed = 7f;
        jumpSpeed = 10f;
    }
}
