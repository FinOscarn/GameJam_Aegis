using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isGround = true;
    bool isJump = true;

    Rigidbody2D rb;
    StageManager stageManager;

    [Header("플레이어 스텟")]
    public float hp;
    public float attackDamage;
    public float defensePower;
    public float attackSpeed;
    public float speed = 5f;
    public float jumpSpeed = 3f;
    public float attackRange;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stageManager = FindObjectOfType<StageManager>();
        isGround = true;
    }

    void Update()
    {
        PlayerMove();
        CheckGround();

        Debug.Log(isGround);

    }

    void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
        transform.Translate(new Vector2(speed*h*Time.deltaTime, 0));

        if(Input.GetButtonDown("Jump") && isGround)
        {
            rb.AddForce(Vector2.up*jumpSpeed, ForceMode2D.Impulse);
            isJump = true;
        }
    }

    void CheckGround()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.4f), 0.3f, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Potal"))
        {
            //다음스테이지 이동 함수 실행
            Debug.Log("포탈포탈");
            stageManager.NextStage();
        }
    }
}
