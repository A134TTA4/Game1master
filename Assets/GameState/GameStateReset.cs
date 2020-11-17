using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameState
{
    public class GameStateReset : MonoBehaviour
    {
        static private bool ResetGame = false;

        private void Start()
        {
            ResetGame = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (ResetGame == true)
            {
                Debug.Log("reset Game");
                TimeManager.MainPhaze.ResetMainphaze();
                TimeManager.TimeCounter.ResetTime();
                TimeManager.PreParationTime.PraparationReset();

                ResetGame = false;
            }

        }

        static public void SetReset()
        {
            ResetGame = true;
        }

        static public bool InformReset()
        {
            return ResetGame;
        }
    }
}