using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public GameObject target;

    protected float speed = 4f;
    protected Vector2 dir;

    rPlayer rplayer;

    public virtual void Start() 
    {
        dir = target.transform.position - transform.position;
        rplayer = GetComponent<rPlayer>();
    }
    
    void Update() 
    {
        transform.Translate( dir * speed * Time.deltaTime);
    }

    public virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Monster>().OnDamage(rplayer.attackDamage);
        }
        Destroy(this.gameObject);
    }
}
