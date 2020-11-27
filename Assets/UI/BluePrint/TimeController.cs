using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    namespace BluePrint
    {
        public class TimeController : MonoBehaviour
        {
            [SerializeField]
            private Text Timer;
            private int time;

            void Update()
            {
                time = TimeManager.BluePrint.BruePrintPhaze.InformTimesLeft();
                if (time >= 60)
                {
                    Timer.text = "0" + (time / 60).ToString() + ":" + (time % 60).ToString();
                }
                else if (time >= 10)
                {
                    Timer.text = "00:" + time.ToString();
                }
                else
                {
                    Timer.text = "00:0" + time.ToString();
                }

            }

        }
    }
}
