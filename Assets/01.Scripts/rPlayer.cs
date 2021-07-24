using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rPlayer : Player
{


    
    

    public override void Start() 
    {
        base.Start();
        Init();
    }

    void Init()
    {
        speed = 4f;
        jumpSpeed = 7f;
    }

    public override void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // curTr = transform.position;

            // Vector2 dir = target - curTr;
            // float z = Mathf.Atan2(dir.y , dir.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(0,0,z);

            // Instantiate(projectTile, transform.position, transform.rotation);
        }
    }
}
