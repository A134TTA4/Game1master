using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    namespace BluePrint
    {

        public class DrawModeController : MonoBehaviour
        {
            [SerializeField]
            Text DrawModeText;

            void Update()
            {
                if(Player.BluePrint.DrawBluePrint.InformDrawMode() == 1)
                {
                    DrawModeText.text = "MODE:DRAWING";
                }
                else
                {
                    DrawModeText.text = "MODE:ERASING";
                }
            }
        }
    }
}
