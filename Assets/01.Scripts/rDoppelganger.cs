using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rDoppelganger : Player
{
    public enum States
    {
        Chase,
        Attack,
        Jump
    }
    States states = States.Chase;

    bool isMove = true;

    public override void Start() 
    {
        base.Start();
        Init();
    }

    public override void Update() 
    {
        CheckStates();
    }

    void Init()
    {
        attackSpeed = 2f;
        attackRange = 6f;
        jumpSpeed = 5f;
    }

    void CheckPlatform()
    {
        Debug.DrawRay(transform.position, Vector2.up * 100f, Color.red);
        Debug.Log("SAdasd");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 100f);
        if(hit)
        {
            if(hit.transform.CompareTag("Ground")&& isGround)
            {
                rb.AddForce(Vector2.up*jumpSpeed, ForceMode2D.Impulse);
                Debug.Log("Jump");
                isMove = false;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
    }

    void StartStates(States _states)
    {
        states = _states;
        switch(states)
        {
            case States.Chase:
            isMove = true;
            DoppelgangerMove();
            CheckPlatform();
            break;
            case States.Attack:
            isMove = false;
            attackSpeed = Time.time + 1f; 
            Attack();
            break;
        }
    }

    void CheckStates()
    {
        switch(states)
        {
            case States.Chase:
                if(distance > attackRange * attackRange) StartStates(States.Chase);
                if(distance < (attackRange * attackRange)) StartStates(States.Attack);
                Debug.Log("Chase");
            break;
            case States.Attack:
                if(Time.time > 0.5f) StartStates(States.Chase);
                Debug.Log("attack");
            break;
        }
    }
}
