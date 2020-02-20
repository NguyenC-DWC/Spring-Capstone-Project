using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //Represents the velocity of the playing field.
    private Rigidbody2D fieldVel;
    
    //Speed of the object.
    public float speed;

    //Rigidbody of the weapon.
    [SerializeField]
    private Rigidbody2D weaponRigid;

    void Start()
    {
        fieldVel = GameObject.Find("PlayingField").GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {
        weaponRigid.velocity = fieldVel.velocity + ((Vector2)transform.right * speed);
    }
}
