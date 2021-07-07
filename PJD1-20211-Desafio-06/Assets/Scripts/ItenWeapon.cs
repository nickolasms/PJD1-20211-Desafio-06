using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItenWeapon : MonoBehaviour
{
    public WeaponDTO weaponDTO;

    public Color color;

    public int index;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            GameController.CollectWeapon(this, index, color, weaponDTO);
        }
    }
}
