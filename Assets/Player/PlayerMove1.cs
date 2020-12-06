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
        private float fowardSpeed = 5.0f;
        private float backwardSpeed = 5.0f;
        private float leftSpeed = 6.0f;
        private float rightSpeed = 6.0f;
        private float groundspeed = 1;
        private int SprintMul = 1;
        private float ADSMul = 1;
        private float ClouchMul = 1;
        private float HitStopMul = 1;
        private bool isDash = false;
        static private bool isWalking = false;
        
        void Start()
        {
            playerTrans = player.transform;
        }

        void Update()
        {
            isDash = false;
            if(PhotonScriptor.ConnectingScript.informPlayerID() != PN)
            {
                return;
            }

            if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true)
            {
                return;
            }
            if(PLCameraFocus.InformForcusState() == true)
            {
                ADSMul = 0.7f;
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

            if(PN == 1)
            {

            }
            else
            {
                if(Player.PlayerHP2.InformHitStop() == true)
                {
                    HitStopMul = 0.7f;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (SprintMul == 1 && isDash == false)
                {
                    SprintMul = 2;
                    isDash = true;
                }
                if (SprintMul == 2 && isDash == false)
                {
                    SprintMul = 1;
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftControl))//しゃがみダッシュ禁止
            {
                SprintMul = 1;
            }
            if (AnimationConrollScripts.PLMoveAnimeControl.InformJumping() == true)//ジャンプ中は早く動けない
            {
                groundspeed = 0.5f;
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
                playerTrans.position += playerTrans.forward * Time.deltaTime * fowardSpeed * groundspeed * SprintMul* ClouchMul * ADSMul * HitStopMul;
                isWalking = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerTrans.position += -1 * playerTrans.forward * Time.deltaTime * backwardSpeed * groundspeed * ClouchMul * ADSMul * HitStopMul;
                isWalking = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerTrans.position += -1 * playerTrans.right * Time.deltaTime * leftSpeed * groundspeed * ClouchMul * ADSMul * HitStopMul;
                isWalking = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerTrans.position += playerTrans.right * Time.deltaTime * rightSpeed * groundspeed * ClouchMul * ADSMul * HitStopMul;
                isWalking = true;
            }
        }

        static public bool InformWalking()
        {
            //Debug.Log("siWalking refferd");
            return isWalking;
        }
    }
}
