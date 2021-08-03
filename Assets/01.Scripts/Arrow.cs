using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Rigidbody2D rb;

    public GameObject player;

    float speed = 12f;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("mPlayer");
        Vector2 dir = player.transform.position - transform.position;

        rb.AddForce(dir * speed, ForceMode2D.Impulse);
    }

    

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}
