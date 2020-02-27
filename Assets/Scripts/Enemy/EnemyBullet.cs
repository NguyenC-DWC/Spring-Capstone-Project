using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Represents the velocity of the playing field.
    private Rigidbody2D fieldVel;

    //Rigidbody of the bullet.
    private Rigidbody2D weaponRigid;

    //Represents the movement path of the bullet.
    public enum movementPath {PlayerAim,Straight};
    public movementPath pathSet;

    //Speed of the enemy bullet.
    public float speed;
    
    void Start()
    {
        weaponRigid = GetComponent<Rigidbody2D>();
        fieldVel = GameObject.Find("PlayingField").GetComponent<Rigidbody2D>();
        Vector2 weaponVelocity;

        switch (pathSet)
        {
            //Moves the enemy in a wave motion.
            case movementPath.PlayerAim:
                Transform player = GameObject.Find("PlayerShip").transform;
                transform.right = player.position - transform.position;
                weaponVelocity = transform.right * speed;
                weaponRigid.velocity = fieldVel.velocity + weaponVelocity;
                break;
            default:
                weaponVelocity = transform.right * -speed;
                weaponRigid.velocity = fieldVel.velocity + weaponVelocity;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
