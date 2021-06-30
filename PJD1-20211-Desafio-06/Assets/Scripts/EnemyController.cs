using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class EnemyController : Rigidbody2DBase, IPoolableObject
{
    public int MaxHp { get; protected set; }
    public int Hp { get; protected set; }
    public int Damage = 10;
    
    
    private NavMeshAgent map;
    

    public Image HUDhp;
    public Text HUDdamage;

    private void Start()
    {


        Hp = MaxHp = Random.Range(100, 201);
        HUDhp = GetComponentInChildren<Image>();
        HUDdamage = GetComponentInChildren<Text>();
        

        //currentTarget = point1;

        map = GetComponent<NavMeshAgent>();
        map.updateRotation = false;
        map.updateUpAxis = false;

        //adiciona o icon do enemy no minimapa
        IconCreator.AddIcon(transform, 1);

    }


    public bool ApplyDamage(int damage)
    {
        GameEvents.EnemyHpEvent.Invoke(Hp, MaxHp, HUDhp);
        GameEvents.EnemyDamageEvent.Invoke(damage, HUDdamage);

        Hp -= damage;
        return Hp <= 0;
    }


    public void Recycle()
    {
        gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        Start();
    }


    private void OnCollisionEnter2D(Collision2D collision)

    {

        GameController.EnnemyDamageTrigger(this, collision);

    }



}
