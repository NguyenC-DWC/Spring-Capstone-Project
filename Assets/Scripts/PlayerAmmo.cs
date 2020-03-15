using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    //Represents the velocity of the playing field.
    private Rigidbody2D fieldVel;

    //Rigidbody of the bullet.
    private Rigidbody2D weaponRigid;

    //Represents the movement path of the bullet.
    public enum movementPath { Straight };
    public movementPath pathSet;

    //Speed of the bullet.
    public float speed;

    //Determines the lifetime of the bullet.
    public bool hasLifeTime;
    public float lifeTime;
    private float startTime;

    void Start()
    {
        weaponRigid = GetComponent<Rigidbody2D>();
        fieldVel = GameObject.Find("PlayingField").GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    void FixedUpdate()
    {   
        Vector2 weaponVelocity = transform.right * speed;
        weaponRigid.velocity = fieldVel.velocity + weaponVelocity;
        if(hasLifeTime && (Time.time - startTime) > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
