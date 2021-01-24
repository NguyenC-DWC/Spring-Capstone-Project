using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTripleSpread : EnemyWeapon
{
    public override void fireWeapon(Transform enemyTransform)
    {
        Instantiate(enemyBullet, enemyTransform.position, Quaternion.identity);
        Instantiate(enemyBullet, enemyTransform.position, Quaternion.Euler(0,0,15));
        Instantiate(enemyBullet, enemyTransform.position, Quaternion.Euler(0,0,-15));
    }
}
