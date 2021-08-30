using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rDoppelganger : Doppelganger
{
    void Awake() 
    {
        InitStat();
    }

    void InitStat()
    {
        attackSpeed = 2f;
        attackRange = 6f;
        jumpSpeed = 5f;
    }

    public override void Attack()
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

    public override void CheckStates()
    {
        switch(doppleStates)
        {
            case DoppleStates.Chase:
                if(distance > attackRange * attackRange) StartStates(DoppleStates.Chase);
                if(distance < (attackRange * attackRange)) StartStates(DoppleStates.Attack);
                Debug.Log("Chase");
            break;
            case DoppleStates.Attack:
                if(Time.time > 0.5f) StartStates(DoppleStates.Chase);
                Debug.Log("attack");
            break;
        }
    }
}
