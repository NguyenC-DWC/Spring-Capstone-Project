using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //Represents the audio source.
    private AudioSource source;

    public enum collectableType {weaponPower,weaponUp,speed};
    public collectableType collectType;

    //Represents the velocity of the playing field.
    private Rigidbody2D fieldVel;

    //Rigidbody of the powerup.
    private Rigidbody2D powerupRB;

    private float speed = 5f;
    private float upDownSpeed;


    void Start()
    {
        powerupRB = GetComponent<Rigidbody2D>();
        fieldVel = GameObject.Find("PlayingField").GetComponent<Rigidbody2D>();
        source = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        upDownSpeed = Random.Range(-1f, 1f);
    }

    void FixedUpdate()
    {
        if (speed > -5)
        {
            Vector2 powerupVel = transform.right * speed;
            Vector2 verticalVel = transform.up * upDownSpeed;
            powerupRB.velocity = fieldVel.velocity + powerupVel + verticalVel;
            speed -= .1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            switch (collectType)
            {
                case collectableType.weaponPower:
                    collision.GetComponent<PlayerShip>().activeWeapon.increaseLevel();
                    source.PlayOneShot(Resources.Load("SoundEffects/retro_beep_01") as AudioClip);
                    break;
                case collectableType.speed:
                    if(speed < 6)
                    {
                        source.PlayOneShot(Resources.Load("SoundEffects/SFX_Powerup_03") as AudioClip);
                        GameObject.Find("GameManager").GetComponent<GameManager>().currentSpeed++;
                        collision.GetComponent<PlayerShip>().speed++;
                    }
                    break;
                case collectableType.weaponUp:
                    collision.GetComponent<PlayerShip>().activeWeapon.increaseLevel();
                    collision.GetComponent<PlayerShip>().activeWeapon.increaseLevel();
                    collision.GetComponent<PlayerShip>().activeWeapon.increaseLevel();
                    break;

            }
            collision.GetComponent<PlayerShip>().updateDisplays();
        }
    }
}
