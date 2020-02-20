using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //Represents the playing field.
    private GameObject playingField;

    //Rigidbody of the object.
    [SerializeField]
    private Rigidbody2D enemyRigid;

    //Represents the enemy bullet.
    public GameObject enemyBullet;

    //Determines if the enemy is active.
    private bool isActive = false;

    //The point in time the enemy is active.
    private float activeTime;

    //Speed of the enemy.
    public float speed;

    //Represents the movement path of the enemy.
    public enum movementPath{ Straight, Wave, Zigzag, UTurn, StopChase};
    public movementPath pathSet;

    //Represents the point at which the enemy changes direction depending on its position in the field, and what path they are taking.
    public int xChange;
    private bool reachedX = false;

    //Determines if the initial y direction is flipped. 
    public bool yFlipped;

    //Determines if the enemy will shoot a bullet and where, and if it shoots a bullet after death.
    public bool shoot;
    public float shootX;
    public bool revengeBullet;

    void Start()
    {
        playingField = GameObject.Find("PlayingField");
    }

    void FixedUpdate()
    {
        if(isActive)
        {
            move();
            attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Determines if the ship has hit the player weapon.
        if (collision.CompareTag("PlayerWeapon"))
        {
            if (revengeBullet)
            {
                Instantiate(enemyBullet, transform.position, transform.rotation);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        //If the enemy has reached the playing field, have the enemy be active.
        if (!isActive)
        {
            if (collision.CompareTag("PlayingField"))
            {
                isActive = true;
                activeTime = Time.time;
            }
        }
    }

    private void move()
    {
        int yVel = 1;
        if (yFlipped)
        {
            yVel = -1;
        }

        Vector2 enemyMovement = transform.right * -speed;

        switch (pathSet)
        {
            //Moves the enemy in a wave motion.
            case movementPath.Wave:
                enemyMovement.y = moveWave(yVel);
                break;

            //Have the enemy move to the middle of the screen, then go backwards.
            case movementPath.Zigzag:
                moveZigZag(ref enemyMovement, yVel);
                break;
            
            //Have the enemy turn 180 degrees.
            case movementPath.UTurn:
                moveUTurn(yVel);
                break;
            case movementPath.StopChase:
                moveStopChase(ref enemyMovement);
                break;

        }

        enemyRigid.velocity = playingField.GetComponent<Rigidbody2D>().velocity + enemyMovement;
    }

    private bool reachedXPosition()
    {
        if(reachedX || transform.position.x < (playingField.transform.position.x + xChange))
        {
            reachedX = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    private float moveWave(int yVel)
    {
        return Mathf.Cos((Time.time - activeTime) * 3) * 2 * yVel;
    }

    private void moveZigZag(ref Vector2 enemyMovement, int yVel)
    {
        if(reachedXPosition())
        {
            enemyMovement.x = speed * 1.5f;
            enemyMovement.y = speed * .5f * yVel;
        }
    }

    private void moveUTurn(int yVel)
    {
        if (reachedXPosition())
        {
            //Rotate 180 degrees then stay at that angle.
            Quaternion target = Quaternion.AngleAxis(180,Vector3.forward * yVel);
            transform.rotation = Quaternion.Lerp(transform.rotation,target, 6 * Time.deltaTime);
        }
    }

    private void moveStopChase(ref Vector2 enemyMovement)
    {
        if (reachedXPosition())
        {
            enemyMovement.x = 0;
        }
    }

    private void attack()
    {
        switch (pathSet)
        {
            //Shoot when the ship turns around.
            case movementPath.Zigzag:
            case movementPath.UTurn:
                if(shoot && reachedX)
                {
                    Instantiate(enemyBullet, transform.position, transform.rotation);
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
