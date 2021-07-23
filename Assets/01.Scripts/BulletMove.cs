using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public GameObject player;

    protected float speed = 4f;
    protected Vector2 dir;

    public virtual void Start() 
    {
        player = GameObject.Find("Player");
        dir = player.transform.position - transform.position;
    }
    
    void Update() 
    {
        transform.Translate( dir * speed * Time.deltaTime);
    }

    public virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().OnDamaged(this.transform.position);
        }
        else if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Monster>().OnDamage(other.GetComponent<Player>().attackDamage);
        }
        Destroy(this.gameObject);
    }
}
