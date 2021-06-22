using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MachineGunDTO", menuName = "ScriptableObjects/MachineGunDTO")]
public class MachineGunDTO : WeaponDTO
{
    public float Accuracy;
    public float DeltaDistance;
}
