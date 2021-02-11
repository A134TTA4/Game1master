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
        private Rigidbody playerRigid;


        static private bool JumpBool = false;
        private float jumpSpeed = 5;
        private float Count = 0f;
        private Ray ray;
        private RaycastHit hit;
        private Vector3 rayPosition;

        void Start()
        {
            playerRigid = PlayerObject.GetComponent<Rigidbody>();
        }

        void Update()
        {
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
            if (JumpBool)
            {
                //Debug.Log("Jumpend");
                SoundEffects.GroundedSoundEffect.IntheAirFalse();
                JumpBool = false;
                Count = 0f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigid.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                JumpBool = true;
                SoundEffects.GroundedSoundEffect.InTheAirTrue();
                //Debug.Log("Jumptrue");
            }
        }

        private bool boolGrounded()
        {
            if(BluePrint.DrawBluePrint.InformPlayerState() == 2)
            {
                jumpSpeed = 8.3f;
            }
            else
            {
                jumpSpeed = 5;
            }
            rayPosition = transform.position + new Vector3(0, -0.4f, 0);
            ray = new Ray(rayPosition, transform.up * -1);
            if (Physics.Raycast(ray, out hit, 0.4f))
            {
 
                if (hit.collider)
                {
                    return true;
                }
            }
            return false;
        }

        static public bool InformJump()
        {
            return JumpBool;
        }

    }

   
}
