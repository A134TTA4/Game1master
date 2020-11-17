using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    namespace SettingPanel
    {
        public class GraficsController : MonoBehaviour
        {
            int Full = 1;
            int NotFull = 2;

            // Start is called before the first frame update
            void Start()
            {
                if (PlayerPrefs.GetInt("FullScreen", 1) == Full)
                {
                    Screen.fullScreen = true;
                }
                else
                {
                    Screen.fullScreen = false;
                }
            }

            // Update is called once per frame
            void Update()
            {

            }

            public void ToggleFullScreen()
            {
                Screen.fullScreen = !Screen.fullScreen;
                if(Screen.fullScreen == true)
                {
                    PlayerPrefs.SetInt("FullScreen", Full);
                }
                else
                {
                    PlayerPrefs.SetInt("FullScreen", NotFull);
                }
            }
        }
    }
}
