using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    
    public class GameNowProperty : MonoBehaviour
    {
        [SerializeField]
        Text NowPoints;
        private int RedWins;
        private int BlueWins;

        void Update()
        {
            RedWins = WinorLose.InformRedPoint();
            BlueWins = WinorLose.InformBluePoint();
            if(PhotonScriptor.ConnectingScript.informPlayerID() ==1)
            {
                NowPoints.text = BlueWins + " - " + RedWins;
            }
            else
            {
                NowPoints.text = RedWins + " - " + BlueWins;
            }
        }
    }
}
