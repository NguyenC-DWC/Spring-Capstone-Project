using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
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
        shotSpawn = GameObject.Find("WeaponFire").GetComponent<Transform>();
        source = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

    public void increaseLevel()
    {
        if(weaponLevel < 3)
        {
            if(levelCharge < 3)
            {
                levelCharge++;
            }

            if(levelCharge == 3)
            {
                levelCharge = 0;
                weaponLevel++;
            }
        }
    }

    public void decreaseLevel()
    {
        if (weaponLevel > 1)
        {
            levelCharge = 0;
            weaponLevel--;
        }
    }

    virtual public void playSound()
    {
        source.PlayOneShot(weaponSound);
    }

    public void fireWeapon()
    {
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
