using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    //Represents the velocity of the playing field.
    private Rigidbody fieldVel;

    //Speed of the object the speed of the playing field.
    public float speed;

    [SerializeField]
    private Rigidbody weaponRigid;

    void Start()
    {
        Transform player = GameObject.Find("PlayerShip").transform;
        fieldVel = GameObject.Find("PlayingField").GetComponent<Rigidbody>();
        transform.right = player.position - transform.position;
        weaponRigid.velocity = fieldVel.velocity + (transform.right * speed);
    }
}
