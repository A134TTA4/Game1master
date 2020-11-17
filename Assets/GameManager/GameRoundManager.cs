using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
namespace GameManager
{ 
    public class GameRoundManager : MonoBehaviour
    {
        static private int RoundCount = 0;
        static private bool RoundEnd = false;


        private void Start()
        {
            RoundCount = 0;
            RoundEnd = false;
        }
        void Update()
        {
            if(RoundEnd == true)
            {
                Debug.Log("Round End True");
                RoundCount++;
                RoundEnd = false;
                TimeManager.IntercalTimeManager.setIntervalTrue();
            }
        }

        static public int InformRoundCounter()
        {
            return RoundCount;
        }
        
        static public void SetEndM()
        {
            RoundEnd = true;
        }
    }
}
