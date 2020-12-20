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
        if(Shoot.InformMagazineLeft() <= 5)
        {
            ReloadText.SetActive(true);
        }
        else
        {
            ReloadText.SetActive(false);
        }
    }
}
