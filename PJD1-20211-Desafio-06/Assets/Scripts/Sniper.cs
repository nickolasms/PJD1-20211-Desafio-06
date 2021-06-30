using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
   

    protected override void Awake()
    {
        Type = WeaponType.Sniper;
        base.Awake();
    }

}
