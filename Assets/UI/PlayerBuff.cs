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
                if (Bufftext == null)
                {
                    return;
                }
                Bufftext.text = "BUFF:ATTACK";
                Bufftext.color = Color.blue;
                none = false;
            }
            else if (BluePrint.DrawBluePrint.InformPlayerState() == 2)
            {
                if (Bufftext == null)
                {
                    return;
                }
                Bufftext.text = "BUFF:JUMP";
                Bufftext.color = Color.yellow;
                none = false;
            }
            else if (BluePrint.DrawBluePrint.InformPlayerState() == 3)
            {
                if (Bufftext == null)
                {
                    return;
                }
                Bufftext.text = "BUFF:SPEED";
                Bufftext.color = Color.green;
                none = false;
            }
            else if (BluePrint.DrawBluePrint.InformPlayerState() == 0)
            {
                if (Bufftext == null)
                {
                    return;
                }
                Bufftext.text = "BUFF:NONE";
                Bufftext.color = Color.red;
                none = true;

            }
        }
    }
}
