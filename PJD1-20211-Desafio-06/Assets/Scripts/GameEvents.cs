﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventFloat : UnityEvent<float> { }
public class EventIntInt : UnityEvent<int, int> { }
public class EventFireWeapon : UnityEvent<int, int, WeaponType> { }

public class EventReloadWeapon : UnityEvent<float, int, WeaponType> { }

public class EventSniperTimer : UnityEvent<float> { }

public class GameEvents : MonoBehaviour
{
    static public EventReloadWeapon WeaponReloadEvent = new EventReloadWeapon();

    static public EventFireWeapon WeaponFireEvent = new EventFireWeapon();

    static public EventSniperTimer WeaponSniper = new EventSniperTimer();

    private void Awake()
    {
        
    }


}
