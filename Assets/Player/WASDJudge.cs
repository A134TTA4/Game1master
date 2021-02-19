using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class WASDJudge : MonoBehaviour
    {
        [SerializeField]
        private bool Whantei = false;
        [SerializeField]
        private bool Ahantei = false;
        [SerializeField]
        private bool Shantei = false;
        [SerializeField]
        private bool Dhantei = false;

        [SerializeField]
        private int PN;

        private bool Wstop = false;
        private bool Astop = false;
        private bool Sstop = false;
        private bool Dstop = false;

        void Update()
        {
            if(PN != PhotonScriptor.ConnectingScript.informPlayerID())
            {
                return;
            }
            if(Whantei == true && Wstop == true)
            {
                PlayerMove1.Wstop = 0;
            }
            else if(Whantei == true)
            {
                PlayerMove1.Wstop = 1;
            }

            if(Ahantei== true && Astop == true)
            {
                PlayerMove1.Astop = 0;
            }
            else if(Ahantei == true)
            {
                PlayerMove1.Astop = 1;
            }

            if (Shantei == true && Sstop == true)
            {
                PlayerMove1.Sstop = 0;
            }
            else if (Shantei == true)
            {
                PlayerMove1.Sstop = 1;
            }

            if(Dhantei == true &&Dstop ==true)
            {
                PlayerMove1.Dstop = 0;
            }
            else if(Dhantei == true)
            {
                PlayerMove1.Dstop = 1;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(Whantei == true)
            {
                Wstop = true;
            }
            else if (Ahantei == true)
            {
                Astop = true;
            }
            else if (Shantei == true)
            {
                Sstop = true;
            }
            else
            {
                Dstop = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (Whantei == true)
            {
                Wstop = false;
            }
            else if (Ahantei == true)
            {
                Astop = false;
            }
            else if (Shantei == true)
            {
                Sstop = false;
            }
            else
            {
                Dstop = false;
            }
        }
    }
}
