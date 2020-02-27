using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    //Represents the playing field.
    private GameObject playingField;

    //Represents the audio source.
    public AudioSource source;

    //Rigidbody of the ship.
    [SerializeField]
    private Rigidbody2D shipRB;

    //Represents the weapon, and where the weapon should be shot from on the ship.
    
    public Transform shotSpawn;
    public GameObject weapon;
    public AudioClip weaponSound;

    //Firerate of the ship;
    float fireRate = .25f;
    float nextFire = 0f;

    //Speed of the ship.
    private float speed = 3f;

    void Start()
    {
        playingField = GameObject.Find("PlayingField");
        source = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Ship Movement
        moveShip();
        shoot();
    }

    void moveShip()
    {
        //Gets the positive/negative movement values from the keyboard.
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        //Sets the movement depending on the control direction. Have the player move with the field plus the direction.
        Vector2 movement = new Vector2(horizontalMove,verticalMove);
        shipRB.velocity = playingField.GetComponent<Rigidbody2D>().velocity + (movement * speed);

        //Sets the boundaries of the field.
        Transform fieldLoc = playingField.GetComponent<Transform>();
        shipRB.position = new Vector2
        (
            Mathf.Clamp(shipRB.position.x,fieldLoc.position.x - 8.6f,fieldLoc.position.x + 8.6f),
            Mathf.Clamp(shipRB.position.y,-4.9f,4.9f)
        );
    }

    void shoot()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(weapon,shotSpawn.position,shotSpawn.rotation);
            source.PlayOneShot(weaponSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyWeapon"))
        {
            source.PlayOneShot(Resources.Load("SoundEffects/retro_die_02") as AudioClip);
            Debug.Log("Ouch!");
        }
    }
}
