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
    bool isCheckAtk = false;
    //몬스터가 존재하는지
    bool isLiveMonster = true;

    int layerMask = 1 << 6;


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
        PlayerAtk();
        PlayerChase();  
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
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(-0.4f, -0.8f), 0.3f, 1 << LayerMask.NameToLayer("Ground"));
    }

    void PlayerAtk()
    {
        if (isCheckAtk)
        {
            Monster = player;
        }
    }

    void PlayerChase()
    {
        if (DataManager.Instance.monsters == null)
        {
            Vector2 dir = transform.position - player.transform.position;
            float dist = dir.sqrMagnitude;
            if(dist > 20)
            {
                float direction = transform.position.x - player.transform.position.x > 0 ? 1 : -1;
                sr.flipX = direction > 0 ? false : true;
                rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            }
        }
    }

    void JumpPlatform()
    {
        Debug.DrawRay(transform.position , Vector2.right * 1f, Color.red);
        RaycastHit2D hitX = Physics2D.Raycast(transform.position , Vector2.right, 1f, layerMask);
        if(hitX)
        {
            Debug.Log(hitX.collider.gameObject);

            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            Debug.Log("Jump");
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
                collider.GetComponent<Monster>().Damaged();
                if(collider.GetComponent<LivingEntity>().hp < 0)
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
            monDir = Monster.transform.position.x - transform.position.x > 0 ? 1 : -1;
            sr.flipX = monDir > 0 ? false : true;
            pos = dir.x > 0 ? rightAtkPos : leftAtkPos;
            
            rb.velocity = new Vector2(monDir * speed ,rb.velocity.y);

            Debug.Log(monDir);
            dir = monDir > 0 ? Vector2.right : Vector2.left;
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere((Vector2)transform.position + new Vector2(-0.4f, -0.8f), 0.3f);
            
    }
}
