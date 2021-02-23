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
        [SerializeField]
        Rigidbody PLRifgid;

        Transform playerTrans;
        private float fowardSpeed = 2.5f;
        private float backwardSpeed = 2.5f;
        private float leftSpeed = 2.5f;
        private float rightSpeed = 2.5f;
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

        private float PLVx;
        private float PLVz;
        private float Rad;
        private float NRad;
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
            PLVx = 0;
            PLVz = 0;
            isWalking = false;
            if (Input.GetKey(KeyCode.W))
            {
                isWalking = true;
                PLVz +=  -1*fowardSpeed * groundspeed * SprintMul * ClouchMul * ADSMul * HitStopMul * BuffMul * Wstop;
            }
            else
            {
                isDash = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                
                isWalking = true;
                PLVz += backwardSpeed * groundspeed * ClouchMul * ADSMul * HitStopMul * BuffMul * Sstop;
            }
            if (Input.GetKey(KeyCode.A))
            {
                isWalking = true;
                PLVx +=  -1*leftSpeed * groundspeed * ClouchMul * ADSMul * HitStopMul * BuffMul * Astop;
                A = true;
            }
            else
            {
                A = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                isWalking = true;
                PLVx +=   rightSpeed * groundspeed * ClouchMul * ADSMul * HitStopMul * BuffMul * Dstop;
                D = true;
            }
            else
            {
                D = false;
            }
            Rad = player.transform.position.y * Mathf.Deg2Rad;
            NRad = Mathf.PI / 2 - Rad;
            Vector3 v1 = new Vector3(PLVx, 0, PLVz);
            Vector3 v2 = new Vector3(Vector3.Dot(v1, playerTrans.right), PLRifgid.velocity.y,  -1*Vector3.Dot(v1, playerTrans.forward));
            PLRifgid.velocity = v2;
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
