using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItenAmmo : MonoBehaviour
{

   public WeaponType type;

   public int Amount;
    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            GameController.CollectAmmo(type, Amount);
            Destroy(gameObject);
        }
    }
}
