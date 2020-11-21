using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    namespace UIEffect
    {
        
        public class RoundWinEffect : MonoBehaviour
        {
            [SerializeField]
            private GameObject WinText;
            [SerializeField]
            private GameObject startPosition;
            [SerializeField]
            private GameObject endPosition;
            static private bool WinEffect = false;

            private float Count = 0f;
            private float MoveCount = 1f;
            private float Max = 4f;

            void Start()
            {
                WinText.SetActive(false);
                WinEffect = false;
            }

            void Update()
            {
                if(WinEffect == true)
                {
                    if (Count == 0f)
                    {
                        WinText.SetActive(true);
                        WinText.transform.position = startPosition.transform.position;
                    }
                    Count += Time.deltaTime;
                    if(Count < MoveCount)
                    {
                        WinText.transform.position += (endPosition.transform.position - startPosition.transform.position) * Time.deltaTime;
                    }
                    else if(Count >= MoveCount+1)
                    {
                        WinText.transform.position += (startPosition.transform.position - endPosition.transform.position) * Time.deltaTime / 2;
                    }
                    if(Count >= Max)
                    {
                        WinEffect = false;
                        Count = 0f;
                        WinText.SetActive(false);

                    }
                }
            }

            static public void WinEffectOnM()
            {
                WinEffect = true;
            }
        }
    }
}