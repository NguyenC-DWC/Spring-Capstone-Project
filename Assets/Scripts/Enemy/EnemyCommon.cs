using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    void FixedUpdate()
    {
        GetComponent<EnemyMove>().move();
        GetComponent<EnemyShoot>().attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Determines if the ship has hit the player weapon.
        if (collision.CompareTag("PlayerWeapon"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
