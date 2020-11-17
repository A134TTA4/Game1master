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

        // Update is called once per frame
        void Update()
        {
            if(TimeManager.PreParationTime.InformPreparationState() == false )
            {
                if(preparepanelState ==true)
                {
                    preparepanelState = false;
                    PareaparePanel.SetActive(false);
                }
            }
            if (TimeManager.PreParationTime.InformPreparationState() == true)
            {
                if (preparepanelState == false)
                {
                    preparepanelState = true ;
                    PareaparePanel.SetActive(true);
                }
            }
        }

        private void ResetPrepare()//リセット関数
        {
            //PareaparePanel.SetActive(true);
            preparepanelState = true;
        }


    }
}