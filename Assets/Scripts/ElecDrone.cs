using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecDrone : EnemyCtrl
{
    [SerializeField] float chaseDistance, chargeDistance;

    //[SerializeField] float patrolArea;
    //private Vector2 patrolTarget;
    //private float patrolTimer;

    // Update is called once per frame
    public override void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < chargeDistance)
        {
            ChargeAtk();
        }
        else if (distance < chaseDistance)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        /*if (patrolTimer <= 0)
        {
            patrolTarget = new Vector2(transform.position.x + Random.Range(-patrolArea, patrolArea),
                                       transform.position.y + Random.Range(-patrolArea, patrolArea));

            patrolTimer = Random.Range(5f, 7f);
        }

        transform.position = Vector2.MoveTowards(transform.position, patrolTarget, spd * Time.deltaTime);

        if (patrolTimer > 0)
        {
            patrolTimer -= Time.deltaTime;
        }

        if (patrolTarget.x > transform.position.x)
        {
            transform.localScale = new(-1, 1);
        }

        if (patrolTarget.x < transform.position.x)
        {
            transform.localScale = new(1, 1);
        }*/
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, spd * Time.deltaTime);

        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new(-1, 1);
        }

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new(1, 1);
        }
    }

    void ChargeAtk()
    {
        anim.SetTrigger("Charge");
    }
}
