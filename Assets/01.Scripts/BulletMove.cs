using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public GameObject player;

    float speed = 8f;
    Vector2 dir;

    private void Start() 
    {
        player = GameObject.Find("Player");
        dir = player.transform.position - transform.position;
    }
    
    void Update() 
    {
        transform.Translate( dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Destroy(this.gameObject);
    }
}
