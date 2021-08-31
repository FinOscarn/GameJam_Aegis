using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour , IDamageable
{
    [SerializeField]
    public float hp;

    public void OnDamage(float damage)
    {
        hp -= damage;

        //if(hp <= 0)
        //{


        //}
    }

}
