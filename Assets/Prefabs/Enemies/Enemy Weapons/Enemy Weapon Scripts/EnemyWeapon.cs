using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    //Represents the enemy bullet.
    public GameObject enemyBullet;

    public virtual void fireWeapon(Transform enemyTransform)
    {
        Instantiate(enemyBullet, enemyTransform.position, Quaternion.identity);
    }
}
