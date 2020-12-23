using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TimeManager
{
    public class IntercalTimeManager : MonoBehaviour
    {
        private float IntrevalTime = 10f;
        static private float IntervalCounter = 0f;
        static private bool IntervalState = false;

        private void Start()
        {
            IntervalState = false;
            IntervalCounter = 0f;
        }

        void Update()
        {
            if (IntervalState == true)
            {
                IntervalCounter += Time.deltaTime;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (IntervalCounter >= IntrevalTime)
                {
                    IntervalState = false;
                    IntervalCounter = 0f;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    GameState.GameStateReset.SetReset();
                }
                if(UI.WinorLose.InformRedPoint() >= 3 || UI.WinorLose.InformBluePoint() >= 3)
                {
                    GameManager.GameFinisher.GameFinishSet();
                }
            }
        }

        static public bool InformIntervalState()
        {
            return IntervalState;
        }

        static public void setIntervalTrue()
        {
            IntervalState = true;
        }
    }
}
