using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected bool isGround = true;

    protected Rigidbody2D rb;
    protected StageManager stageManager;

    public GameObject Monster;

    public Animator animator;
    public SpriteRenderer sr;

    [Header("플레이어 스텟")]
    public float hp;
    public float attackDamage;
    public float defensePower;
    public float attackSpeed;
    public float speed = 5f;
    public float jumpSpeed = 3f;
    public float attackRange;

    public float distance;

    public enum PlayerStates
    {
        idle,
        walk,
        attack,
        die
    }
    PlayerStates states = PlayerStates.idle;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stageManager = FindObjectOfType<StageManager>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        isGround = true;
    }

    public void FixedUpdate() 
    {
        PlayerMove();
    }

    public virtual void Update()
    {   
        
        CheckGround();
        Attack();
        SetAnim();

    }

    public void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
        rb.velocity = new Vector2( h* speed ,rb.velocity.y);

        if(h != 0)
        {
            states = PlayerStates.walk;
        }
        else
        {
            states = PlayerStates.idle;
        }
        
        if(h < 0)
        {
            sr.flipX = true;
        }
        else if (h > 0)
        {
            sr.flipX = false;
        }

        if(Input.GetButtonDown("Jump") && isGround)
        {
            rb.AddForce(Vector2.up*jumpSpeed, ForceMode2D.Impulse);
        }
    }

    public virtual void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //상속해서 어택하면댐
            states = PlayerStates.attack;
        }
        
    }

    public virtual void DoppelgangerMove()
    {
        Vector2 dir = Monster.transform.position - transform.position;
        distance = dir.sqrMagnitude;
        float realDir = dir.x > 0 ? 1 : -1;

        rb.velocity = new Vector2( realDir * speed ,rb.velocity.y);
    }

    public void CheckGround()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.8f), 0.3f, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void SetAnim()
    {
        switch(states)
        {
            case PlayerStates.idle:
            animator.SetBool("isWalk" , false);
            break;
            case PlayerStates.walk:
            animator.SetBool("isWalk", true);
            break;
            case PlayerStates.attack:
            animator.SetTrigger("IsAttack");
            break;
            case PlayerStates.die:
            animator.SetTrigger("isDie");
            break;
        }
    }

    public void Die()
    {
        if(hp <= 0)
        {
            states = PlayerStates.die;
        }
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
