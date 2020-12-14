using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Photon.Pun;

namespace Player
{
    public class PlayerHP2 : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private int PN;
        [SerializeField]
        private GameObject PlayerBody;
        static private float PlayerHp2 = 100;
        static private float outOfAreaCount = 0f;

        static private float HitStopCount = 0f;
        static private float HitStopMax = 0.3f;
        static private bool HitStopBool = false;

        static private bool PlayerGetDamageB = false;

        private float priviousHP = 100;
        void Update()
        {
            PlayerGetDamageB = false;
            if (TimeManager.MainPhaze.InformMainphaze() == false)
            {
                return;
            }

            if(HitStopBool == true)
            {
                //Debug.Log("Hit Stop is Active");
                HitStopCount += Time.deltaTime;
                if(HitStopCount > HitStopMax)
                {
                    HitStopBool = false;
                    HitStopCount = 0;
                }
            }

            if(PhotonScriptor.LinkProperty.InformNowHP2() < PlayerHp2)
            {
                PlayerHp2 = PhotonScriptor.LinkProperty.InformNowHP2();
                Debug.Log("Renew HP2:" + PlayerHp2);
                HitStopBool = true;
                HitStopCount = 0;
            }

            if (priviousHP > PlayerHp2)
            {
                HitStopBool = true;
                HitStopCount = 0;
            }

            if (redPanel.OutOfAreaInform2() == true)
            {
                outOfAreaCount += Time.deltaTime;
                if (outOfAreaCount >= 1)
                {
                    PlayerHp2 -= 8;
                    outOfAreaCount = 0;
                }
            }
            if (redPanel.OutOfAreaInform2() == false)
            {
                outOfAreaCount = 0f;
            }
            if (PlayerHp2 <= 0)
            {
                if (PhotonScriptor.ConnectingScript.informPlayerID() != 2)
                {
                    photonView.RPC(nameof(PlayerDead), RpcTarget.All);
                }
            }

            priviousHP = PlayerHp2;
        }

        static public void PlayerGetDamage(float Damage)
        {
            PlayerGetDamageB = true;
            PlayerHp2 -= Damage;
            Debug.Log("get Damage");
            HitStopBool = true;
            HitStopCount = 0;
        }

        static public void PlayerGetDamageforme(float Damage)
        {
            PlayerHp2 -= Damage;
            Debug.Log("PL2 get Damage from Breach");
        }

        static public float InformPlayerHP()
        {
            return PlayerHp2;
        }
        
        static public void ResetPlayerHP()
        {
            PlayerHp2 = 100;
            outOfAreaCount = 0f;
        }

        [PunRPC]
        void PlayerDead()
        {
            Debug.Log("Died");
            Debug.Log("Blue Increased");
            PhotonScriptor.LinkProperty.ResetLink();
            UI.WinorLose.IncrementBlue();
            PlayerHp2 = 100;
            GameManager.GameRoundManager.SetEndM();
            TimeManager.MainPhaze.ResetMainphaze();
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
