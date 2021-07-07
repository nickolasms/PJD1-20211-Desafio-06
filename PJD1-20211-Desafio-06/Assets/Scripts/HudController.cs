using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public Text Ammo;
    public Image Reload;
    public Image PlayerHp;
  
    public Color AmmoMax;
    public Color AmmoMin;
    public Color PlayerHpMax;
    public Color PlayerHpMin;

    public Image SniperTimer;
   

    private void Awake()
    {
        Reload.fillAmount = 0;
        PlayerHp.fillAmount = 1f;
        PlayerHp.color = PlayerHpMax;

        SniperTimer.fillAmount = 0;
     
    }

    private void Start()
    {
        GameEvents.WeaponReloadEvent.AddListener(HandleReload);
        GameEvents.WeaponFireEvent.AddListener(HandleAmmo);
        GameEvents.PlayerHpEvent.AddListener(HandlePlayerHp);

        GameEvents.WeaponSniper.AddListener(HandleSniperTimer);

    }

    protected void HandlePlayerHp(int currentHp, int maxHp)
    {
        PlayerHp.fillAmount = (float)currentHp / (float)maxHp;

        PlayerHp.color = Color.Lerp(PlayerHpMin, PlayerHpMax, (float)currentHp / (float)maxHp);
        
    }

    protected void HandleAmmo(int currentAmmo, int maxAmmo, WeaponType weapon)
    {
        Ammo.text = string.Format("{0}/{1}", currentAmmo, maxAmmo);
        Ammo.color = Color.Lerp(AmmoMin, AmmoMax, (float)currentAmmo / (float)maxAmmo);
    }

    protected void HandleReload(float reloadSpeed, int ammo, WeaponType weapon)
    {
        StartCoroutine(StartReload(reloadSpeed));
    }

    protected void HandleSniperTimer(float time)
    {
        StartCoroutine(StartSniperTimer(time));
    }

    protected IEnumerator StartReload(float reloadSpeed)
    {
        float current = reloadSpeed;
        Reload.fillAmount = 1f;
        while(current >= 0)
        {
            yield return new WaitForEndOfFrame();
            current -= Time.deltaTime;
            Reload.fillAmount = current / reloadSpeed;
        }
    }

    protected IEnumerator StartSniperTimer(float time)
    {
        float t = 1f / time;
        yield return new WaitForEndOfFrame();
        SniperTimer.fillAmount = t;
        if(t < 1f)
        {
            SniperTimer.gameObject.SetActive(true);
        }
        else
        {
            SniperTimer.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        
    }

}
