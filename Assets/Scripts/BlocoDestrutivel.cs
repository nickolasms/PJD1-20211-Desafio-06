using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoDestrutivel : MonoBehaviour
{
    public int blockHealth = 100;
    void Update()
    {
        
    }
    //colocar tags dos tiros como "Bullet" ou mudar a tag da colisão
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        blockHealth -= 50;

        if(blockHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
}
