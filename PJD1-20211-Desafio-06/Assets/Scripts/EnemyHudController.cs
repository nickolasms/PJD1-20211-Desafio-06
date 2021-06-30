using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHudController : MonoBehaviour
{
    public Image EnemyHp;
    public Text Damage;

    public Color EnemyHpMax;
    public Color EnemyHpMin;
    

    private void Awake()
    {
        EnemyHp.fillAmount = 1f;
        EnemyHp.color = EnemyHpMax;
        Damage.enabled = false;

    }

    private void Start()
    {
        GameEvents.EnemyHpEvent.AddListener(HandleEnemyHp);
        GameEvents.EnemyDamageEvent.AddListener(HandleDamage);
    }

    protected void HandleEnemyHp(int currentHp, int maxHp, Image img)
    {
        img.fillAmount = (float)currentHp / (float)maxHp;
        img.color = Color.Lerp(EnemyHpMin, EnemyHpMax, (float)currentHp / (float)maxHp);

    }

    protected void HandleDamage(int damage, Text txt)
    {
        StartCoroutine(TakeDamage(damage, txt));
    }
   protected IEnumerator TakeDamage(int damage, Text txt)
    {
        txt.enabled = true;
        txt.text = string.Format("{0}", damage);
        yield return new WaitForSeconds(0.2f);
        txt.enabled = false;
        
    }

    private void Update()
    {
        

    }
}
