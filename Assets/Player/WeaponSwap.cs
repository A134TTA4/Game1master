using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Player
{
    public class WeaponSwap : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject AssultRifle;
        [SerializeField]
        private GameObject SideArm;
        [SerializeField]
        private int PN = 0;

        static private bool NowWeapon = false;
        private bool WeaPonNow = false;
        static private bool Swap = false;
        private float count = 0f;
        private float countMax = 0.5f;
        void Start()
        {
            AssultRifle.SetActive(true);
            SideArm.SetActive(false);
            NowWeapon = false;
            Swap = false;
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
            if(TimeManager.IntercalTimeManager.InformIntervalState() == true)
            {
                return;
            }
            if (Input.mouseScrollDelta.y != 0 && Swap == false)
            {
                //Debug.Log("swap now");
                Swap = true;
            }
            if (Swap == true)
            {
                count += Time.deltaTime;
            }
            if (count >= countMax)
            {
                count = 0f;
                Swap = false;
                NowWeapon = !NowWeapon;
                //Debug.Log(NowWeapon);
            }
            if(NowWeapon == false && WeaPonNow == true)
            {
                photonView.RPC(nameof(SwapToAR), RpcTarget.All);
            }
            if(NowWeapon == true && WeaPonNow == false)
            {
                photonView.RPC(nameof(SwapToSideArm), RpcTarget.All);
            }

        }

        static public bool InformSwap()
        {
            return Swap;
        }

        static public bool InformWeapon()
        {
            return NowWeapon;
        }

        [PunRPC]
        public void SwapToAR()
        {
            //Debug.Log("RPC executed");
            AssultRifle.SetActive(true);
            SideArm.SetActive(false);
            WeaPonNow = false;
        }

        [PunRPC]
        public void SwapToSideArm()
        {
            //Debug.Log("RPC executed");
            SideArm.SetActive(true);
            AssultRifle.SetActive(false);
            WeaPonNow = true;
        }
    }

    
    
}
