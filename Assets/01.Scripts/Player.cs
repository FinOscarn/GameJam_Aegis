using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected bool isGround = true;

    protected Rigidbody2D rb;
    protected StageManager stageManager;

    public GameObject Monster;

    [Header("플레이어 스텟")]
    public float hp;
    public float attackDamage;
    public float defensePower;
    public float attackSpeed;
    public float speed = 5f;
    public float jumpSpeed = 3f;
    public float attackRange;

    public float distance;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stageManager = FindObjectOfType<StageManager>();
        isGround = true;
    }

    public void Update()
    {
        PlayerMove();
        CheckGround();

        Debug.Log(isGround);
    }

    public void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
        transform.Translate(new Vector2(speed*h*Time.deltaTime, 0));

        if(Input.GetButtonDown("Jump") && isGround)
        {
            rb.AddForce(Vector2.up*jumpSpeed, ForceMode2D.Impulse);
        }
    }

    public virtual void DoppelgangerMove()
    {
        Vector2 dir = Monster.transform.position - transform.position;
        distance = dir.sqrMagnitude;
        float realDir = dir.x > 0 ? 1 : -1;

        transform.Translate(new Vector2(realDir * speed * Time.deltaTime,0));
    }

    public void CheckGround()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.8f), 0.3f, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Potal"))
        {
            //다음스테이지 이동 함수 실행
            Debug.Log("포탈포탈");
            stageManager.NextStage();
        }
        if(other.transform.CompareTag("Ground"))
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Ground"))
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        
    }
}
