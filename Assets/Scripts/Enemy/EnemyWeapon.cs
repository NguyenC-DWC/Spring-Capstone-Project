using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    //Represents the velocity of the playing field.
    private Rigidbody2D fieldVel;

    //Speed of the object the speed of the playing field.
    public float speed;

    [SerializeField]
    private Rigidbody2D weaponRigid;

    void Start()
    {
        Transform player = GameObject.Find("PlayerShip").transform;
        fieldVel = GameObject.Find("PlayingField").GetComponent<Rigidbody2D>();
        transform.right = player.position - transform.position;

        Vector2 weaponVelocity = transform.right * speed;
        weaponRigid.velocity = fieldVel.velocity + weaponVelocity;
    }
}
