using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.childCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
