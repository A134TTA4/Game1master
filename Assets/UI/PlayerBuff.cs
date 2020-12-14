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
        

        void Update()
        {
            if (BluePrint.DrawBluePrint.InformPlayerState() == 1)
            {
                Bufftext.text = "Buff:ATTACK";
            }
            else if (BluePrint.DrawBluePrint.InformPlayerState() == 2)
            {
                Bufftext.text = "Buff:JUMP";
            }
            else if (BluePrint.DrawBluePrint.InformPlayerState() == 3)
            {
                Bufftext.text = "Buff:SPEED";
            }
            else
            {
                Bufftext.text = "Buff:NONE";
            }
        }
    }
}
