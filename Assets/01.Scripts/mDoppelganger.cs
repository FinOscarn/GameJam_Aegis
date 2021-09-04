using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mDoppelganger : Doppelganger
{
    public override void Awake()
    {
        base.Awake();
        InitStat();
    }
    void InitStat()
    {
        attackSpeed = 2f;
        attackRange = 2f;
        jumpSpeed = 5f;
        attackDamage = 2f;
    }

    public override void Attack()
    {
        isMove = false;
        Debug.Log("Attack");
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach(Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Monster>().Damaged();
                collider.GetComponent<LivingEntity>().OnDamage(attackDamage);
                if (collider.GetComponent<LivingEntity>().hp <= 0)
                {
                    DataManager.Instance.DoppelEx++;
                    LevelUpCheck();
                }
            }

         
        }
    }
}
