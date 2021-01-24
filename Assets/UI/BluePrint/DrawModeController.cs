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
                int leftblue = Player.BluePrint.DrawBluePrint.InformBlueLeft() / 20;
                int leftyellow = Player.BluePrint.DrawBluePrint.InfromYellowLeft() / 20;
                int leftgreen = Player.BluePrint.DrawBluePrint.InformGreenLeft() / 20;
                if(Player.BluePrint.DrawBluePrint.InformDrawMode() == 1)
                {
                    DrawModeText.text = "MODE:ATTACK";
                    for(int i = 0; i < leftblue; i++)
                    {
                        DrawModeText.text += "|";
                    }
                    DrawModeText.color = Color.blue;
                    
                }
                else if(Player.BluePrint.DrawBluePrint.InformDrawMode() == 2)
                {
                    DrawModeText.text = "MODE:JUMP";
                    for (int i = 0; i < leftyellow; i++)
                    {
                        DrawModeText.text += "|";
                    }
                    DrawModeText.color = Color.yellow;
                }
                else if (Player.BluePrint.DrawBluePrint.InformDrawMode() == 3)
                {
                    DrawModeText.text = "MODE:SPRINT";
                    for (int i = 0; i < leftgreen; i++)
                    {
                        DrawModeText.text += "|";
                    }
                    DrawModeText.color = Color.green;
                }
                else
                {
                    DrawModeText.text = "MODE:EMPTY";
                    DrawModeText.color = Color.red;
                }
            }
        }
    }
}
