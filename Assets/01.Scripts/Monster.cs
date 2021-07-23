using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Monster : MonoBehaviour, IDamageable
{
    public enum MonsterType
    {
        rangedMonster,
        meleeMonster
    }

    public enum States
    {
        Patrolling,
        Chase,
        Attack
    }

    public MonsterType monsterType = MonsterType.rangedMonster;
    public States states = States.Chase;

    [Header("플레이어 관련")]
    public GameObject PlayerObj;
    public Player player;

    [Header("스텟")]
    public float hp;
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

    Vector2 dir;
    Rigidbody2D rb;
    float distance;

    Animator animator;

    SpriteRenderer sr;

    bool isPatrolling;
    bool isAttack;
    bool isMove;

    float cooltime = 1.2f;

    float stiffnessTime = 0.1f;

    private void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
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
            //원거리 몹
            Instantiate(projectTile, transform.position, Quaternion.identity);
        }
        else if(monsterType == MonsterType.meleeMonster)
        {
            //근거리 몹
        }
    }

    void Pattrol()
    {
        if(isMove) 
        {
            int randomIndex = Random.Range(-1, 1);

            if(randomIndex == 0)
            {
                isMove = false;
            }
            else 
            {
                rb.velocity = new Vector2(randomIndex * moveSpeed, 0);  
            }
        }
    }

    void Chase()
    {
        float realDir = dir.x > 0 ? 1 : -1;
        if(isMove) rb.velocity = dir.normalized * moveSpeed;
        if(realDir == -1) sr.flipX = true;
        else if(realDir == 1) sr.flipX = false;
    }

    void StartStates(States _states)
    {
        states = _states;
        switch(states)
        {
            case States.Patrolling:
            isMove = true;
            Invoke("Pattrol", cooltime);
            cooltime = Time.time + 1f;
            Debug.Log("Pattrol");
            break;
            case States.Chase:
            animator.SetBool("IsWalk", true);
            isMove = true;
            Debug.Log("Chase");
            Chase();
            break;
            case States.Attack:
            animator.SetTrigger("IsAttack");
            isMove = false;
            Debug.Log("Attack");
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
                //Debug.Log("Pattrol");
            break;
            case States.Chase:
                if(distance > attackRange * attackRange && Time.time > cooltime) StartStates(States.Patrolling);
                if(distance < (attackRange * attackRange) && Time.time > attackSpeed) StartStates(States.Attack);
                //Debug.Log("Chase");
            break;
            case States.Attack:
                if(Time.time > 1f) StartStates(States.Chase);
                //Debug.Log("attack");
            break;
        }
    }

    public void OnDamage(float playDamage)
    {
        hp -= playDamage;

        if(hp < 0)
        {
            //sr.color = 
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
       
        
    }
}
