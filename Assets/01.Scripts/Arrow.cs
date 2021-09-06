using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Rigidbody2D rb;

    public GameObject player;

    float speed = 12f;
    float damage = 15f;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("mPlayer");
        Vector2 dir = player.transform.position - transform.position;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);

        rb.AddForce(dir * speed, ForceMode2D.Impulse);
    }

    

    public void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<LivingEntity>().OnDamage(damage);
    }
}
