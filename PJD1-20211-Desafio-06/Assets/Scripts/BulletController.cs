using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public class BulletController : Rigidbody2DBase, IPoolableObject
{
    private float speed = 8f;
    private float distance = 3f;
    public int Damage { get; protected set; }
    private Vector2 startPosition;

    public void Recycle()
    {
        gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        Start();
    }

    private void Start()
    {
        startPosition = tf.position;
        rb.velocity = tf.up * speed;
    }

    public void Init()
    {
        speed = 0f;
        Start();
    }

    public void Init(WeaponDTO wdto)
    {
        Damage = wdto.Damage;
        distance = wdto.Distance;
        speed = wdto.BulletSpeed;
        Start();
    }

    public void Init(MachineGunDTO mdto)
    {
        Damage = mdto.Damage;
        speed = mdto.BulletSpeed;
        distance = Random.Range(mdto.Distance - mdto.DeltaDistance, mdto.Distance + mdto.DeltaDistance);
        Start();
    }

    private void Update()
    {
        if(Vector2.Distance(startPosition,tf.position) >= distance)
        {
            Debug.Log(gameObject.name);
            Factory.Recycle(FactoryItem.PlayerBullet, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        GameController.PlayerBulletTrigger(this, collision);
        Factory.Recycle(FactoryItem.PlayerBullet, gameObject);
    }

}
