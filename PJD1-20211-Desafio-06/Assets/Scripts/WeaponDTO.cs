using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDTO", menuName = "ScriptableObjects/WeaponDTO")]
public class WeaponDTO : ScriptableObject
{
    public string Name;
    public int Ammo;
    public int AmmoMax;
    public int Damage;
    public float FireRate;
    public float ReloadSpeed;
    public float BulletSpeed;
    public float Distance;
}
