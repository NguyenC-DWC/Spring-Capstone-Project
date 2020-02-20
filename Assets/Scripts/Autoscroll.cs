using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoscroll : MonoBehaviour
{
    //Speed of the playing field.
    public float speed;

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = transform.right * speed;
    }
}
