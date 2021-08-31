using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject PlayerHpbar;

    protected bool isGround = true;

    protected Rigidbody2D rb;
    protected StageManager stageManager;

    public GameObject Weapon;

    public Animator animator;
    public SpriteRenderer sr;

    [Header("플레이어 스텟")]
    public float attackDamage;
    public float defensePower;
    public float attackSpeed;
    public float speed = 5f;
    public float jumpSpeed = 3f;
    public float attackRange;

    public float distance;

    public Transform rightAtkPos;
    public Transform leftAtkPos;
    protected Transform pos;

    public Vector2 boxSize;

    public float realDir;
    float savePos;

    public enum PlayerStates
    {
        idle,
        walk,
        attack,
        die
    }

    float angle;
    Vector2 mouse;
    Vector2 target;

    PlayerStates states = PlayerStates.idle;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stageManager = FindObjectOfType<StageManager>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        PlayerHpbar = GameObject.Find("Canvas/Player");

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
        Hpbar();
    }

    void Hpbar()
    {
        PlayerHpbar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.2f ,0));
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

        
        if (h != 0) savePos = h;


        pos = savePos > 0 ? rightAtkPos :  leftAtkPos;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        Weapon.transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
    }

    public virtual void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            states = PlayerStates.attack;

            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach(Collider2D collider in collider2Ds)
            {
                Debug.Log(collider.tag);
                if(collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<LivingEntity>().OnDamage(attackDamage);
                    collider.GetComponent<Monster>().Damaged();
                    if (collider.GetComponent<LivingEntity>().hp < 0)
                    {
                        DataManager.Instance.PlayerEx++;
                        LevelUpCheck();
                    }
                }
            }

        }
    }

    public void LevelUpCheck()
    {
        if(DataManager.Instance.PlayerEx == 12)
        {
            DataManager.Instance.PlayerLv++;
        }
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


    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Potal"))
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("포탈포탈");
                stageManager.NextStage();
            }
        }
        
        if(other.transform.CompareTag("Ground")&& !isGround)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        if(other.transform.CompareTag("Enemy"))
        {
            OnDamaged(other.transform.position);
        }
    }

    public void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 10;

        sr.color = new Color(1,1,1,0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;

        rb.AddForce(new Vector2(dirc, 0)*3f, ForceMode2D.Impulse);
        
        Invoke("OffDamaged", 1f);
    }

    void OffDamaged()
    {
        gameObject.layer = 7;
        sr.color = new Color(1, 1, 1, 1);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.CompareTag("Ground") && !isGround)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
