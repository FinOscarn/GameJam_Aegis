using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mPlayer : Player
{

    public override void Start()
    {
        base.Start();
        Init();
    }

    

    void Init()
    {
        speed = 7f;
        jumpSpeed = 10f;
    }
}
