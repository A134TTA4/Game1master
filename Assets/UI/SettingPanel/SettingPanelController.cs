using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    namespace SettingPanel
    {
        public class SettingPanelController : MonoBehaviour
        {
            [SerializeField]
            private GameObject SettingPanelObj;

            static private bool SettingPanelState = false;


            [SerializeField]
            private bool LobbyOrNot = false;
            void Start()
            {
                SettingPanelObj.SetActive(false);
            }

            void Update()
            {
                if (Input.GetKeyDown(KeyCode.Escape) && SettingPanelState == false)
                {
                    SettingPanelState = true;
                    SettingPanelObj.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    return;
                }
                if (Input.GetKeyDown(KeyCode.Escape) && SettingPanelState == true)
                {
                    SettingPanelState = false;
                    SettingPanelObj.SetActive(false);
                    if(LobbyOrNot == true)
                    {
                        return;
                    }
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    return;
                }
                
            }

            static public bool InformPanelState()
            {
                return SettingPanelState;
            }
        }
    }
}
