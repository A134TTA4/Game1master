using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    namespace BluePrint
    {
        public class ClynderController : MonoBehaviour
        {
            [SerializeField]
            GameObject Hasira1;
            [SerializeField]
            GameObject Hasira2;
            private float count = 0f;
            private float Max = 0.5f;
            private bool SetComplete = false;
            Vector3 startPosition = new Vector3();
            [SerializeField]
            GameObject FX;
            // Start is called before the first frame update
            void Start()
            {
                float R = Random.Range(0, 10);
                Debug.Log(R);
                if(R >= 5)
                {
                    Hasira1.SetActive(true);
                }
                else
                {
                    Hasira2.SetActive(true);
                }
                startPosition = this.transform.position;
                this.transform.position = new Vector3(this.transform.position.x,  20, this.transform.position.z);
            }

            // Update is called once per frame
            void Update()
            {
                if (SetComplete == false)
                {
                    count += Time.deltaTime;
                    if (count > Max)
                    {
                        count = Max;
                    }
                    
                    this.transform.position = new Vector3(this.transform.position.x, (20 - (count * count / Max / Max * 20)), this.transform.position.z);
                    if (count >= Max * 0.93)
                    {
                        FX.SetActive(true);
                    }
                    if (count >= Max)
                    {
                        SetComplete = true;

                        this.transform.position = startPosition;
                    }
                }
            }
        }
    }
}