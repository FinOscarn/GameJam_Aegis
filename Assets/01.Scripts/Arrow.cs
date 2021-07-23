using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : BulletMove
{

    Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce((dir * speed) , ForceMode2D.Impulse);
    }

    void Update()
    {
        
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
