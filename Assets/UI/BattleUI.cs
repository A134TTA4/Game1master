using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField]
    private GameObject battlePanel;
    static private bool battlePanelState = false;
    void Start()
    {
        battlePanel.SetActive(false);
        battlePanelState = false;
    }

    void Update()
    {
        if(TimeManager.IntercalTimeManager.InformIntervalState() == true)
        {
            battlePanel.SetActive(false);
        }

        if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true)
        {
            battlePanel.SetActive(false);
        }


        if(TimeManager.MainPhaze.InformMainphaze() == true)
        {
            if(battlePanelState == false)
            {
                battlePanelState = true;
                battlePanel.SetActive(true);
            }
        }
        if (TimeManager.MainPhaze.InformMainphaze() == false)
        {
            if (battlePanelState == true)
            {
                battlePanelState = false;
                battlePanel.SetActive(false);
            }
        }
    }

}
