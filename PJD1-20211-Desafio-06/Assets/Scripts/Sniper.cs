using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{

    public LayerMask layer;
   public LineRenderer lineRendererSniper;

    protected float TimerSniper;

    protected override void Awake()
    {
        Type = WeaponType.Sniper;
        base.Awake();

        TimerSniper = FireRate;
    }

    private void StopFire()
    {
        isFiring = true;
        Ammo--;
        GameEvents.WeaponFireEvent.Invoke(Ammo, AmmoMax, Type);
    }

    public override void Fire()
    {
    }

    public override void Update()
    {
        base.Update();
        
        if(Ammo >= 1 && Input.GetMouseButton(0))
        {
            TimerSniper -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(TimerSniper % 60F);
            GameEvents.WeaponSniper.Invoke(TimerSniper);
            if(seconds <= 0)
            {
                CreateProjectile();
                StopFire();
                TimerSniper = FireRate;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            TimerSniper = FireRate;
        }


        
        
        lineRendererSniper.SetPosition(0, bulletRespawn.position);
        RaycastHit2D hitInfo = Physics2D.Raycast(bulletRespawn.position, bulletRespawn.up, float.PositiveInfinity, layer);
        if(hitInfo)
        {
            EnemyController enemy = hitInfo.transform.GetComponent<EnemyController>();
            Debug.Log(enemy);
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
