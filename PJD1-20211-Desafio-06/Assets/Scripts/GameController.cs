using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public class GameController : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject Enemy;

    static private PlayerController player;

    private void Awake()
    {
        Factory.Clear();
        Factory.Register(FactoryItem.PlayerBullet, BulletPrefab, 100);
        Factory.Register(FactoryItem.Enemy, Enemy, 200);

        player = GameObject.FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        player.Init();
        RespawnEnemy();
    }

    static public void PlayerBulletTrigger(BulletController bullet, Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if(enemy)
        {
            if(enemy.ApplyDamage(bullet.Damage))
            {
                Factory.Recycle(FactoryItem.Enemy, enemy.gameObject);
                RespawnEnemy();
            }
        }
    }

    static public void EnnemyDamageTrigger(EnemyController damage, Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player)
        {
            if (player.ApplyDamage(damage.Damage))
            {
                Destroy(player.gameObject);
            }
             
        }
    }

    static public int CheckReloadPlayerAmmunition(WeaponType type)
    {
        return player.GetAmmunition(type);
    }

    static public void RespawnEnemy()
    {
        Vector2 playerPosition = player.transform.position;
        float angle = Random.value * (2 * Mathf.PI);
        Vector2 onUnitCircle = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Vector2 position = playerPosition + onUnitCircle * Random.Range(3f,4f);
        GameObject go = Factory.GetObject(FactoryItem.Enemy, position, Quaternion.identity);
    }
}
