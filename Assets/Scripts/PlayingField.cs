using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingField : MonoBehaviour
{
    //Speed of the playing field.
    public float speed;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerWeapon") || collision.CompareTag("EnemyWeapon"))
        {
            Destroy(collision.transform.root.gameObject);
        }
        else if(collision.CompareTag("Enemy") || collision.CompareTag("Powerup"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StageSlow"))
        {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().startSlow();
        }
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
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    public IEnumerator speedUp(int targetSpeed)
    {
        while (speed < targetSpeed)
        {
            speed += .01f;
            GetComponent<Rigidbody2D>().velocity = transform.right * speed;
            yield return null;
        }
        speed = targetSpeed;
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }
}
