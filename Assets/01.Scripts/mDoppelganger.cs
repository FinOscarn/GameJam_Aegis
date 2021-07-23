using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mDoppelganger : Player
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
        Monster = DataManager.Instance.monsters[0];
    }

    void Init()
    {
        
        attackSpeed = 2f;
        attackRange = 6f;
        jumpSpeed = 5f;
    }

    private void FixedUpdate() 
    {
        CheckStates();
    }

    void CheckPlatform()
    {
        Debug.DrawRay(transform.position, Vector2.up * 4f, Color.red);
        RaycastHit2D hitY = Physics2D.Raycast(transform.position, Vector2.up, 7f);
        if(hitY)
        {
            if(hitY.transform.CompareTag("Ground")&& isGround)
            {
                rb.AddForce(Vector2.up*jumpSpeed, ForceMode2D.Impulse);
                Debug.Log("Jump");
                isMove = false;
            }
        }

        Debug.DrawRay(transform.position, Vector2.right * 3f, Color.red);
        RaycastHit2D hitX = Physics2D.Raycast(transform.position, Vector2.right, 3f);
        if(hitX)
        {
            if(hitX.transform.CompareTag("Ground")&& isGround)
            {
                rb.AddForce(Vector2.up*jumpSpeed, ForceMode2D.Impulse);
                Debug.Log("Jump");
            }
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
    }

    void Teleport()
    {
        int curTr = Mathf.RoundToInt(transform.position.x);
        int monTr = Mathf.RoundToInt(Monster.transform.position.x);

        if(curTr == monTr)                                                                                                            
        {
            Debug.Log("apsdasd");

            transform.position = Monster.transform.position;
        }
    }

    void StartStates(States _states)
    {
        states = _states;
        switch(states)
        {
            case States.Chase:
            isMove = true;
            animator.SetBool("isWalk", true);
            DoppelgangerMove();
            CheckPlatform();
            Teleport();
            break;
            case States.Attack:
            animator.SetTrigger("IsAttack");
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
                if(distance < (attackRange * attackRange)&& Time.time > attackSpeed) StartStates(States.Attack);
                //Debug.Log("Chase");
            break;
            case States.Attack:
                if(Time.time > 0.5f) StartStates(States.Chase);
                //Debug.Log("attack");
            break;
        }
    }
    
}
