
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    namespace SettingPanel
    {
        public class AudioController : MonoBehaviour
        {
            [SerializeField]
            private Slider AudioSlider;
            static private float AudioValue = 1.0f;
            [SerializeField]
            private Text AudioValueDesp;
            private void Update()
            {
                AudioValue = PlayerPrefs.GetFloat("AudioFloat", 0.5f);
                AudioSlider.value = AudioValue;
                AudioValueDesp.text = "" + AudioValue;
            }
            public void GetAudioValue()
            {
                AudioValue = AudioSlider.value;
                PlayerPrefs.SetFloat("AudioFloat", AudioValue);
                AudioValueDesp.text = "" + AudioValue;
                PlayerPrefs.Save();
            }

            static public float InformAudioValue()
            {
                return AudioValue;
            }
        }
    }
}
