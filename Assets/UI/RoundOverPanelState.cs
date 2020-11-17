using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace UI
{
    public class RoundOverPanelState : MonoBehaviour
    {
        [SerializeField]
        private GameObject RoundOverPanel;
        private bool roundOverbool = false;

        void Start()
        {
            RoundOverPanel.SetActive(false);
        }


        void Update()
        {
            if (TimeManager.IntercalTimeManager.InformIntervalState() == true)
            {
                if(roundOverbool == false)
                {
                    Debug.Log("Round Over Panel Active");
                    roundOverbool = true;
                    RoundOverPanel.SetActive(true);
                }   
            }
            if (TimeManager.IntercalTimeManager.InformIntervalState() == false)//リセットが知らされたらリセットする
            {
                if (roundOverbool == true)
                {
                    roundOverbool = false;
                    RoundOverPanel.SetActive(false);
                }
            }

        }

    }
}
