using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //Represents the playing field.
    private GameObject playingField;

    //Represents the enemy bullet.
    public GameObject enemyBullet;

    //Determines if the enemy will shoot a bullet, when, and if it shoots a bullet after death.
    public bool shoot;
    public float shootX;
    public bool revengeBullet;

    //When the enemy will shoot will depend on the movement pattern.
    private EnemyMove movementPattern;

    private void Start()
    {
        playingField = GameObject.Find("PlayingField");
        movementPattern = GetComponent<EnemyMove>();
    }

    private void OnDestroy()
    {
        if (revengeBullet)
        {
            Instantiate(enemyBullet, transform.position, transform.rotation);
        }
    }

    public void attack()
    {
        switch (movementPattern.pathSet)
        {
            //Shoot when the ship turns around.
            case EnemyMove.movementPath.Zigzag:
                if (shoot && movementPattern.reachedX)
                {
                    Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    shoot = false;
                }
                break;

            //Shoot if it reaches the specified X coordinate.
            default:
                if (shoot && transform.position.x < (playingField.transform.position.x + shootX))
                {
                    Instantiate(enemyBullet, transform.position, transform.rotation);
                    shoot = false;
                }
                break;
        }

    }
}
