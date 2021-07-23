using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : Monster
{
    
    // float angle;
    // Vector2 mouse;
    // Vector2 target;

    // SpriteRenderer sprite;


    // public override void Awake() 
    // {
    //     base.Awake();
    //     target = PlayerObj.transform.position;    
    //     sprite = GetComponent<SpriteRenderer>();
    // }
    
    // private void Update() 
    // {
    //     CanonMove();
    // }

    // void CanonMove()
    // {
    //     //mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); 플레이어에도 사용
    //     angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
    //     CanonHead.transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
    // }

    // public override void Chase()
    // {
    //     if(realDir == -1) sprite.flipX = true;
    //     else if(realDir == 1) sprite.flipX = false;
    // }
    
}
