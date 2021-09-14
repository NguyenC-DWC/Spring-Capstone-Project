using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRotateRigid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().SetRotation(90f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
