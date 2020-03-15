using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBullet : PlayerWeapon
{
    private void FixedUpdate()
    {
        if(weaponLevel >= 2)
        {
            fireRate = .25f;
        }
        else
        {
            fireRate = .5f;
        }
    }

    override public void Fire1()
    {
        Instantiate(weaponAmmo, shotSpawn.position, shotSpawn.rotation);
    }

    override public void Fire2()
    {
        Instantiate(weaponAmmo, shotSpawn.position, shotSpawn.rotation);
    }

    override public void Fire3()
    {
        Instantiate(weaponAmmo, shotSpawn.position + new Vector3(0,.1f), shotSpawn.rotation);
        Instantiate(weaponAmmo, shotSpawn.position + new Vector3(0,-.1f), shotSpawn.rotation);
    }
}
