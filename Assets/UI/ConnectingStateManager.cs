using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ConnectingStateManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject ConnectingStatePanel;

        private void Start()
        {
            ConnectingStatePanel.SetActive(true);
        }

        void Update()
        {
            if(TimeManager.TimeCounter.InformStartGame() == true)
            {
                ConnectingStatePanel.SetActive(false);
            }
            if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true)//試験用
            {
                ConnectingStatePanel.SetActive(false);
            }
        }
    }
}
