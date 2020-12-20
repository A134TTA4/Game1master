using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerBuff : MonoBehaviour
    {
        [SerializeField]
        Text Bufftext;

        private bool none = false;
        void Update()
        {
            if(TimeManager.MainPhaze.InformMainphaze() == false)
            {
                return;
            }

            if (BluePrint.DrawBluePrint.InformPlayerState() == 1)
            {
                Bufftext.text = "Buff:ATTACK";
                Bufftext.color = Color.blue;
                none = false;
            }
            else if (BluePrint.DrawBluePrint.InformPlayerState() == 2)
            {
                Bufftext.text = "Buff:JUMP";
                Bufftext.color = Color.yellow;
                none = false;
            }
            else if (BluePrint.DrawBluePrint.InformPlayerState() == 3)
            {
                Bufftext.text = "Buff:SPEED";
                Bufftext.color = Color.green;
                none = false;
            }
            else
            {
                if (none == false)
                {
                    Bufftext.text = "Buff:NONE";
                    Bufftext.color = Color.red;
                    none = true;
                }
            }
        }
    }
}
