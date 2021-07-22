using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
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
    public float defensePower;
    public float attackSpeed;
    public float moveSpeed;
    public float range;
    public float attackRange;

    public GameObject projectTile;

    Vector2 dir;
    float distance;

    bool isPatrolling;
    bool isAttack;
    bool isMove;

    private void Start() 
    {
        player = FindObjectOfType<Player>();
        StartStates(States.Patrolling);
    }


    private void Update() 
    {
        dir = PlayerObj.transform.position - transform.position;
        distance = dir.sqrMagnitude;
        Debug.LogError(states);

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
        if(isMove) transform.Translate(new Vector2((Random.Range(-1 , 1))*moveSpeed* Time.deltaTime ,0));
    }

    void Chase()
    {
        if(isMove) transform.Translate(dir * moveSpeed* Time.deltaTime);
    }

    void StartStates(States _states)
    {
        states = _states;
        switch(states)
        {
            case States.Patrolling:
            isMove = true;
            Pattrol();
            break;
            case States.Chase:
            isMove = true;
            Chase();
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
            case States.Patrolling:
                if(distance < (range * range)) StartStates(States.Chase);
                //Debug.Log("Pattrol");
            break;
            case States.Chase:
                if(distance > attackRange * attackRange) StartStates(States.Patrolling);
                if(distance < (attackRange * attackRange) && Time.time > attackSpeed) StartStates(States.Attack);
                //Debug.Log("Chase");
            break;
            case States.Attack:
                if(Time.time > 0.5f) StartStates(States.Chase);
                //Debug.Log("attack");
            break;
        }
    }

    void Damage()
    {
        player.hp -= attackDamage;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Damage();
    }
}
