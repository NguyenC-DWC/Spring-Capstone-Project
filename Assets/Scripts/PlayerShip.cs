using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private GameManager manager;

    //Represents the playing field.
    private GameObject playingField;

    //Represents the audio source.
    private AudioSource source;

    //Rigidbody of the ship.
    [SerializeField]
    private Rigidbody2D shipRB;

    //The current health of the ship.
    public int shipHealth = 7;

    //Represents the weapons of the ship, and the current active weapon.
    public PlayerWeapon activeWeapon;

    private PlayerWeapon mainWeapon;
    private PlayerWeapon secondaryWeapon;

    public bool isActive;

    //Speed of the ship.
    public int speed = 3;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playingField = GameObject.Find("PlayingField");
        source = GameObject.Find("SoundManager").GetComponent<AudioSource>();

        shipHealth = manager.currentHealth;
        speed = manager.currentSpeed;

        mainWeapon = GameObject.Find("CurrentMain").transform.GetChild(0).GetComponent<PlayerWeapon>();
        secondaryWeapon = GameObject.Find("CurrentSecondary").transform.GetChild(0).GetComponent<PlayerWeapon>();

        activeWeapon = mainWeapon;

        updateDisplays();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Ship Movement
        if (isActive)
        {
            moveShip();
            shoot();
        }
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
        if (Input.GetKey(KeyCode.Space))
        {
            activeWeapon.fireWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(swapWeapons());
        }
    }

    public IEnumerator swapWeapons()
    {
        StartCoroutine(GameObject.Find("DisplayMain").GetComponent<WeaponDisplay>().toggleDisplay());
        StartCoroutine(GameObject.Find("DisplaySecondary").GetComponent<WeaponDisplay>().toggleDisplay());

        source.PlayOneShot(Resources.Load("SoundEffects/synth_beep_02") as AudioClip);

        if (activeWeapon == mainWeapon)
        {
            activeWeapon = secondaryWeapon;
        }
        else
        {
            activeWeapon = mainWeapon;
        }

        yield return new WaitForSeconds(.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyWeapon"))
        {
            
            shipHealth--;
            manager.currentHealth = shipHealth;

            if (speed > 3)
            {
                manager.currentSpeed--;
                speed--;
            }

            activeWeapon.decreaseLevel();
            updateDisplays();

            if (shipHealth == 0)
            {
                Instantiate(Resources.Load("Animations/Explosion"),transform.position,transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                source.PlayOneShot(Resources.Load("SoundEffects/retro_die_02") as AudioClip,.5f);
                Debug.Log("Ouch!");
            }
        }
    }

    public void updateDisplays()
    {
        GameObject.Find("PlayerHealth").GetComponent<PlayerHealth>().updateHealth(shipHealth);
        GameObject.Find("PlayerSpeed").GetComponent<PlayerSpeed>().updateSpeed(speed);
        if (activeWeapon == mainWeapon)
        {
            GameObject.Find("DisplayMain").GetComponent<WeaponDisplay>().updateLevel(ref activeWeapon);
        }
        else
        {
            GameObject.Find("DisplaySecondary").GetComponent<WeaponDisplay>().updateLevel(ref activeWeapon);
        }
    }

    public void setActive(bool active)
    {
        isActive = active;

        if (!active && activeWeapon == secondaryWeapon)
        {
            StartCoroutine(swapWeapons());
        }
    }
}
