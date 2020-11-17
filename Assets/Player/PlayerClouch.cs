using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerClouch : MonoBehaviour
    {
        [SerializeField]
        private int PN;
        [SerializeField]
        private GameObject PlayerObject;
        private Transform playerTrans;
        private Vector3 Originscale;
        static private bool ClouchOrNot = false;
        void Start()
        {
            playerTrans = PlayerObject.transform;
            Originscale = playerTrans.localScale;
        }


        void Update()
        {
            beOriginScale();
            PlayerClouchM();
        }

        void PlayerClouchM()
        {
            if(PhotonScriptor.ConnectingScript.informPlayerID() != PN)
            {
                return;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                playerTrans.localScale = new Vector3(Originscale.x, Originscale.y / 1.5f, Originscale.z);
                ClouchOrNot = true;
            }
        }

        void beOriginScale()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() != PN)
            {
                return;
            }
            playerTrans.localScale = Originscale;
            ClouchOrNot = false;
        }

        static public bool InformClouch()
        {
            return ClouchOrNot;
        }

    }
}
