using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager 
{
    public class MainPhaze : MonoBehaviour
    {
        static private bool MainphazeState = false;
        static private bool MainPhazeStart = false;
        [SerializeField]
        private Transform player1trans;
        private Vector3 playerStartPosition;
        [SerializeField]
        private Transform player2trans;
        private Vector3 playerStartPosition2;
        void Start()
        {
            MainphazeState = false;
            MainPhazeStart = false;
            playerStartPosition = player1trans.position;
            playerStartPosition2 = player2trans.position;
        }


        void Update()
        {
            //Debug.Log(MainphazeState);
            if (MainphazeState == true)
            {
                if (MainPhazeStart == false)
                {
                    Debug.Log("start MainPhaze");
                    MainPhazeStart = true;
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
            MainPhazeStart = false;
        }

        static public void StartMainPhaze()
        {
            MainphazeState = true;
        }

    }
}
