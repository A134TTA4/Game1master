using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    namespace BluePrint
    {
        public class Stage : MonoBehaviour
        {
            void Update()
            {
                if (TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true)
                {
                    this.gameObject.transform.position = new Vector3(0, 6f, 0);
                }
                else
                {
                    this.gameObject.transform.position = new Vector3(0, 0.1f, 0);
                }
            }
        }
    }

}
