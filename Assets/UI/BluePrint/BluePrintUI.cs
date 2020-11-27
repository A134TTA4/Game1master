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

            private bool BluePrintStart = false;
            void Start()
            {
                UI.SetActive(false);
            }

            void Update()
            {
                if (TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true && BluePrintStart == false)
                {
                    BluePrintStart = true;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    BlueprintUIeffect.BluePrintEffectOnM();
                    UI.SetActive(true);
                }
                else if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == false)
                {
                    UI.SetActive(false);
                }
            }
        }
    }
}
