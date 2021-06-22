using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunDTO", menuName = "ScriptableObjects/ShotgunDTO")]
public class ShotgunDTO : WeaponDTO
{
    public int Pellet;
    public float Spread;
}
