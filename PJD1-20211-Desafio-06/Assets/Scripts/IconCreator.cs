using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconCreator : MonoBehaviour
{
    public GameObject playerIcon;
    public GameObject enemyIcon;

    static public List<GameObject> icons = new List<GameObject>();

    private void Awake()
    {
        icons.Add(playerIcon);
        icons.Add(enemyIcon);
    }

    static public void AddIcon(Transform parent, int type)
    {
        Instantiate(icons[type], parent);
    }
}
