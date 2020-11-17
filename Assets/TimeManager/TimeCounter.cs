using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace TimeManager
{
    public class TimeCounter : MonoBehaviour
    {
        static private float timeCount = 0f;
        static bool ResetTimeBool = false;
        static private bool StartGame = false;


        private void Start()
        {
            timeCount = 0f;
            StartGame = false;
            ResetTimeBool = false;
        }

        void Update()
        {
            if (StartGame)
            {
                timeCount += Time.deltaTime;  
            }

            if(ResetTimeBool == true)
            {
                ResetTimeBool = false;
                timeCount = 0f;
            }
        }

        static public float TimeInformer()
        {
            return timeCount;
        }

        static public void ResetTime()
        {
            StartGame = true;
            ResetTimeBool = true;
        }

        static public void StartGameM()
        {
            Debug.Log("start game");
            StartGame = true;
        }


        static public bool InformStartGame()
        {
            return StartGame;
        }
    }
}
