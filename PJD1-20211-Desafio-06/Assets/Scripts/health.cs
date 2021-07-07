using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{

    public int Cura;
    public PlayerController player;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {

            player.ApplyDamage(-Cura);
            Destroy(gameObject);
        }
    }
}
