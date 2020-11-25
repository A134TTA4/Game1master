using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    namespace BluePrint
    {
        public class BluePrintUI : MonoBehaviour
        {
            [SerializeField]
            private GameObject UI;
            void Start()
            {
                UI.SetActive(false);
            }

            // Update is called once per frame
            void Update()
            {
                if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    UI.SetActive(true);
                }
                else
                {
                    UI.SetActive(false);
                }
            }
        }
    }
}
