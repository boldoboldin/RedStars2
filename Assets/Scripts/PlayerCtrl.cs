using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] DynamicJoystick moveJoystick, atkJoystick;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] protected int hp;
    [SerializeField] float maxSpd;
    public int damage;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attack();
        Move();
    }

    private void Move()
    {
        float inputX = moveJoystick.Horizontal;
        float inputY = moveJoystick.Vertical;

        Vector2 direction = new Vector2(inputX, inputY);

        float currentSpd = Mathf.Lerp(0, maxSpd, direction.magnitude);

        if (canMove == true)
        {
            rb.velocity = direction * currentSpd;

            anim.SetFloat("moveInput", currentSpd);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (inputX > 0)
        {
            transform.localScale = new(-1, 1);
        }

        if (inputX < 0)
        {
            transform.localScale = new(1, 1);
        }
    }

    private void Attack()
    {
        float inputX = atkJoystick.Horizontal;
        float inputY = atkJoystick.Vertical;

        if (inputX != 0 || inputY != 0)
        {      
            Vector2 attackDirection = new Vector2(inputX, inputY).normalized;

            if (inputX > 0)
            {
                transform.localScale = new(-1, 1);
            }

            if (inputX < 0)
            {
                transform.localScale = new(1, 1);
            }

            if (inputY > 0)
            {
                anim.SetTrigger("highAtk");
            }

            if (inputY < 0)
            {
                anim.SetTrigger("lowAtk");
            }
        }
    }

    public void TakeShock(int damage)
    {
        TakeHit(damage);
    }

    public void TakeHit(int damage)
    {
        anim.SetTrigger("electrocute");

        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    public void MoveCtrl(int canMove)
    {
        if(canMove == 0)
        {
            this.canMove = false;
        }
        else
        {
            this.canMove = true;
        }
    }

    private void Die()
    {
        //anim.SetTrigger("die");
    }
}

