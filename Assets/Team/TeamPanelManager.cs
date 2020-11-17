using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team
{
    public class TeamPanelManager : MonoBehaviour
    {
        private int PlayerID = -1;
        [SerializeField]
        private GameObject BlueTeamText;
        [SerializeField]
        private GameObject RedTeamText;
        private bool teamState = false;

        private void Update()
        {
            
            PlayerID = PhotonScriptor.ConnectingScript.informPlayerID();
            
            if (PlayerID != -1)
            {
                teamState = true;
                if (PlayerID == 1)
                {
                    //Debug.Log("Team1 Panel Active");
                    RedTeamText.SetActive(false);
                    BlueTeamText.SetActive(true);
                }
                if (PlayerID == 2)
                {
                    //Debug.Log("Team2 Panel Active");
                    RedTeamText.SetActive(true);
                    BlueTeamText.SetActive(false);
                }
            }
        }
    }
}
