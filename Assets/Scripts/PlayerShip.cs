using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    //Represents the playing field.
    private GameObject playingField;

    //Rigidbody of the ship.
    [SerializeField]
    private Rigidbody shipRB;
    
    //Represents the weapon, and where the weapon should be shot from on the ship.
    public Transform shotSpawn;
    public GameObject weapon;

    //Firerate of the ship;
    float fireRate = .25f;
    float nextFire = 0f;

    //Speed of the ship.
    private float speed = 3f;

    void Start()
    {
        playingField = GameObject.Find("PlayingField");
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
        Vector3 movement = new Vector3(horizontalMove,verticalMove,0);
        shipRB.velocity = playingField.GetComponent<Rigidbody>().velocity + (movement * speed);

        //Sets the boundaries of the field.
        Transform fieldLoc = playingField.GetComponent<Transform>();
        shipRB.position = new Vector3
        (
            Mathf.Clamp(shipRB.position.x,fieldLoc.position.x - 8.6f,fieldLoc.position.x + 8.6f),
            Mathf.Clamp(shipRB.position.y,-4.9f,4.9f),
            0
        );
    }

    void shoot()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(weapon,shotSpawn.position,shotSpawn.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "EnemyWeapon")
        {
            Destroy(other.gameObject);
            Debug.Log("Ouch!");
        }
    }
}
