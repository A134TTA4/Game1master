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

            private bool strt = false;

            [SerializeField]
            private bool LobbyOrNot = false;
            void Start()
            {
                SettingPanelObj.SetActive(true);
            }

            void Update()
            {
                if(strt == false)
                {
                    SettingPanelObj.SetActive(false);
                    strt = true;
                }
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
                    if(TimeManager.PreParationTime.InformPreparationState() == false && TimeManager.MainPhaze.InformMainphaze() == false)
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
