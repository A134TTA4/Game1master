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
        private GameObject Launchar;
        [SerializeField]
        private int PN = 0;
        static private bool Q = false;
        static private bool nowQ = false;
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
            Q = false;
        }

        void Update()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() != PN)
            {
                return;
            }
            if (TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true)
            {
                return;
            }
            if(TimeManager.MainPhaze.InformMainphaze() ==false)
            {
                return;
            }
            if (TimeManager.IntercalTimeManager.InformIntervalState() == true)
            {
                return;
            }
            if ((Input.GetKeyDown(KeyCode.Q) || Input.mouseScrollDelta.y != 0) && Swap == false)
            {
                //Debug.Log("swap now");
                Swap = true;
                if (Input.GetKeyDown(KeyCode.Q) && Q == false)
                {
                    Q = true;
                }
                else if (Q == true)
                {
                    Q = false;
                }
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
                if (nowQ == true)
                {
                    NowWeapon = true;
                }
                //Debug.Log(NowWeapon);
            }
            if (NowWeapon == false && (WeaPonNow == true || nowQ == true) && Swap == false)
            {
                photonView.RPC(nameof(SwapToAR), RpcTarget.All);
            }
            if (NowWeapon == true && (WeaPonNow == false || nowQ == true) && Swap == false)
            {
                photonView.RPC(nameof(SwapToSideArm), RpcTarget.All);
            }
            if (Q == true && nowQ == false && Swap ==false)
            {
                photonView.RPC(nameof(SwapToLaunchar), RpcTarget.All);
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

        static public bool InformQ()
        {
            return nowQ;
        }

        [PunRPC]
        public void SwapToAR()
        {
            //Debug.Log("RPC executed");
            AssultRifle.SetActive(true);
            SideArm.SetActive(false);
            Launchar.SetActive(false);
            WeaPonNow = false;
            nowQ = false;
        }

        [PunRPC]
        public void SwapToSideArm()
        {
            //Debug.Log("RPC executed");
            SideArm.SetActive(true);
            AssultRifle.SetActive(false);
            Launchar.SetActive(false);
            WeaPonNow = true;
            nowQ = false;
        }

        [PunRPC]
        public void SwapToLaunchar()
        {
            SideArm.SetActive(false);
            AssultRifle.SetActive(false);
            Launchar.SetActive(true);
            WeaPonNow = true;
            nowQ = true;
        }
    }

    
    
}
