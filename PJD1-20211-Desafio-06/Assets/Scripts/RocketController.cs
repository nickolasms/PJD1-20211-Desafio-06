using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public class RocketController : Rigidbody2DBase, IPoolableObject
{
    private float speed = 7f;
    private float distance = 3f;

    public int Damage { get; protected set; }
    private Vector2 startPosition;

    public CircleCollider2D cc2D;
    public PointEffector2D pe2D;
    public Collider2D[] enemies;

    private int aux = 0;

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

    private void Update()
    {
        if (Vector2.Distance(startPosition, tf.position) >= distance)
        {
            Debug.Log(gameObject.name);
            Factory.Recycle(FactoryItem.PlayerRocket, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pe2D.gameObject.SetActive(true);
        cc2D.radius = 3;
        StartCoroutine(endExplosion(collision));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (aux == 0)
        {
            enemies = Physics2D.OverlapCircleAll(this.transform.position, 3.5f, 1 << LayerMask.NameToLayer("Enemy"));
            GameController.PlayerRocketTrigger(this, collision);
            aux++;
        }
    }

    IEnumerator endExplosion(Collider2D collision)
    {
        yield return new WaitForSeconds(0.06f);
        Debug.Log(collision.name);
        Factory.Recycle(FactoryItem.PlayerRocket, gameObject);
    }
}
