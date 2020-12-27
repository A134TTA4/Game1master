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
        [SerializeField]
        private GameObject Humanoid;
        private Vector3 HumanoidOriginScale;
        void Start()
        {
            playerTrans = PlayerObject.transform;
            Originscale = playerTrans.localScale;
            HumanoidOriginScale = Humanoid.transform.localScale;
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
                Humanoid.transform.localScale = HumanoidOriginScale;
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
            Humanoid.transform.localScale = HumanoidOriginScale;
            ClouchOrNot = false;
        }

        static public bool InformClouch()
        {
            return ClouchOrNot;
        }

    }
}
