using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReloadMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Gun;
    [SerializeField]
    private int PN;
    //Transform Gunposition;

    void Update()
    {
        if(PN != PhotonScriptor.ConnectingScript.informPlayerID())
        {
            return;
        }
        if(Shoot.InformReloadState() == true && Shoot.InformReloadingTime() > 1.5f)
        {
            Gun.transform.position += transform.forward * Time.deltaTime;
        }
        if(Shoot.InformReloadState() == true && Shoot.InformReloadingTime() < 1.5f)
        {
            Gun.transform.position -= transform.forward * Time.deltaTime;
        }
    }
}
