using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
    namespace BluePrint
    {
        public class BlueprintUIeffect : MonoBehaviour
        {
            [SerializeField]
            private GameObject ContentText;
            [SerializeField]
            private GameObject startPosition;
            [SerializeField]
            private GameObject endPosition;
            static private bool BluePrintEffect = false;

            private float Count = 0f;
            private float MoveCount = 2f;
            private float Max = 8f;

            void Start()
            {
                ContentText.SetActive(false);
                BluePrintEffect = false;
            }

            void Update()
            {
                if (BluePrintEffect == true)
                {
                    if (Count == 0f)
                    {
                        ContentText.SetActive(true);
                        ContentText.transform.position = startPosition.transform.position;
                    }
                    Count += Time.deltaTime;
                    if (Count < MoveCount)
                    {
                        ContentText.transform.position += (endPosition.transform.position - startPosition.transform.position) * Time.deltaTime / 2;
                    }
                    else if (Count >= MoveCount + 3)
                    {
                        ContentText.transform.position += (startPosition.transform.position - endPosition.transform.position) * Time.deltaTime / 3;
                    }
                    if (Count >= Max)
                    {
                        BluePrintEffect = false;
                        Count = 0f;
                        ContentText.SetActive(false);

                    }
                }
            }

            static public void BluePrintEffectOnM()
            {
                BluePrintEffect = true;
            }
        }
    }
}
