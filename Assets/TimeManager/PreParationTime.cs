
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager
{
    public class PreParationTime : MonoBehaviour
    {
        private float nowTime = 0f;
        static private float preparationStart = 0f;
        static private float preparationEnd = 20f;
        static private float preparationLeft ;
        static private bool preparationEndbool = false;
        [SerializeField]
        private Transform PlayerTrans;
        [SerializeField]
        private Transform PlayerTrans2;
        private Vector3 StartPosition;
        private Vector3 StartPosition2;

        static private bool PreparationState = true;

        static private bool PreparationRestart = false;
        void Start()
        {
            StartPosition = PlayerTrans.position;
            StartPosition2 = PlayerTrans2.position;
            preparationLeft = preparationStart - preparationEnd;
        }

        void Update()
        {
            nowTime = TimeCounter.TimeInformer();

            if(MainPhaze.InformMainphaze() == true)
            {
                preparationEndbool = false;
            }

            if (PreparationRestart == true)
            {
                MainPhaze.ResetMainphaze();
                Debug.Log("restart Preparation");
                PreparationRestart = false;
                /////準備ラウンド再開の準備
                Player.PlayerHp.ResetPlayerHP();
                Player.PlayerHP2.ResetPlayerHP();
                PlayerTrans.position = StartPosition;
                PlayerTrans2.position = StartPosition2;
                Cube.CubeSet.resetCreated();
            }
            if (nowTime >= preparationStart && nowTime <= preparationEnd)
            {
                preparationLeft = preparationEnd - nowTime;
                PreparationState = true;
                return;
            }
            if (nowTime >= preparationEnd)
            {
                PreparationState = false;
                preparationEndbool = true;
                preparationLeft = 0f;
            }
        }

        static public bool InformPreparationState()
        {
            return PreparationState;
        }

        static public float InformPreparationLeft()
        {
            return preparationLeft;
        }

        static public void PraparationReset()
        {
            PreparationRestart = true;
            preparationEndbool = false;
            preparationLeft = preparationStart - preparationEnd;
        }

        static public bool PreparationEndInform()
        {
            return preparationEndbool;
        }
    }
}