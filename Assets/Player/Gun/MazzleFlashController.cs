using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    namespace Gun
    {
        public class MazzleFlashController : MonoBehaviour
        {
            [SerializeField]
            private GameObject MazzleEffect;
            [SerializeField]
            private int PN;
            private bool Effecton = false;
            private float Count = 0f;
            private float Max = 0.08f;
            [SerializeField]
            private bool ADS;
            void Start()
            {
                MazzleEffect.SetActive(false);
            }
            void Update()
            {
                if(PN != PhotonScriptor.ConnectingScript.informPlayerID())
                {
                    return;
                }
                if (Shoot.shot == true)
                {
                    if (PLCameraFocus.InformForcusState() == ADS)
                    {
                        Effecton = true;
                        MazzleEffect.SetActive(true);
                    }
                }
                if (Effecton == true)
                {
                    Count += Time.deltaTime;
                    if (Count >= Max)
                    {
                        Effecton = false;
                        Count = 0f;
                        MazzleEffect.SetActive(false);
                    }
                }
            }

            
        }
    }
}
