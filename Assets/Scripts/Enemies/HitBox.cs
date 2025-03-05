using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    EnemyCtrl enemyCtrl;
    PlayerCtrl playerCtrl;

    [SerializeField] bool isEnemy;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEnemy)
        {
            enemyCtrl = GetComponentInParent<EnemyCtrl>();

            if (collision.CompareTag("Player"))
            {
                PlayerCtrl playerCtrl = collision.GetComponent<PlayerCtrl>();
                playerCtrl.TakeHit(enemyCtrl.damage);
            }
        }
        else
        {
            playerCtrl = GetComponentInParent<PlayerCtrl>();

            if (collision.CompareTag("Enemy"))
            {
                EnemyCtrl enemyCtrl = collision.GetComponent<EnemyCtrl>();
                enemyCtrl.TakeHit(playerCtrl.damage);
            }
        }
    }
}
