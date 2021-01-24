using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggThrower : PlayerWeapon
{
    private void FixedUpdate()
    {

    }

    override public void Fire1()
    {
        GameObject ammo = Instantiate(weaponAmmo, shotSpawn.position, shotSpawn.rotation);
    }

    override public void Fire2()
    {
        GameObject ammo = Instantiate(weaponAmmo, shotSpawn.position, shotSpawn.rotation);
    }

    override public void Fire3()
    {
        GameObject ammo = Instantiate(weaponAmmo, shotSpawn.position, shotSpawn.rotation);
    }
}
