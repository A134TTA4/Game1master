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
            private Slider MouseSenseSlider_100;
            

            [SerializeField]
            private Text SenseValueDesp_100;


            [SerializeField]
            private Text Desplayer;

            static private float Sense = 1.0f;

            private void Start()
            {
                Sense = PlayerPrefs.GetFloat("MouseSensitivity", 0.5f);
                MouseSenseSlider_100.value = Sense;
                Desplayer.text = "" + Sense;
            }

            private void Update()
            {
                Sense = PlayerPrefs.GetFloat("MouseSensitivity",0.5f);
                MouseSenseSlider_100.value = Sense;
                Desplayer.text = "" + Sense;
            }

            


            public void GetSliderValue100()
            {
                Sense = MouseSenseSlider_100.value;
                Desplayer.text = "" + Sense;
                PlayerPrefs.SetFloat("MouseSensitivity", Sense);
                PlayerPrefs.Save();
            }

            public void SetSliderValue()
            {
                Sense = float.Parse(SenseValueDesp_100.text);
                MouseSenseSlider_100.value = Sense;
                Desplayer.text = "" + Sense;
                PlayerPrefs.SetFloat("MouseSensitivity", Sense);
                PlayerPrefs.Save();
            }


            static public float InformSense()
            {
                return Sense;
            }
        }
    }
}
