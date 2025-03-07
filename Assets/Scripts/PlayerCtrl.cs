using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] DynamicJoystick leftJoystick, rightJoystick;

    private Rigidbody2D rb;
    private Animator anim, weaponAnim;

    [SerializeField] private GameObject weapon, shotPos, shotFX, sparkFX, bulletMarkFX;

    [SerializeField] private int hp;
    [SerializeField] float maxSpd, shotTimer;
    public float currentShotTimer = 0f;
    public int damage;

    private bool isFliped = false, canMove = true;

    public LayerMask ignoreLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        weaponAnim = weapon.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WeaponMove();
        Move();
    }

    private void Move()
    {
        float inputX = leftJoystick.Horizontal;
        float inputY = leftJoystick.Vertical;

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
            isFliped = true;
        }

        if (inputX < 0)
        {
            transform.localScale = new(1, 1);
            isFliped = false;
        }
    }

    private void WeaponMove()
    {
        float inputX = rightJoystick.Horizontal;
        float inputY = rightJoystick.Vertical;

        Vector2 direction = isFliped ? new Vector2(inputX, inputY) : new Vector2(-inputX, -inputY);

        if (direction.magnitude > 0)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            angle = Mathf.Clamp(angle, -90f, 90f);

            weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, Quaternion.Euler(0, 0, angle), maxSpd * Time.deltaTime);
        }

        if (inputX != 0)
        {
            if (currentShotTimer <= shotTimer)
            {
                currentShotTimer += 1 * Time.deltaTime;
            }
            else
            {
                Shoot();
                currentShotTimer = 0f;
            }
            
            if (inputX > 0)
            {
                transform.localScale = new(-1, 1);
                isFliped = true;
            }

            if (inputX < 0)
            {
                transform.localScale = new(1, 1);
                isFliped = false;
            }
        }
    }

    private void Shoot()
    {
        Vector2 direction = isFliped ? weapon.transform.right : -weapon.transform.right;

        GameObject _shotFX = Instantiate(shotFX, shotPos.transform.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _shotFX.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Destroy(_shotFX, 1f);

        weaponAnim.SetTrigger("shoot");

        RaycastHit2D hit = Physics2D.Raycast(weapon.transform.position, direction, Mathf.Infinity, ~ignoreLayer);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyCtrl enemyCtrl = hit.collider.GetComponent<EnemyCtrl>();

                enemyCtrl.TakeHit(damage); 
            }
            else
            {
                GameObject _bulletMarkFX = Instantiate(bulletMarkFX, hit.transform.position, Quaternion.identity);
                Destroy(_bulletMarkFX, 2f);
            }

            GameObject _sparkFX = Instantiate(sparkFX, hit.transform.position, Quaternion.identity);
            Destroy(_sparkFX, 1f);
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

