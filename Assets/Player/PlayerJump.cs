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

        private Ray ray;
        private RaycastHit hit;
        private Vector3 rayPosition;

        void Start()
        {
            playerTrans = PlayerObject.transform;
            playerRigid = PlayerObject.GetComponent<Rigidbody>();
            //groundposition = 1f;//playerTrans.position.y;
        }

        void Update()
        {
            playerTrans = PlayerObject.transform;
            if (boolGrounded() == true)
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
            if(AnimationConrollScripts.PLMoveAnimeControl.InformJumping() == true)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigid.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                JumpBool = true;
            }
        }

        private bool boolGrounded()
        {
            if(BluePrint.DrawBluePrint.InformPlayerState() == 2)
            {
                jumpSpeed = 10;
            }
            else
            {
                jumpSpeed = 5;
            }
            rayPosition = transform.position + new Vector3(0, 0.5f, 0);
            ray = new Ray(rayPosition, transform.up * -1);
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            //Debug.Log("ray");
            if (Physics.Raycast(ray, out hit, 1.5f))
            {
                
                if (hit.collider)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
