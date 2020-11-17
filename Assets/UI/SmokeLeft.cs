using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SmokeLeft : MonoBehaviour
    {
        [SerializeField]
        private Text SmokeFigure;

        void Update()
        {
            SmokeFigure.text = "" + Player.Smoke.UseSmoke.InformSmokeLeft();
        }


    }
}
