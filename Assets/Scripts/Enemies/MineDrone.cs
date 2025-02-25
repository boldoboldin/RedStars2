using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDrone : EnemyCtrl
{
    [SerializeField] float explosionDistance;

    // Start is called before the first frame update

    public override void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance < explosionDistance)
        {
            ActivateAtk();
        }
    }

    void ActivateAtk()
    {
        anim.SetTrigger("Activate");
    }

    void Explode()
    {
        Vector3 instantiatePos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        
        GameObject fx = Instantiate(sExplosionFX, instantiatePos, transform.rotation);
        Destroy(fx, 3f);
    }
}
