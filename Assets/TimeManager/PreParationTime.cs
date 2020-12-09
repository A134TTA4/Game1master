
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager
{
    public class PreParationTime : MonoBehaviour
    {
        static private float nowTime = 0f;
        static private float preparationStart = 1f;
        static private float preparationEnd = 20f;
        static private float preparationLeft ;
        [SerializeField]
        private Transform PlayerTrans;
        [SerializeField]
        private Transform PlayerTrans2;
        private Vector3 StartPosition;
        private Vector3 StartPosition2;

        static private bool PreparationState = false;

        static private bool PreparationRestart = false;
        void Start()
        {
            nowTime = 0f;
            PreparationState = false;
            PreparationRestart = false;
            StartPosition = PlayerTrans.position;
            StartPosition2 = PlayerTrans2.position;
            preparationLeft = preparationStart - preparationEnd;
        }

        void Update()
        {
            
            nowTime = TimeCounter.TimeInformer();


            if (PreparationRestart == true)
            {
                UI.PrapareUi.ResetPrepare();
                Debug.Log("restart Preparation");
                PreparationRestart = false;
                /////準備ラウンド再開の準備
                Player.PlayerHp.ResetPlayerHP();
                Player.PlayerHP2.ResetPlayerHP();
                PlayerTrans.position = StartPosition;
                PlayerTrans2.position = StartPosition2;
                Cube.CubeSet.resetCreated();
                Cube.CubeSet.DeclimentLimit();
            }
            if (nowTime >= preparationStart && nowTime <= preparationEnd)
            {
                preparationLeft = preparationEnd - nowTime;
                PreparationState = true;
                return;
            }
            if (nowTime >= preparationEnd && PreparationState == true)
            {
                PreparationState = false;
                preparationLeft = 0f;
                MainPhaze.StartMainPhaze();
            }
        }

        static public bool InformPreparationState()
        {
            return PreparationState;
        }

        static public void StartPreparation()
        {
            PreparationState = true;
            nowTime = 0f;
        }


        static public float InformPreparationLeft()
        {
            return preparationLeft;
        }

        static public void PraparationReset()
        {
            PreparationRestart = true;
            preparationLeft = preparationStart - preparationEnd;
        }


    }
}