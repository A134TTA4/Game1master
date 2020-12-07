using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Photon.Pun;

namespace Player
{
    public class PlayerHp : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private int PN;
        [SerializeField]
        private GameObject PlayerBody;
        static private float PlayerHP = 100;
        static private float outOfAreaCount = 0f;

        static private float HitStopCount = 0f;
        static private float HitStopMax = 0.2f;
        static private bool HitStopBool = false;

        static private bool Playerdie = false;
        static private bool PlayerGetDamageB = false; 

        void Update()
        {
            PlayerGetDamageB = false;
            if(TimeManager.MainPhaze.InformMainphaze() == false)
            {
                return;
            }
            if (TimeManager.IntercalTimeManager.InformIntervalState() == true)
            {
                return;
            }

            if (HitStopBool == true)
            {
                HitStopCount += Time.deltaTime;
                if (HitStopCount > HitStopMax)
                {
                    HitStopBool = false;
                    HitStopCount = 0;
                }
            }

            if (PhotonScriptor.LinkProperty.InformNowHP1() < PlayerHP)
            {
                PlayerHP = PhotonScriptor.LinkProperty.InformNowHP1();
                Debug.Log("Renew HP1 " + PlayerHP);
                HitStopBool = true;
                HitStopCount = 0;
                
            }

            if (redPanel.OutOfAreaInform() == true)
            {
                { 
                    outOfAreaCount += Time.deltaTime;
                    if(outOfAreaCount >= 1)
                    {
                        PlayerHP -= 8;
                        outOfAreaCount = 0;
                        //Debug.Log("player Got Area Damage");
                    }

                }
            }
            if (redPanel.OutOfAreaInform() == false)
            {
                outOfAreaCount = 0f;
            }
            if (PlayerHP <= 0)
            {
                photonView.RPC(nameof(PlayerDead), RpcTarget.All);
            }
            
        }

        static public void PlayerGetDamage(float Damage)
        {
            HitStopBool = true;
            HitStopCount = 0;
            PlayerGetDamageB = true;
            PlayerHP -= Damage;
            Debug.Log("get Damage");
        }

        static public void PlayerGetDamageforme(float Damage)
        {
            PlayerHP -= Damage;
            Debug.Log("get Damage");
        }

        static public float InformPlayerHP()
        {
            return PlayerHP;
        }
        
        static public void ResetPlayerHP()
        {
            PlayerHP = 100;
            outOfAreaCount = 0f;
        }

        [PunRPC]
        void PlayerDead()
        {
            Debug.Log("Died");
            Debug.Log("Red Increased");
            UI.WinorLose.IncrementRed();
            PlayerHP = 100;
            GameManager.GameRoundManager.SetEndM();
            TimeManager.MainPhaze.ResetMainphaze();
            Playerdie = true;
        }

        static public bool InformPlayerGetDamage()
        {
            return PlayerGetDamageB;
        }

        static public bool InformHitStop()
        {
            return HitStopBool;
        }
    }
}
