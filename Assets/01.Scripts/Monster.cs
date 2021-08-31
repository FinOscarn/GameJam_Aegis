using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    public enum MonsterType
    {
        rangedMonster,
        meleeMonster
    }

    public enum States
    {
        Idle,
        Patrolling,
        Chase,
        Attack
    }

    public enum Canon
    {
        other,
        Canon
    }

    public MonsterType monsterType = MonsterType.rangedMonster;
    public States states = States.Chase;

    [Header("플레이어 관련")]
    public GameObject PlayerObj;
    public Player player;

    [Header("스텟")]
    public float attackDamage;
    public float attackSpeed;
    public float moveSpeed;
    
    [Header("방어력")]
    public float defensePower;
    [Header("인식범위")]
    public float range;
    [Header("공격범위")]
    public float attackRange;
    
    public GameObject projectTile;

    Transform pos;
    public Transform rightAtkPos;
    public Transform leftAtkPos;

    public Vector2 boxSize;

    Vector2 dir;
    Rigidbody2D rb;
    public float distance;

    Animator animator;

    SpriteRenderer sr;
    SpriteRenderer sprite;

    bool isPatrolling;
    bool isAttack;
    bool isMove;

    float cooltime = 1.2f;

    float stiffnessTime = 0.1f;

    float timer = 0f;

    public LivingEntity entity;

    private void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        entity = GetComponent<LivingEntity>();
        PlayerObj = player.gameObject;
        rb = GetComponent<Rigidbody2D>();
        StartStates(States.Patrolling);
    }

    private void Update() 
    {
        dir = PlayerObj.transform.position - transform.position;
        distance = dir.sqrMagnitude;
    }

    private void FixedUpdate() 
    {
        CheckStates();
    }

    void Attack()
    {
        if(monsterType == MonsterType.rangedMonster)
        {

            timer += Time.deltaTime;
            
            if(timer > attackSpeed)
            {
                Instantiate(projectTile, transform.position, Quaternion.identity);
                timer = 0;
            }

        }
        else if(monsterType == MonsterType.meleeMonster)
        {
            pos = dir.x > 0 ? rightAtkPos : leftAtkPos;
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach(Collider2D collider in collider2Ds)
            {
                Debug.Log(collider.tag);
                if(collider.CompareTag("Player"))
                {
                    collider.GetComponent<LivingEntity>().OnDamage(attackDamage);
                    collider.GetComponent<Player>().OnDamaged(transform.position);
                }
            }
        }
    }

   

    void Pattrol()
    {
        if(isMove) 
        {
            int randomIndex = Random.Range(-1, 2);

            Debug.Log(randomIndex);

            if(randomIndex == 0)
            {
                isMove = false;
                
            }
            else 
            {
                rb.velocity = new Vector2(randomIndex * moveSpeed, rb.velocity.y);  
            }

            if(randomIndex == -1) sr.flipX = true;
            else if(randomIndex == 1) sr.flipX = false;
        }
    }

    public virtual void Chase()
    {
        float realDir = dir.x > 0 ? 1 : -1;
        if(isMove) 
        {
            rb.velocity = new Vector2(realDir * moveSpeed, rb.velocity.y);
        }

        if(realDir == -1) sr.flipX = true;
        else if(realDir == 1) sr.flipX = false;

    }

    void StartStates(States _states)
    {
        Debug.Log(states);
        states = _states;
        switch(states)
        {
            case States.Idle:
            animator.SetBool("IsWalk", false);
            break;
            case States.Patrolling:
            animator.SetBool("IsWalk", true);
            isMove = true;
            Invoke("Pattrol", cooltime);
            cooltime = Time.time + 2f;
            break;
            case States.Chase:
            animator.SetBool("IsWalk", true);
            isMove = true;
            Chase();
            break;
            case States.Attack:
            animator.SetTrigger("IsAttack");
            animator.SetBool("IsWalk", false);
            isMove = false;
            Attack();
            break;
        } 
    }

    void CheckStates()
    {
        switch(states)
        {
            case States.Patrolling:
                if(distance < (range * range)) StartStates(States.Chase);
                if(Time.time > cooltime) StartStates(States.Patrolling);
            break;
            case States.Chase:
                if(distance > range * range && Time.time > cooltime) StartStates(States.Patrolling);
                if(distance < (attackRange * attackRange) && Time.time > attackSpeed) StartStates(States.Attack);
            break;
            case States.Attack:
                if(Time.time > 1f) StartStates(States.Chase);
            break;
        }
    }

    public void Damaged()
    {
        sr.DOColor(Color.black,0.1f).OnComplete(() => {
            sr.DOColor(Color.white, 0.5f);
        });

        if(entity.hp < 0)
        {
            DataManager.Instance.deadMonsterCount++;

            sr.DOColor(Color.black, 0.1f).OnComplete(() =>{
                sr.DOFade(0f, 0.3f).OnComplete(() => {
                    Destroy(this.gameObject);
                    DataManager.Instance.monsters.RemoveAt(0);
                });
            });
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
       
        
    } 
}
