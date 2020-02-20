using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //Represents the velocity of the playing field.
    private Rigidbody fieldVel;
    
    //Speed of the object.
    public float speed;

    //Rigidbody of the weapon.
    [SerializeField]
    private Rigidbody weaponRigid;

    void Start()
    {
        fieldVel = GameObject.Find("PlayingField").GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {
        weaponRigid.velocity = fieldVel.velocity + (transform.right * speed);
    }
}
