using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PrepareTimeDesp : MonoBehaviour
    {
        [SerializeField]
        private Text TimeDesper;
        private float TimeLeft;

        void Update()
        {
            TimeLeft = TimeManager.PreParationTime.InformPreparationLeft();
            if (TimeLeft > 10)
            {
                TimeDesper.text = "00:" + (int)TimeLeft;
            }
            else
            {
                TimeDesper.text = "00:0" + (int)TimeLeft;
            }
        }
    }
}
