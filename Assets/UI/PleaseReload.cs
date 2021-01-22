using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PleaseReload : MonoBehaviour
{
    [SerializeField]
    GameObject ReloadText;
    [SerializeField]
    private Text Reload;
    void Start()
    {
        ReloadText.SetActive(false);
    }

    void Update()
    {
        if(Shoot.InformMagazineLeft() <= 5 && Player.WeaponSwap.InformWeapon() == false)
        {
            ReloadText.SetActive(true);
            Reload.text = "PRESS \"R\" TO RELOAD";
            Reload.color = Color.red;
            if(Shoot.InformReloadState() == true)
            {
                Reload.text = "RELOADING";
                Reload.color = Color.yellow;
            }
        }
        if (Shoot.InformMagazineLefts() <= 3 && Player.WeaponSwap.InformWeapon() == true)
        {
            ReloadText.SetActive(true);
            Reload.text = "PRESS \"R\" TO RELOAD";
            Reload.color = Color.red;
            if (Shoot.InformReloadState() == true)
            {
                Reload.text = "RELOADING";
                Reload.color = Color.yellow;
            }
        }
        else
        {
            ReloadText.SetActive(false);
        }
    }
}
