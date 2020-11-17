using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class YouCantChange : MonoBehaviour
    {
        [SerializeField]
        GameObject CannotCgangeText;
        
        void Update()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
            {
                if (WinorLose.InformBluePoint() >= WinorLose.InformRedPoint())
                {
                    CannotCgangeText.SetActive(true);
                }
                else
                {
                    CannotCgangeText.SetActive(false);
                }
            }
            else
            {
                if (WinorLose.InformBluePoint() <= WinorLose.InformRedPoint())
                {
                    CannotCgangeText.SetActive(true);
                }
                else
                {
                    CannotCgangeText.SetActive(false);
                }
            }
        }
    }
}
