using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMove1 : MonoBehaviour
    {
        [SerializeField]
        private int PN;

        [SerializeField]
        GameObject player;
        Transform playerTrans;
        [SerializeField]
        private float fowardSpeed = 3.0f;
        private float backwardSpeed = 3.0f;
        private float leftSpeed = 3.0f;
        private float rightSpeed = 3.0f;
        private float groundspeed = 1;
        private float SprintMul = 1;
        private float HitStopMul = 1;
        private float ADSMul = 1;
        private float ClouchMul = 1;
        private float BuffMul = 1;
        static private bool isDash = false;
        static private bool isWalking = false;
        static private bool A = false;
        static private bool D = false;

        static public float Wstop = 1;
        static public float Astop = 1;
        static public float Sstop = 1;
        static public float Dstop = 1;
        void Start()
        {
            isWalking = false;
            isDash = false;
            playerTrans = player.transform;
        }

        void Update()
        {
            if(PhotonScriptor.ConnectingScript.informPlayerID() != PN)
            {
                return;
            }

            if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true)
            {
                return;
            }

            if(PN ==1)
            {
                if(PlayerHp.InformHitStop())
                {
                    HitStopMul = 0.7f;
                }
                else
                {
                    HitStopMul = 1f;
                }
            }
            else
            {
                if (PlayerHP2.InformHitStop())
                {
                    HitStopMul = 0.7f;
                }
                else
                {
                    HitStopMul = 1f;
                }
            }

            if (PLCameraFocus.InformForcusState() == true)
            {
                ADSMul = 0.5f;
            }
            else
            {
                ADSMul = 1f;
            }

            if(PlayerClouch.InformClouch() == true)
            {
                ClouchMul = 0.4f;
            }
            else
            {
                ClouchMul = 1f;
            }

            if(BluePrint.DrawBluePrint.InformPlayerState() == 3)
            {
                BuffMul = 1.5f;
            }
            else
            {
                BuffMul = 1.0f;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isDash = !isDash;
                if (isDash == true)
                {
                    SprintMul = 1.8f;
                }
                if (isDash == false)
                {
                    SprintMul = 1;
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftControl))//しゃがみダッシュ禁止
            {
                SprintMul = 1;
            }
            if (AnimationConrollScripts.PLMoveAnimeControl.InformJumping() == true)//ジャンプ中は早く動ける
            {
                groundspeed = 1.1f;
            }
            if (AnimationConrollScripts.PLMoveAnimeControl.InformJumping() == false)
            {
                groundspeed = 1.0f;
            }
            characterMove();
        }

        private void characterMove()
        {
            isWalking = false;
            if (Input.GetKey(KeyCode.W))
            {
                playerTrans.position += playerTrans.forward * Time.deltaTime * fowardSpeed * groundspeed * SprintMul* ClouchMul * ADSMul * HitStopMul * BuffMul * Wstop;
                isWalking = true;
            }
            else
            {
                isDash = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerTrans.position += -1 * playerTrans.forward * Time.deltaTime * backwardSpeed * groundspeed * ClouchMul * ADSMul * HitStopMul * BuffMul * Sstop;
                isWalking = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerTrans.position += -1 * playerTrans.right * Time.deltaTime * leftSpeed * groundspeed * ClouchMul * ADSMul * HitStopMul * BuffMul * Astop;
                isWalking = true;
                A = true;
            }
            else
            {
                A = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerTrans.position += playerTrans.right * Time.deltaTime * rightSpeed * groundspeed * ClouchMul * ADSMul * HitStopMul * BuffMul *Dstop;
                isWalking = true;
                D = true;
            }
            else
            {
                D = false;
            }
        }

        static public bool InformWalking()
        {
            return isWalking;
        }

        static public bool InformDash()
        {
            return isDash;
        }

        static public bool InformA()
        {
            return A;
        }

        static public bool InfromD()
        {
            return D;
        }

    }
}
