using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecDrone : EnemyCtrl
{
    [SerializeField] float chaseDistance, chargeDistance;

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

        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new(-1, 1);
        }

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new(1, 1);
        }
    }

    void Patrol()
    {
        
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, spd * Time.deltaTime);
    }

    void ChargeAtk()
    {
        anim.SetTrigger("Charge");
    }
}
