using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleaseReload : MonoBehaviour
{
    [SerializeField]
    GameObject ReloadText;
    void Start()
    {
        ReloadText.SetActive(false);
    }

    void Update()
    {
        if(Shoot.InformMagazineLeft() <= 5 && Player.WeaponSwap.InformWeapon() == false)
        {
            ReloadText.SetActive(true);
        }
        if (Shoot.InformMagazineLefts() <= 3 && Player.WeaponSwap.InformWeapon() == true)
        {
            ReloadText.SetActive(true);
        }
        else
        {
            ReloadText.SetActive(false);
        }
    }
}
