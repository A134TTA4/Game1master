
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

            private void Start()
            {
                AudioValue = PlayerPrefs.GetFloat("AudioFloat", 0.5f);
                AudioSlider.value = AudioValue;
            }
            public void GetAudioValue()
            {
                AudioValue = AudioSlider.value;
                PlayerPrefs.SetFloat("AudioFloat", AudioValue);
            }

            static public float InformAudioValue()
            {
                return AudioValue;
            }
        }
    }
}
