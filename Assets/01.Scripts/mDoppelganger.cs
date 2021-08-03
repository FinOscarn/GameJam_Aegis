using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mDoppelganger : Player
{
    public GameObject player; 
    public float playerDis;

    public enum States
    {
        Idle,
        Chase,
        Attack,
        Jump
    }
    States states = States.Chase;

    public Vector2  dir;

    bool isMove = true;

    public override void Start() 
    {
        base.Start();
        Init();
        DoppelHpbar = GameObject.Find("Canvas/Doppelganger");
    }

    public override void Update() 
    {
        if(DataManager.Instance.monsters.Count > 0)
        {
            Monster = DataManager.Instance.monsters[0];
        }
        else
        {
            Vector2 dir = player.transform.position - transform.position;
            int playerDir = dir.x > 0 ? 1 : -1;
            if(Mathf.Abs(transform.position.x - player.transform.position.x) > distance)
            {
                rb.velocity = new Vector2(playerDir * speed ,rb.velocity.y);
            }
        }
        Hpbar();
    }

    void Hpbar()
    {
        DoppelHpbar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.2f ,0));
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
            Debug.Log("왜 안댐");
            if(hitY.transform.CompareTag("Ground") && isGround)
            {
                rb.AddForce(Vector2.up*jumpSpeed, ForceMode2D.Impulse);
                Debug.Log("Jump");
                isMove = false;
            }
        }

        Debug.DrawRay(transform.position, dir * 3f, Color.red);
        RaycastHit2D hitX = Physics2D.Raycast(transform.position, dir, 3f);
        if(hitX)
        {
            Debug.Log("왜 안댐");
            if(hitX.transform.CompareTag("Ground") && isGround)
            {
                rb.AddForce(Vector2.up*jumpSpeed, ForceMode2D.Impulse);
                Debug.Log("Jump");
            }
        }
    }

    new void Attack()
    {
        isMove = false;
        Debug.Log("Attack");
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach(Collider2D collider in collider2Ds)
        {
            Debug.Log(collider.tag);
            if(collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Monster>().OnDamage(attackDamage);
                if(collider.GetComponent<Monster>().hp < 0)
                {
                    DataManager.Instance.DoppelEx++;
                    LevelUpCheck();
                }
            }
        }
    }

    public override void DoppelgangerMove()
    {
        base.DoppelgangerMove();
        dir = realDir > 0 ? Vector2.right : Vector2.left;


        if(distance > 3000)
        {
            transform.position = Monster.transform.position;
        }
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
            case States.Idle:
            animator.SetBool("isWalk" , false);
            break;
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
            case States.Idle:
                if(realDir != 0) StartStates(States.Chase);
                break;
            case States.Chase:
                if(distance > attackRange * attackRange) StartStates(States.Chase);
                if(distance < (attackRange * attackRange)&& Time.time > attackSpeed) StartStates(States.Attack);
            break;
            case States.Attack:
                if(Time.time > 0.5f) StartStates(States.Chase);
                if(DataManager.Instance.monsters.Count == 0) StartStates(States.Idle);
            break;
        }
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    
}
