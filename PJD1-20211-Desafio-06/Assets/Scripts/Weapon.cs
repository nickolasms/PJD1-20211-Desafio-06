using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public enum WeaponType { None, Pistol, Shotgun, MachineGun, Sniper, RocketLauncher }

public class Weapon : MonoBehaviour
{
    public string Name { get; protected set; }
    public int Ammo { get; protected set; }
    public int AmmoMax { get; protected set; }
    public int Damage { get; protected set; }
    public float FireRate { get; protected set; }
    public float ReloadSpeed { get; protected set; }
    public float BulletSpeed { get; protected set; }
    public float Distance { get; protected set; }
    public WeaponType Type { get; protected set; }

    public WeaponDTO weaponDTO;

    protected bool Sniper = false;
    public LineRenderer lineRendererSniper;

    protected float TimerSniper;

    protected bool isFiring;
    protected bool isReloading;
    protected bool CanFire 
    { 
        get {
            return !isFiring && !isReloading && Ammo > 0;
        } 
    }

    [SerializeField]
    protected Transform bulletRespawn;

    protected virtual void Awake()
    {
        bulletRespawn = transform.Find("BulletRespawn");

        if(weaponDTO != null)
        {
            Init(weaponDTO);
        }

        TimerSniper = FireRate;
    }

    public virtual void Init(WeaponDTO wdto)
    {
        Name = wdto.Name;
        Ammo = wdto.Ammo;
        AmmoMax = wdto.AmmoMax;
        Damage = wdto.Damage;
        FireRate = wdto.FireRate;
        ReloadSpeed = wdto.ReloadSpeed;
        BulletSpeed = wdto.BulletSpeed;
        Distance = wdto.Distance;

        weaponDTO = wdto;
    }

    public void Fire()
    {
        if(CanFire && Name == "Sniper")
        {
            Sniper = true;
        }
        else if(CanFire)
        {
            CreateProjectile();
            StartCoroutine(FireCooldown());
        }
    }

    protected virtual void CreateProjectile()
    {
        GameObject go = Factory.GetObject(FactoryItem.PlayerBullet, bulletRespawn.position, bulletRespawn.rotation);
        BulletController bullet = go.GetComponent<BulletController>();
        bullet.Init(weaponDTO);
    }

    private IEnumerator FireCooldown()
    {
        isFiring = true;
        Ammo--;
        GameEvents.WeaponFireEvent.Invoke(Ammo, AmmoMax, Type);
        yield return new WaitForSeconds(FireRate);
        isFiring = false;
    }

    private void StopFire()
    {
        isFiring = true;
        Ammo--;
        GameEvents.WeaponFireEvent.Invoke(Ammo, AmmoMax, Type);
    }

    public void Reload()
    {
        if(!isReloading && Ammo < AmmoMax)
        {
            StartCoroutine(ReloadCooldown());
        }
    }

    private IEnumerator ReloadCooldown()
    {
        int amount = GameController.CheckReloadPlayerAmmunition(Type);
        if(amount <= 0)
        {
            yield break;
        }
        Debug.Log("Begin Reload");
        isReloading = true;
        int ammoDiff = Mathf.Min(AmmoMax, amount);
        GameEvents.WeaponReloadEvent.Invoke(ReloadSpeed,ammoDiff,Type);
        yield return new WaitForSeconds(ReloadSpeed);
        Ammo = ammoDiff;
        isReloading = false;
        GameEvents.WeaponFireEvent.Invoke(Ammo, AmmoMax,Type);
        Debug.Log("End Reload");
    }

    public void Update()
    {
        if(Sniper == true  && Ammo >= 1 && Input.GetMouseButton(0))
        {
            TimerSniper -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(TimerSniper % 60F);
            GameEvents.WeaponSniper.Invoke(TimerSniper);
            if(seconds <= 0)
            {
                CreateProjectile();
                StopFire();
                Sniper = false;
                TimerSniper = FireRate;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            TimerSniper = FireRate;
        }


        if(Name == "Sniper")
        {
            lineRendererSniper.SetPosition(0, bulletRespawn.position);
            RaycastHit2D hitInfo = Physics2D.Raycast(bulletRespawn.position, bulletRespawn.up);
            if(hitInfo)
            {
                EnemyController enemy = hitInfo.transform.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    lineRendererSniper.enabled = true;
                    lineRendererSniper.SetPosition(1, hitInfo.point);
                }
                
            }
            else
            {
                lineRendererSniper.enabled = false;
            }
           
        }
        
        
    }


}
