using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class WeaponSwap : MonoBehaviour
    {
        [SerializeField]
        private GameObject AssultRifle;
        [SerializeField]
        private GameObject SideArm;
        [SerializeField]
        private int PN = 0;

        static private bool NowWeapon = false;
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
            if (Input.mouseScrollDelta.y != 0 && Swap == false)
            {
                Debug.Log("swap now");
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
                Debug.Log(NowWeapon);
            }
            if(NowWeapon == false)
            {
                AssultRifle.SetActive(true);
                SideArm.SetActive(false);
            }
            else
            {
                SideArm.SetActive(true);
                AssultRifle.SetActive(false);
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
    }
}
