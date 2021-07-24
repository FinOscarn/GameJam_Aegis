using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Rigidbody2D rb;

    float speed = 12f;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
    }

    

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}
