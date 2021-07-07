﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;
using UnityEngine.UI;

public class PlayerController : Rigidbody2DBase
{

    public Text debug;

    public int Hp { get; set; }
    public int MaxHp { get; protected set; }

    public GameObject bulletPrefab;


    private float speed = 2f;

    public float Horizontal { get; protected set; }
    public float Vertical { get; protected set; }
    public Vector3 MousePosition { get; protected set; }
    public bool Fire { get; protected set; }
    public bool Reload { get; protected set; }

    [SerializeField]
    private Dictionary<WeaponType, int> ammunition = new Dictionary<WeaponType, int>();

    public int GetAmmunition(WeaponType type)
    {
        return ammunition[type];
    }

    [SerializeField]
    private Vector3 diffAngle;

    [SerializeField]
    private int weaponIndex = 0;
    public int WeaponIndex 
    { 
        get { return weaponIndex; }
        protected set {
            if(weaponIndex != value)
            {
                CurrentWeapon.gameObject.SetActive(false);
                weaponIndex = value;
                CurrentWeapon.gameObject.SetActive(true);
                GameEvents.WeaponFireEvent.Invoke(CurrentWeapon.Ammo, CurrentWeapon.weaponDTO.AmmoMax, CurrentWeapon.Type);
            }
        }
    }
    public Weapon CurrentWeapon { get { return weapons[weaponIndex]; } }

    [SerializeField]
    private List<Weapon> weapons = new List<Weapon>();

    protected override void Awake()
    {
        base.Awake();

        weapons.AddRange(GetComponentsInChildren<Weapon>(true));
        WeaponIndex = 0;
        CurrentWeapon.gameObject.SetActive(true);
        GameEvents.WeaponFireEvent.Invoke(CurrentWeapon.Ammo, CurrentWeapon.weaponDTO.AmmoMax, CurrentWeapon.Type);
    }


    public bool ApplyDamage(int damage)
    {
        Hp -= damage;

        GameEvents.PlayerHpEvent.Invoke(Hp, MaxHp);
        return Hp <= 0;

    }


    protected virtual void Start()
    {

        Hp = MaxHp = 100;
        GameEvents.WeaponFireEvent.Invoke(CurrentWeapon.Ammo, CurrentWeapon.weaponDTO.AmmoMax, CurrentWeapon.Type);

    

        //adiciona o icon do player no minimapa
        // IconCreator.AddIcon(transform, 0);

    }



    public virtual void Init()
    {
        ammunition.Add(WeaponType.None, 0);
        ammunition.Add(WeaponType.Pistol, 26);
        ammunition.Add(WeaponType.Shotgun, 16);
        ammunition.Add(WeaponType.MachineGun, 200);
        ammunition.Add(WeaponType.RocketLauncher, 12);
        ammunition.Add(WeaponType.Sniper, 10);

        //GameEvents.WeaponFireEvent.AddListener();
        GameEvents.WeaponReloadEvent.AddListener(HandleReload);
    }

    protected void HandleReload(float reloadSpeed, int ammo, WeaponType weapon)
    {
        Debug.LogFormat("B>> {0} {1} {2}", reloadSpeed, ammo, weapon);
        int weaponAmmo = ammunition[weapon];
        weaponAmmo = weaponAmmo - ammo;
        ammunition[weapon] = weaponAmmo;
    }

    public void SetInput(float horizontal, float vertical, Vector3 mousePosition, int selectWeapon, bool fire, bool reload)
    {
        Horizontal = horizontal;
        Vertical = vertical;
        MousePosition = mousePosition;
        diffAngle = MousePosition - tf.position;
        WeaponIndex = (selectWeapon + weapons.Count) % weapons.Count;
        Fire = fire;
        Reload = reload;
    }

    public void SetInput(float horizontal, float vertical, Vector2 angle, int selectWeapon, bool fire, bool reload)
    {
        Horizontal = horizontal;
        Vertical = vertical;
        diffAngle = new Vector3(angle.x,angle.y);
        WeaponIndex = (selectWeapon + weapons.Count) % weapons.Count;
        Fire = fire;
        Reload = reload;
    }

    private void Update()
    {
        if(Fire)
        {
            CurrentWeapon.Fire();

            //Vector3 rotation = bulletRespawn.rotation.eulerAngles;
            //Factory.GetObject("bullet", bulletRespawn.position, bulletRespawn.rotation);

            //Factory.GetObject("bullet", bulletRespawn.position, Quaternion.Euler(rotation.x,rotation.y,rotation.z - 10));

            //Factory.GetObject("bullet", bulletRespawn.position, Quaternion.Euler(rotation.x, rotation.y, rotation.z + 10));
        }

        if(Reload)
        {
            CurrentWeapon.Reload();
        }

        // Rotation
        tf.rotation = Quaternion.Euler(0,0,Mathf.Atan2(diffAngle.y, diffAngle.x) * Mathf.Rad2Deg - 90f);

        string str = "";
        foreach (KeyValuePair<WeaponType,int> weapon in ammunition)
        {
            str += weapon.Key + ": " + weapon.Value + "\n";
        }

        debug.text = str;

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Horizontal, Vertical) * speed;
    }

    public void ChangeWeapon(ItenWeapon iten, int index, Color color, WeaponDTO weaponDTO)
    {
        WeaponDTO CurrentDTO = weapons[index].weaponDTO;
        Color CurrentColor = weapons[index].gameObject.GetComponentInChildren<SpriteRenderer>().color;
        weapons[index].weaponDTO = weaponDTO;
        weapons[index].gameObject.GetComponentInChildren<SpriteRenderer>().color = color;

        iten.weaponDTO = CurrentDTO;
        iten.color = CurrentColor;
    }

    public void MaisAmmo(WeaponType type, int amount)
    {
        int weaponAmmo = ammunition[type];
        weaponAmmo = weaponAmmo + amount;
        ammunition[type] = weaponAmmo;
    }

    // public void OnTriggerStay2D(Collider2D other) 
    // {
    //     if(other.tag == "upgrade" && other.name == "Sniper2 collect")
    //     {
    //         if(Input.GetKeyDown(KeyCode.F))
    //         {
                
    //             Debug.Log("sniper coletada");
    //         }
    //     }
    // }
}
