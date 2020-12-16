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
                Bufftext.color = Color.blue;
            }
            else if (BluePrint.DrawBluePrint.InformPlayerState() == 2)
            {
                Bufftext.text = "Buff:JUMP";
                Bufftext.color = Color.yellow;
            }
            else if (BluePrint.DrawBluePrint.InformPlayerState() == 3)
            {
                Bufftext.text = "Buff:SPEED";
                Bufftext.color = Color.green;
            }
            else
            {
                Bufftext.text = "Buff:NONE";
                Bufftext.color = Color.red;
            }
        }
    }
}
