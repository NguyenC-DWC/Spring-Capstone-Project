using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : PlayerWeapon
{
    int angle = 0;
    bool wavingUp = true;

    private void FixedUpdate()
    {
        if (weaponLevel >= 2)
        {
            weaponAmmo.GetComponent<PlayerAmmo>().lifeTime = .30f;
        }
        else
        {
            weaponAmmo.GetComponent<PlayerAmmo>().lifeTime = .15f;
        }
    }

    override public void Fire1()
    {
        Instantiate(weaponAmmo, shotSpawn.position, shotSpawn.rotation);
    }

    override public void Fire2()
    {
        Fire1();
    }

    override public void Fire3()
    {
        Debug.Log("Fire Wave");
        if (wavingUp)
        {
            angle += 5;
        }
        else
        {
            angle -= 5;
        }
        Instantiate(weaponAmmo, shotSpawn.position, Quaternion.Euler(0,0,angle));
        if(Mathf.Abs(angle) == 20)
        {
            wavingUp = !wavingUp;
        }
    }

    override public void playSound()
    {
        source.PlayOneShot(weaponSound,.2f);
    }
}
