using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public class RocketLauncher : Weapon
{
    protected override void Awake()
    {
        Type = WeaponType.RocketLauncher;
        base.Awake();
    }

    protected override void CreateProjectile()
    {
        GameObject go = Factory.GetObject(FactoryItem.PlayerRocket, bulletRespawn.position, bulletRespawn.rotation);
        RocketController bullet = go.GetComponent<RocketController>();
        bullet.Init(weaponDTO);
    }
}
