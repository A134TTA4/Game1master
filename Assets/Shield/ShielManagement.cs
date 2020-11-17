using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Shield
{
    public class ShielManagement : MonoBehaviour
    {
        [SerializeField]
        private GameObject Shield;
        static private bool shieldState = false;
        void Start()
        {
            Shield.SetActive(false);
        }

        void Update()
        {
            if(TimeManager.MainPhaze.InformMainphaze()==true)
            {
                if(shieldState == false)
                {
                    shieldState = true;
                    Shield.SetActive(true);
                    return;
                }
            }
            if (TimeManager.MainPhaze.InformMainphaze() == false)
            {
                if (shieldState == true)
                {
                    shieldScale.ShieldScaleReset();
                    shieldState = false;
                    Shield.SetActive(false);
                    return;
                }
            }
           

        }

        static public bool InformShieldState()
        {
            return shieldState;
        }
    }
}
