using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    Vector2 target;
    Vector2 curTr;
    public Transform pos;

    public GameObject projectTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            curTr = transform.position;

            Vector2 dir = target - curTr;
            float z = Mathf.Atan2(dir.y , dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,z);

            Instantiate(projectTile, pos.position, transform.rotation);
        }
    }
}
