using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    private AudioSource source;

    //The effect and sound the enemy leaves when destroyed.
    public GameObject deathEffect;
    public AudioClip deathSound;

    private void Start()
    {
        source = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        GetComponent<EnemyMove>().move();
        GetComponent<EnemyAttack>().attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Determines if the ship has hit the player weapon or the player.
        if (collision.CompareTag("PlayerWeapon") || collision.CompareTag("Player"))
        {
            if(collision.CompareTag("PlayerWeapon"))
            {
                Destroy(collision.gameObject);
            }

            Instantiate(deathEffect, transform.position, transform.rotation);
            source.PlayOneShot(deathSound);
            Destroy(gameObject);
        }
    }
}
