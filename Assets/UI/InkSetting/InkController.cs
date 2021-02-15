using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    namespace InkSetting
    {
        public class InkController : MonoBehaviour
        {
            [SerializeField]
            Text Ink1;
            [SerializeField]
            Text Ink2;
            [SerializeField]
            Text Ink3;
            [SerializeField]
            Text Ink4;
            [SerializeField]
            Text InkLeft;
            private int[] ink = new int[5];
            private bool canincrease = false;
            private int Max = 2000;
            private int inkMax = 850;
            void Start()
            {
                ink[1] =PlayerPrefs.GetInt("ink1",500);
                ink[2] =PlayerPrefs.GetInt("ink2",500);
                ink[3] =PlayerPrefs.GetInt("ink3",500);
                ink[4] =PlayerPrefs.GetInt("ink4",500);
            }

            // Update is called once per frame
            void Update()
            {
                if(Max <= (ink[1] + ink[2] + ink[3] + ink[4]))
                {
                    canincrease = false;
                }
                else
                {
                    canincrease = true;
                }
                Ink1.color = Color.blue;
                Ink1.text = "ATTACK:";
                for(int n = ink[1]/20; n >0;n--)
                {
                    Ink1.text += "|";
                }

                Ink2.color = Color.yellow;
                Ink2.text = "JUMP:";
                for (int n = ink[2] / 20; n > 0; n--)
                {
                    Ink2.text += "|";
                }

                Ink3.color = Color.green;
                Ink3.text = "SPEED:";
                for (int n = ink[3] / 20; n > 0; n--)
                {
                    Ink3.text += "|";
                }

                Ink4.color = Color.gray;
                Ink4.text = "WALL:";
                for (int n = ink[4] / 20; n > 0; n--)
                {
                    Ink4.text += "|";
                }
                Debug.Log(ink[3]);

                InkLeft.text = "INK LEFT:";
                for (int n = (Max - (ink[1] + ink[2] + ink[3] + ink[4]))/ 20 ;n > 0; n--)
                {
                    InkLeft.text += "|";
                }
            }

            public void IncreaseInk1()
            {
                if (canincrease == true && ink[1] <= inkMax-20)
                {
                    ink[1] += 20;
                    PlayerPrefs.SetInt("ink1", ink[1]);
                    PlayerPrefs.Save();
                }
                
            }

            public void IncreaseInk2()
            {
                if (canincrease == true && ink[2] <= inkMax-20)
                {
                    ink[2] += 20;
                    PlayerPrefs.SetInt("ink2", ink[2]);
                    PlayerPrefs.Save();
                }
                
            }

            public void IncreaseInk3()
            {
                if (canincrease == true && ink[3] <= inkMax-20)
                {
                    ink[3] += 20;
                    PlayerPrefs.SetInt("ink3", ink[3]);
                    PlayerPrefs.Save();
                }
            }

            public void IncreaseInk4()
            {
                if (canincrease == true && ink[4] <= inkMax-20)
                {
                    ink[4] += 20;
                    PlayerPrefs.SetInt("ink4", ink[4]);
                    PlayerPrefs.Save();
                }
            }

            public void DecreaseInk1()
            {
                ink[1] -= 20;
                PlayerPrefs.SetInt("ink1", ink[1]);
                PlayerPrefs.Save();
            }

            public void DecreaseInk2()
            {
                ink[2] -= 20;
                PlayerPrefs.SetInt("ink2", ink[2]);
                PlayerPrefs.Save();
            }

            public void DecreaseInk3()
            {
                ink[3] -= 20;
                PlayerPrefs.SetInt("ink3", ink[3]);
                PlayerPrefs.Save();
            }

            public void DecreaseInk4()
            {
                ink[4] -= 20;
                PlayerPrefs.SetInt("ink4", ink[4]);
                PlayerPrefs.Save();
            }
        }
    }
}
