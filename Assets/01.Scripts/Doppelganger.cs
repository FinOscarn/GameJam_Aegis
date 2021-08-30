using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doppelganger : MonoBehaviour
{

    public Animator animator;
    public SpriteRenderer sr;

    public GameObject player; 
    public float playerDis;

    public GameObject Monster;

    bool playerChase;
    bool monsterChase;
    bool isGround;

    public bool isMove = true;

    public GameObject DoppelHpbar;

    Rigidbody2D rb;

    public Vector2 boxSize;

    public float realDir;

    [Header("도플갱어 스텟")]
    public float hp;
    public float attackDamage;
    public float defensePower;
    public float attackSpeed;
    public float speed = 5f;
    public float jumpSpeed = 3f;
    public float attackRange;
    public float maxExp = 15000; 

    public float distance;


    public Transform rightAtkPos;
    public Transform leftAtkPos;
    protected Transform pos;

    int monDir;
    Vector2 MonsterDir;

    //플레이어를 공격하는 조건
    bool isCheckAtk;


    public enum DoppleStates
    {
        Idle,
        Chase,
        Attack,
        Jump
    }
    protected DoppleStates doppleStates = DoppleStates.Chase;

    public Vector2 dir;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DoppelHpbar = GameObject.Find("Canvas/Doppelganger");
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Update() 
    {
        if(DataManager.Instance.monsters.Count > 0 && !isCheckAtk)
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
        MonsterDistance();
        CheckGround();
        JumpPlatform();
        //Debug.Log(doppleStates);
    }

    void Hpbar()
    {
        DoppelHpbar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.2f ,0));
    }
    
    private void FixedUpdate() 
    {
        CheckStates();
    }

    void CheckGround()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.8f), 0.3f, 1 << LayerMask.NameToLayer("Ground"));
    }

    void PlayerAtk()
    {
        if(isCheckAtk)
        {
            Monster = player;
            //TODO : 플레이어랑 도플갱어 충돌체크 on off 해주기, 플레이어가 죽는다면 마지막 체크포인트로 이동 UI 띄워주기. 저장도 만들기
            
        }
    }


    void JumpPlatform()
    {
        Debug.DrawRay(transform.position, Vector2.up * 2f, Color.red);
        RaycastHit2D hitY = Physics2D.Raycast(transform.position, Vector2.up, 2f);
        if(hitY)
        {
            if(hitY.transform.CompareTag("celling") && isGround)
            {
                rb.AddForce(Vector2.up*jumpSpeed, ForceMode2D.Impulse);
                Debug.Log("Jump");
                isMove = false;
            }
        }

        Debug.DrawRay(transform.position, Vector2.right * 1f, Color.red);
        RaycastHit2D hitX = Physics2D.Raycast(transform.position, Vector2.right, 1f);
        if(hitX)
        {
            Debug.LogError("DKDKDK");
            if(hitX.transform.CompareTag("Ground") && isGround)
            {   
                rb.AddForce(Vector2.up*jumpSpeed, ForceMode2D.Impulse);
                Debug.Log("Jump");
            }
        }
    }

    public void LevelUpCheck()
    {
        if(DataManager.Instance.DoppelEx >= maxExp)
        {
            Debug.Log("레벨업!!");
            DataManager.Instance.DoppelgangerLv++;
            DataManager.Instance.DoppelEx += DataManager.Instance.DoppelEx - maxExp;
        }
    }

    public virtual void Attack()
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

    public void MonsterDistance()
    {
        MonsterDir = Monster.transform.position - transform.position;
        distance = MonsterDir.sqrMagnitude;
    }
    
    public void DoppelgangerMove()
    {
        if (isMove)
        {
            CheckEnemy();
            Debug.LogWarning(monsterChase);
            if(monsterChase)
            {
                
                monDir = Monster.transform.position.x - transform.position.x > 0 ? 1 : -1;
                sr.flipX = monDir > 0 ? false : true;
                pos = dir.x > 0 ? rightAtkPos : leftAtkPos;
            
                rb.velocity = new Vector2(monDir * speed ,rb.velocity.y);

                Debug.Log(monDir);
            }
            else
            {
                dir = monDir > 0 ? Vector2.right : Vector2.left;

                if(distance > 3000)
                {
                    transform.position = Monster.transform.position;
                }
            }
        }
    }

    public void CheckEnemy()
    {
        Vector2 enemyDir = Monster.transform.position - transform.position;
        float enemyDis = enemyDir.sqrMagnitude;

        if (enemyDis < 3000) monsterChase = true;
        else monsterChase = false;
    }

    void Teleport()
    {
        int curTr = Mathf.RoundToInt(transform.position.x);
        int monTr = Mathf.RoundToInt(Monster.transform.position.x);

        if(curTr == monTr)                                                                                                            
        {
            Debug.Log("텔포합니다");
            transform.position = Monster.transform.position;
        }
    }

    public virtual void StartStates(DoppleStates _states)
    {
        doppleStates = _states;
        switch(doppleStates)
        {
            case DoppleStates.Idle:
            animator.SetBool("isWalk" , false);
            break;
            case DoppleStates.Chase:
            isMove = true;
            animator.SetBool("isWalk", true);
            DoppelgangerMove();
            Teleport();
                speed = 5;
                break;
            case DoppleStates.Attack:
            animator.SetTrigger("IsAttack");
            isMove = false;
            speed = 0;
            attackSpeed = Time.time + 1f; 
            Attack();
            break;
        }
    }

    public virtual void CheckStates()
    {
        switch(doppleStates)
        {
            case DoppleStates.Idle:
                if(realDir != 0) StartStates(DoppleStates.Chase);
                break;
            case DoppleStates.Chase:
                if(distance > attackRange * attackRange) StartStates(DoppleStates.Chase);
                if(distance < (attackRange * attackRange)) StartStates(DoppleStates.Attack);
            break;
            case DoppleStates.Attack:
                if (distance > (attackRange * attackRange)) StartStates(DoppleStates.Chase); Debug.Log("체이스로 돌아갑니다");
                if (DataManager.Instance.monsters.Count == 0) StartStates(DoppleStates.Idle);
                if(Time.time > 0.5f && Time.time > attackSpeed) StartStates(DoppleStates.Attack); Debug.Log("어택으로 돌아갑니다");
                break;
        }
    }

    
}
