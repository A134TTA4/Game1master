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
            private Slider MouseSenseSlider_10;
            [SerializeField]
            private Slider MouseSenseSlider_1;

            [SerializeField]
            private Text SenseValueDesp_100;
            [SerializeField]
            private Text SenseValueDesp_10;
            [SerializeField]
            private Text SenseValueDesp_1;
            static private float Sense = 1.0f;
            static private float Sense_1 = 1.0f;
            static private float Sense_10 = 1.0f;
            static private float Sense_100 = 1.0f;

            private void Start()
            {
                Sense = PlayerPrefs.GetFloat("MouseSensitivity", 0.5f);
                MouseSenseSlider_100.value = (int)(Sense)*100;
                MouseSenseSlider_10.value = (int)((Sense*10)%10)*10;
                MouseSenseSlider_1.value = (int)((Sense*100)%10);
            }

            


            public void GetSliderValue100()
            {
                Sense_100 = MouseSenseSlider_100.value;
                SenseValueDesp_100.text =(int)Sense_100 * 100 + "+";
                Sense = Sense_100 * 100 + Sense_10 * 10 + Sense_1;
                PlayerPrefs.SetFloat("MouseSensitivity", Sense/100);
                PlayerPrefs.Save();
            }

            public void GetSliderValue10()
            {
                Sense_10 = MouseSenseSlider_10.value;
                SenseValueDesp_10.text = (int) Sense_10 * 10 + "+";
                Sense = Sense_100 * 100 + Sense_10 * 10 + Sense_1;
                PlayerPrefs.SetFloat("MouseSensitivity", Sense/100);
                PlayerPrefs.Save();
            }

            public void GetSliderValue1()
            {
                Sense_1 = MouseSenseSlider_1.value;
                SenseValueDesp_1.text = "" +(int)Sense_1 + "+";
                Sense = Sense_100 * 100 + Sense_10 * 10 + Sense_1;
                PlayerPrefs.SetFloat("MouseSensitivity", Sense/100);
                PlayerPrefs.Save();
            }


            static public float InformSense()
            {
                return Sense/100;
            }
        }
    }
}
