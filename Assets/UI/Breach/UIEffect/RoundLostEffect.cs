using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    namespace UIEffect
    {
        public class RoundLostEffect : MonoBehaviour
        {
            [SerializeField]
            private GameObject LoseText;
            [SerializeField]
            private GameObject startPosition;
            [SerializeField]
            private GameObject endPosition;
            static private bool LostEffect = false;

            private float Count = 0f;
            private float MoveCount = 1f;
            private float Max = 4f;
            // Start is called before the first frame update
            void Start()
            {
                LoseText.SetActive(false);
                LostEffect = false;
            }

            void Update()
            {
                if (LostEffect == true)
                {
                    if (Count == 0f)
                    {
                        LoseText.SetActive(true);
                        LoseText.transform.position = startPosition.transform.position;
                    }
                    Count += Time.deltaTime;
                    if (Count < MoveCount)
                    {
                        LoseText.transform.position += (endPosition.transform.position - startPosition.transform.position) * Time.deltaTime;
                    }
                    else if (Count >= MoveCount + 1)
                    {
                        LoseText.transform.position += (startPosition.transform.position - endPosition.transform.position) * Time.deltaTime / 2;
                    }
                    if (Count >= Max)
                    {
                        LostEffect = false;
                        Count = 0f;
                        LoseText.SetActive(false);

                    }
                }
            }

            static public void LoseEffectOnM()
            {
                LostEffect = true;
            }
        }
    }
}
