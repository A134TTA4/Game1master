using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shield
{
    public class shieldScale : MonoBehaviour
    {
        static private Vector3 shieldScaled;
        static private float sclace = 1.0f;
        static private float nowscale = 1.0f;
        private float minimumscale = 0.17f;
        static private float SmallerRate = 1.0f;
        static private bool shieldResetBool = false;

        void Start()
        {
            shieldScaled = this.transform.localScale;
        }


        void Update()
        {
            this.transform.localScale = shieldScaled;

            if (TimeManager.MainPhaze.InformMainphaze() == false)
            {
                return;
            }

            //timer += Time.deltaTime;
            if (TimeManager.MainPhaze.InformMainphaze() ==true)
            {
                nowscale -= (nowscale - minimumscale) / 30  * Time.deltaTime  * SmallerRate;
                shieldScaled.x = nowscale;
                shieldScaled.z = nowscale;
            }
        }

        static public void ShieldScaleReset()
        {
            nowscale = sclace;
            shieldScaled.x = nowscale;
            shieldScaled.z = nowscale;
        }

        static public void RateHigh()
        {
            SmallerRate = 3.0f;
        }

        static public void RateMidium()
        {
            SmallerRate = 1.8f;
        }

        static public void RateLow()
        {
            SmallerRate = 1.3f;
        }

        static public float InforSmallerRate()
        {
            return SmallerRate;
        }
    }
}
