using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    namespace SettingPanel
    {
        public class MouseSenseController : MonoBehaviour
        {

            [SerializeField]
            private Slider MouseSenseSlider;
            static private float Sense = 1.0f;

            private void Start()
            {
                Sense = PlayerPrefs.GetFloat("MouseSensitivity", 0.5f);
                MouseSenseSlider.value = Sense;
            }

            public void GetSliderValue()
            {
                Sense = MouseSenseSlider.value;
                PlayerPrefs.SetFloat("MouseSensitivity", Sense);
            }

            static public float InformSense()
            {
                return Sense;
            }
        }
    }
}
