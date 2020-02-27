using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //Represents the playing field.
    private GameObject playingField;

    //What weapon the enemy has.
    public EnemyWeapon enemyWeapon;

    //Determines if the enemy will shoot a bullet, when, and if it shoots a bullet after death.
    public bool shoot;
    public float shootX;
    //public bool revengeBullet;

    //When the enemy will shoot will depend on the movement pattern.
    private EnemyMove movementPattern;

    private void Start()
    {
        playingField = GameObject.Find("PlayingField");
        movementPattern = GetComponent<EnemyMove>();
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Determines if the ship has hit the player weapon.
        if (collision.CompareTag("PlayerWeapon"))
        {
            if(revengeBullet)
            {
                Instantiate(enemyBullet, transform.position, transform.rotation);
            }
        }
    }
    */

    public void attack()
    {
        switch (movementPattern.pathSet)
        {
            //Shoot when the ship turns around.
            case EnemyMove.movementPath.Zigzag:
                if (shoot && movementPattern.reachedX)
                {
                    enemyWeapon.fireWeapon(transform);
                    shoot = false;
                }
                break;

            //Shoot if it reaches the specified X coordinate.
            default:
                if (shoot && transform.position.x < (playingField.transform.position.x + shootX))
                {
                    enemyWeapon.fireWeapon(transform);
                    shoot = false;
                }
                break;
        }

    }
}
