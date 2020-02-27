using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //Represents the playing field.
    private GameObject playingField;

    //Rigidbody of the object.
    private Rigidbody2D enemyRigid;

    //Determines if the enemy is active, and for how long.
    private bool isActive = false;
    private float activeTime;

    //Speed of the enemy.
    public float speed;

    //Represents the movement path of the enemy.
    public enum movementPath { Straight, Wave, Zigzag, StopChase };
    public movementPath pathSet;

    //Represents the point at which the enemy changes direction depending on its position in the field, and what path they are taking.
    public int xChange;
    public bool reachedX = false;

    //Determines if the initial y direction is flipped. 
    public bool yFlipped;

    void Start()
    {
        playingField = GameObject.Find("PlayingField");
        enemyRigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

    private bool reachedXPosition()
    {
        if (reachedX || transform.position.x < (playingField.transform.position.x + xChange))
        {
            reachedX = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void move()
    {
        if(isActive)
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
                    enemyMovement.y = Mathf.Cos((Time.time - activeTime) * 3) * 2 * yVel;
                    break;

                //Have the enemy move to the middle of the screen, then go backwards.
                case movementPath.Zigzag:
                    if (reachedXPosition())
                    {
                        enemyMovement.x = speed * 1.5f;
                        enemyMovement.y = speed * .5f * yVel;
                    }
                    break;

                case movementPath.StopChase:
                    if (reachedXPosition())
                    {
                        enemyMovement.x = 0;
                    }
                    break;
            }

            enemyRigid.velocity = playingField.GetComponent<Rigidbody2D>().velocity + enemyMovement;
        }
       
    }
}
