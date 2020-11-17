using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shield
{
    public class shieldScale : MonoBehaviour
    {
        static private float timer = 0.0f;
        Vector3 shieldScaled;
        static private float sclace = 1.0f;
        static private float nowscale = 1.0f;
        static private float beminmumEnd = 30;
        static private float SmallerRate = 1.0f;
        static private bool shieldResetBool = false;
        // Start is called before the first frame update
        void Start()
        {
            timer = 0f;
            shieldScaled = this.transform.localScale;
        }


        void Update()
        {
            if(shieldResetBool == true)
            {
                shieldResetBool = false;
                shieldScaled.x = nowscale;
                shieldScaled.z = nowscale;
                this.transform.localScale = shieldScaled;
            }

            if (TimeManager.MainPhaze.InformMainphaze() == false)
            {
                return;
            }

            timer += Time.deltaTime;
            if (timer < beminmumEnd)
            {
                nowscale = sclace + (timer / 30000 * SmallerRate);
                shieldScaled.x /= nowscale;
                shieldScaled.z /= nowscale;
                this.transform.localScale = shieldScaled;
            }
        }

        static public void ShieldScaleReset()
        {
            timer = 0f;
            nowscale = 1.0f;
            shieldResetBool = true;
        }

        static public void RateHigh()
        {
            SmallerRate = 3.0f;
            beminmumEnd = 22f;
        }

        static public void RateMidium()
        {
            SmallerRate = 1.0f;
            beminmumEnd = 30f;
        }

        static public void RateLow()
        {
            SmallerRate = 0.7f;
            beminmumEnd = 35f;
        }

        static public float InforSmallerRate()
        {
            return SmallerRate;
        }
    }
}
