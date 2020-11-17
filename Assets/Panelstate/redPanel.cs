using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class redPanel : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject Player2;
    [SerializeField]
    GameObject ShieldPivot;
    [SerializeField]
    GameObject WarningPanel;
    [SerializeField]
    float shielSize = 29;

    float playerx = 0;
    float playerz = 0;
    float size = 1;
    static private bool OutofArea = false;
    static private bool OutofArea2 = false;
    void Start()
    {
        WarningPanel.SetActive(false);
    }

    void Update()
    {
        if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
        {
            if (TimeManager.MainPhaze.InformMainphaze() == false)
            {
                WarningPanel.SetActive(false);
                return;
            }
            playerx = Player.transform.position.x;
            playerz = Player.transform.position.z;
            size = ShieldPivot.transform.localScale.x;

            float dist = (playerx * playerx) + (playerz * playerz);
            if (dist > (shielSize * size) * (shielSize * size))
            {
                WarningPanel.SetActive(true);
                OutofArea = true;
            }
            else
            {
                WarningPanel.SetActive(false);
                OutofArea = false;
            }
        }

        if (PhotonScriptor.ConnectingScript.informPlayerID() == 2)
        {
            if (TimeManager.MainPhaze.InformMainphaze() == false)
            {
                WarningPanel.SetActive(false);
                return;
            }
            playerx = Player2.transform.position.x;
            playerz = Player2.transform.position.z;
            size = ShieldPivot.transform.localScale.x;

            float dist = (playerx * playerx) + (playerz * playerz);
            if (dist > (shielSize * size) * (shielSize * size))
            {
                WarningPanel.SetActive(true);
                OutofArea2 = true;
            }
            else
            {
                WarningPanel.SetActive(false);
                OutofArea2 = false;
            }
        }



    }

    static public bool OutOfAreaInform()
    {
        return OutofArea; 
    }

    static public bool OutOfAreaInform2()
    {
        return OutofArea2;
    }
}
