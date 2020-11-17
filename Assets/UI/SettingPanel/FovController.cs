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

            private void Start()
            {
                FovValue = PlayerPrefs.GetFloat("FovFloat", 90f);
                FovSlider.value = FovValue;
            }
            public void GetFovValue()
            {
                FovValue = FovSlider.value;
                PlayerPrefs.SetFloat("FovFloat", FovValue);
            }

            static public float InformFovValue()
            {
                return FovValue;
            }
        }
    }
}
