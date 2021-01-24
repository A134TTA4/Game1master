using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    namespace SettingPanel
    {
        public class FovController : MonoBehaviour
        {
            [SerializeField]
            private Slider FovSlider;
            static private float FovValue = 90f;
            [SerializeField]
            private Text FovValueText;

            private void Awake()
            {
                FovValue = PlayerPrefs.GetFloat("FovFloat", 90f);
                FovSlider.value = FovValue;
                FovValueText.text = "" + FovValue;
            }
            public void GetFovValue()
            {
                FovValue = FovSlider.value;
                PlayerPrefs.SetFloat("FovFloat", FovValue);
                FovValueText.text = "" + FovValue;
            }

            public void InputFovValue()
            {
                FovValue =  float.Parse( FovValueText.text);
            }

            static public float InformFovValue()
            {
                return FovValue;
            }
        }
    }
}
