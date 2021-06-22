using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public Text Ammo;
    public Image Reload;

    public Color AmmoMax;
    public Color AmmoMin;

    private void Awake()
    {
        Reload.fillAmount = 0;
    }

    private void Start()
    {
        GameEvents.WeaponReloadEvent.AddListener(HandleReload);
        GameEvents.WeaponFireEvent.AddListener(HandleAmmo);
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

    private void Update()
    {
        
    }

}
