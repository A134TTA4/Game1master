using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject FailedPanel;
    
    void Update()
    {
        if(PhotonScriptor.ConnectingScript.informFailed() == true)
        {
            FailedPanel.SetActive(true);
        }
        else
        {
            FailedPanel.SetActive(false);
        }
    }

    public void ClickOKtoFailed()
    {
        PhotonScriptor.ConnectingScript.FailedfalseM();
    }
}
