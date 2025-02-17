using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] DynamicJoystick dynamicJoystick;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] protected int hp, damage;
    [SerializeField] float maxSpd;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = dynamicJoystick.Horizontal;
        float inputY = dynamicJoystick.Vertical;

        Vector2 direction = new Vector2(inputX, inputY);

        float currentSpd = Mathf.Lerp(0, maxSpd, direction.magnitude);

        rb.velocity = direction * currentSpd;

        anim.SetFloat("moveInput", currentSpd);

        if (inputX > 0)
        {
            transform.localScale = new (-1,1);
        }

        if (inputX < 0)
        {
            transform.localScale = new(1,1);
        }

        if (inputY > 0)
        {
            //
        }

        if (inputY < 0)
        {
            //
        }
    }

    public void TakeShock(int damage)
    {
        TakeHit(damage);
    }

    public void TakeHit(int damage)
    {
        Debug.Log("Deu dano");

        anim.SetTrigger("electrocute");

        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("F");
        //anim.SetTrigger("die");
    }
}

