using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingField : MonoBehaviour
{
    //Speed of the playing field.
    public float speed;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerWeapon") || collision.CompareTag("EnemyWeapon") || collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    public void startSlow()
    {
        StartCoroutine(slowToStop());
    }

    public IEnumerator slowToStop()
    {
        while (speed > 0)
        {
            speed -= .01f;
            GetComponent<Rigidbody2D>().velocity = transform.right * speed;
            yield return null;
        }
        speed = 0;
    }
}
