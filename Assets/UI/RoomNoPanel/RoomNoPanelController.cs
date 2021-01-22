using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    namespace RoomNoPanel
    {
        
        public class RoomNoPanelController : MonoBehaviour
        {
            [SerializeField]
            Text RoomNo;
            [SerializeField]
            GameObject panel;

            static private bool panelState = true;
            private bool RoomSetEnd = false;
            void Update()
            {
                if(PhotonScriptor.ConnectingScript.informSetNoComplete() == true && RoomSetEnd ==false)
                {
                    panel.SetActive(false);
                    panelState = false;
                    RoomSetEnd = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

            public void SetNo()
            {
                if (int.TryParse(RoomNo.text, out int o))
                {
                    int No = int.Parse(RoomNo.text);
                    PhotonScriptor.ConnectingScript.SetRoomNo(No);
                    Debug.Log("RoomNo " + No);
                }
                else
                {
                    return;
                }

            }  

            static public bool InformPanelState()
            {
                return panelState;
            }
        }
    }
}
