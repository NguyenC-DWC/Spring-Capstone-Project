using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    private AudioSource source;

    //The effect and sound the enemy leaves when destroyed.
    public GameObject deathEffect;
    public AudioClip deathSound;

    //Used to flash the enemy white when hit.
    private Material flash;
    private Material normal;

    public int health = 3;
    public int scoreValue = 10;

    public GameObject formation;
    public bool inFormation;

    private void Start()
    {
        source = GameObject.Find("SoundManager").GetComponent<AudioSource>();

        flash = Resources.Load("Materials/WhiteFlash") as Material;
        normal = GetComponent<SpriteRenderer>().material;

        if (transform.parent.gameObject.CompareTag("Formation"))
        {
            formation = transform.parent.gameObject;
            inFormation = true;
        }
        else
        {
            formation = null;
            inFormation = false;
        }

        health = 1 + GameObject.Find("GameManager").GetComponent<GameManager>().currentSet;
    }

    void FixedUpdate()
    {
        GetComponent<EnemyMove>().move();
        GetComponent<EnemyAttack>().attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Determines if the ship has hit the player weapon or the player.
        if (collision.CompareTag("PlayerWeapon"))
        {
            GetComponent<SpriteRenderer>().material = flash;
            Destroy(collision.gameObject);

            if (health == 1)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().addScore(scoreValue);

                if (inFormation)
                {
                    formation.GetComponent<EnemyFormation>().decreaseShips();
                }

                Instantiate(deathEffect, transform.position, transform.rotation);
                source.PlayOneShot(deathSound);
                Destroy(gameObject);
            }
            else
            {
                health--;
                Invoke("materialNormal",.05f);
            }
        }
        else if (collision.CompareTag("Player"))
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            source.PlayOneShot(deathSound);
            Destroy(gameObject);
        }
    }

    private void materialNormal()
    {
        GetComponent<SpriteRenderer>().material = normal;
    }
}
