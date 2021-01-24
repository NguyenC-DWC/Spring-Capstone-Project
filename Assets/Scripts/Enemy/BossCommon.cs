using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommon : MonoBehaviour
{
    public GameObject bossHealth;

    private AudioSource source;

    //Used to flash the enemy white when hit.
    private Material flash;
    private Material normal;

    private float maxHealth;
    public int health = 50;
    public int scoreValue = 500;

    public bool active = false;
    public bool movingUp = true;
    public bool canShoot = true;

    public GameObject[] bossWeapons;

    private void Start()
    {
        maxHealth = health;
        source = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        flash = Resources.Load("Materials/WhiteFlash") as Material;
        normal = GetComponent<SpriteRenderer>().material;
    }

    void FixedUpdate()
    {
        if (active)
        {
            move();
            StartCoroutine(attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Determines if the ship has hit the player weapon or the player.
        if (collision.CompareTag("PlayerWeapon") && active)
        {
            GetComponent<SpriteRenderer>().material = flash;
            Destroy(collision.gameObject);

            if (health == 1)
            {
                active = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GameObject.Find("GameManager").GetComponent<GameManager>().addScore(scoreValue);
                StartCoroutine(bossDeath());
            }
            else
            {
                Invoke("materialNormal", .05f);
            }
            health--;
            GameObject.Find("BossHealth").GetComponent<BossHealth>().barDecrease(health / maxHealth);
        }
    }

    public void move()
    {
        if(transform.position.y > 4 || transform.position.y < -4)
        {
            if(transform.position.y > 4)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0,-3);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0,3);
            }
        }
    }

    public IEnumerator attack()
    {
        if(canShoot)
        {
            canShoot = false;
            for(int i = 0; i < bossWeapons.Length; i++)
            {
                bossWeapons[i].GetComponentInChildren<EnemyWeapon>().fireWeapon(bossWeapons[i].transform);
            }
            yield return new WaitForSeconds(3f);
            canShoot = true;
        }
    }

    public IEnumerator bossEntrance()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 0);
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(.5f);
        bossHealth.SetActive(true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
        active = true;
    }

    public IEnumerator bossDeath()
    {
        for(int i = 0; i < 20; i++)
        {
            Vector3 explosionLoc = new Vector3(transform.position.x + Random.Range(-1f,1f), transform.position.y + Random.Range(-1f, 1f),0);
            Instantiate(Resources.Load("Animations/Explosion") as GameObject, explosionLoc, transform.rotation);
            source.PlayOneShot(Resources.Load("SoundEffects/explosion04") as AudioClip);
            yield return new WaitForSeconds(.2f);
        }
        Instantiate(Resources.Load("Animations/BigExplosion") as GameObject, transform.position, transform.rotation);
        source.PlayOneShot(Resources.Load("SoundEffects/explosion1") as AudioClip);
        GameObject.Find("LevelManager").GetComponent<LevelManager>().endLevel();
        bossHealth.SetActive(false);
        Destroy(gameObject);
    }

    private void materialNormal()
    {
        GetComponent<SpriteRenderer>().material = normal;
    }
}
