using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField]
        private int PN;
        [SerializeField]
        private GameObject PlayerObject;
        static private Transform playerTrans;
        private Rigidbody playerRigid;


        static private float groundposition = 1f;
        static private bool JumpBool = false;
        private float jumpSpeed = 5;

        void Start()
        {
            playerTrans = PlayerObject.transform;
            playerRigid = PlayerObject.GetComponent<Rigidbody>();
            //groundposition = 1f;//playerTrans.position.y;
        }

        void Update()
        {
            playerTrans = PlayerObject.transform;
            if (playerTrans.position.y <= groundposition)
            {
                Playerjump();
            }

        }


        void Playerjump()
        {
            if( PhotonScriptor.ConnectingScript.informPlayerID() != PN)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigid.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                JumpBool = true;
            }
        }

        static public bool boolGrounded()
        {
            if (playerTrans.position.y <= groundposition)
            {
                return true;
            }
            return false;
        }
        static public bool InformJump()
        {
            return JumpBool;
        }

        static public void ReSetJumpBool()
        {
            JumpBool = false;
        }
    }

   
}
