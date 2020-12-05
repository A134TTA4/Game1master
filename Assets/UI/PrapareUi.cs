using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PrapareUi : MonoBehaviour
    {
        [SerializeField]
        private GameObject PareaparePanel;
        static private bool preparepanelState = true;


        private void Start()
        {
            PareaparePanel.SetActive(false);
            preparepanelState = false;
        }

        void Update()
        {
            if(TimeManager.PreParationTime.InformPreparationState() == false )
            {
                //if(preparepanelState ==true)
                {
                    preparepanelState = false;
                    PareaparePanel.SetActive(false);
                }
            }
            if (TimeManager.PreParationTime.InformPreparationState() == true)
            {
                //if (preparepanelState == false)
                {
                    preparepanelState = true ;
                    PareaparePanel.SetActive(true);
                }
            }
        }

        static public void ResetPrepare()//リセット関数
        {
            preparepanelState = true;
        }


    }
}