using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
    //The name of the weapon.
    public string weaponName;

    //The image shown on the display,
    public Sprite weaponImage;

    //What the weapon will shoot.
    public GameObject weaponAmmo;

    //What sound will the weapon make.
    public AudioClip weaponSound;

    //What the weapon's level currently is.
    public int weaponLevel = 1;
    public int levelCharge = 0;

    //Firerate of the weapon;
    public float fireRate;
    private float nextFire = 0f;

    //Where the ammo will be fired.
    protected Transform shotSpawn;

    //Represents the audio source.
    protected AudioSource source;

    private void Start()
    {
        source = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

    public void increaseLevel()
    {
        if(weaponLevel < 3)
        {
            levelCharge++;
            if(levelCharge % 3 == 0)
            {
                weaponLevel++;
                source.PlayOneShot(Resources.Load("SoundEffects/SFX_Powerup_01") as AudioClip);
            }
        }
    }

    public void decreaseLevel()
    {
        if (weaponLevel > 1)
        {
            source.PlayOneShot(Resources.Load("SoundEffects/Ouch__008") as AudioClip);
            weaponLevel--;
            if (weaponLevel == 2 || levelCharge > 3)
            {
                Debug.Log("3 To 2");
                levelCharge = 3;
            }
            else
            {
                Debug.Log("2 To 1");
                levelCharge = 0;
            }
        }
        else
        {
            levelCharge = 0;
        }
    }

    virtual public void playSound()
    {
        source.PlayOneShot(weaponSound);
    }

    public void fireWeapon()
    {
        if(shotSpawn == null)
        {
            shotSpawn = GameObject.Find("WeaponFire").GetComponent<Transform>();
        }

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            switch (weaponLevel)
            {
                case 1:
                    Fire1();
                    break;
                case 2:
                    Fire2();
                    break;
                case 3:
                    Fire3();
                    break;
            }
            playSound();
        }
    }

    abstract public void Fire1();
    abstract public void Fire2();
    abstract public void Fire3();
    
}
