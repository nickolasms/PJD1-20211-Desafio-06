using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventFloat : UnityEvent<float> { }
public class EventIntInt : UnityEvent<int, int> { }
public class EventFireWeapon : UnityEvent<int, int, WeaponType> { }

public class EventReloadWeapon : UnityEvent<float, int, WeaponType> { }
public class EventCurrentAmmo : UnityEvent<float, int, WeaponType> { }
public class EventPlayerHp : UnityEvent<int, int> { }
public class EventEnemyHp : UnityEvent<int, int, Image> { }
public class EventEnemyDamage : UnityEvent<int, Text> { }

public class GameEvents : MonoBehaviour
{
    static public EventReloadWeapon WeaponReloadEvent = new EventReloadWeapon();

    static public EventCurrentAmmo PlayerCurrentAmmo = new EventCurrentAmmo();

    static public EventPlayerHp PlayerHpEvent = new EventPlayerHp();

    static public EventEnemyHp EnemyHpEvent = new EventEnemyHp();

    static public EventEnemyDamage EnemyDamageEvent = new EventEnemyDamage();

    static public EventFireWeapon WeaponFireEvent = new EventFireWeapon();

    private void Awake()
    {
        
    }


}
