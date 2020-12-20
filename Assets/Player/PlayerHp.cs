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
        static private float HitStopMax = 0.3f;
        static private bool HitStopBool = false;

        static private bool PlayerGetDamageB = false;

        private float priviousHP = 100;
        void Update()
        {
            PlayerGetDamageB = false;
            if(TimeManager.MainPhaze.InformMainphaze() == false)
            {
                return;
            }

            if (HitStopBool == true)
            {
                Debug.Log("Hit Stop is Active");
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
                Debug.Log("Renew HP1:" + PlayerHP);
                HitStopBool = true;
                HitStopCount = 0;
                
            }

            if(priviousHP > PlayerHP)
            {
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
                    }

                }
            }
            if (redPanel.OutOfAreaInform() == false)
            {
                outOfAreaCount = 0f;
            }
            if (PlayerHP <= 0)
            {
                if (PhotonScriptor.ConnectingScript.informPlayerID() != 1)
                {
                    photonView.RPC(nameof(PlayerDead), RpcTarget.All);
                }
            }
            priviousHP = PlayerHP; 
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
            PhotonScriptor.LinkProperty.ResetLink();
            UI.WinorLose.IncrementRed();
            PlayerHP = 100;
            GameManager.GameRoundManager.SetEndM();
            TimeManager.MainPhaze.ResetMainphaze();
            UI.NextRoundOption.ResetSelect();
            PlayerBody.transform.position = new Vector3(0, -100, 0);
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
