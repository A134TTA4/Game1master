using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager 
{
    public class MainPhaze : MonoBehaviour
    {
        static private bool MainphazeState = false;
        [SerializeField]
        private Transform player1trans;
        private Vector3 playerStartPosition;
        [SerializeField]
        private Transform player2trans;
        private Vector3 playerStartPosition2;
        void Start()
        {
            playerStartPosition = player1trans.position;
            playerStartPosition2 = player2trans.position;
        }


        void Update()
        {
            //Debug.Log(MainphazeState);
            if (PreParationTime.PreparationEndInform() == true)
            {
                if (MainphazeState == false)
                {
                    Debug.Log("start MainPhaze");
                    MainphazeState = true;
                    Player.PlayerHp.ResetPlayerHP();
                    Player.PlayerHP2.ResetPlayerHP();
                    player1trans.position = playerStartPosition;
                    player2trans.position = playerStartPosition2;
                }
            }
        }

        static public bool InformMainphaze()
        {
            return MainphazeState;
        }


        static public void ResetMainphaze()
        {
            MainphazeState = false;
        }

    }
}
