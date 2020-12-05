using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager
{
    namespace BluePrint
    {
        public class BruePrintPhaze : MonoBehaviour
        {
            static private bool BluePrintState = false;
            static private bool BluePrintStart = false;
            static private float Count = 0f;
            static private float MaxCount = 120;
            private bool Gamestart = false;
            void Start()
            {
                BluePrintState = false;
                BluePrintStart = false;
                Count = 0f;
            }

            void Update()
            {
                if(BluePrintState == true)
                {
                    Count += Time.deltaTime;
                }
                if(BluePrintStart == true)
                {
                    BluePrintState = true;
                    BluePrintStart = false;
                }
                if(Count >= MaxCount && Gamestart == false)
                {
                    BluePrintState = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    TimeCounter.StartGameM();
                    Gamestart = true;
                }
                
            }

            static public bool InformBluePrintState()
            {
                return BluePrintState;
            }

            static public void StartBluePrintM()
            {
                BluePrintStart = true;
            }

            static public int InformTimesLeft()
            {
                return (int)(MaxCount - Count);
            }
        }
    }
}
