using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace Player
{
    namespace Smoke
    {
        public class UseSmoke : MonoBehaviourPunCallbacks
        {
            [SerializeField]
            private int PN;
            [SerializeField]
            private float SmokeSpeed = 10f;
            static private bool ableToUse = true;
            static private int MaxSmoke = 2;
            static private int Count = 0;

            private void Start()
            {
                Count = 0;
            }

            void Update()
            {
                
                if(UI.SettingPanel.SettingPanelController.InformPanelState() == true)
                {
                    return;
                }
                if(PN != PhotonScriptor.ConnectingScript.informPlayerID())
                {
                    return;
                }
                if(TimeManager.PreParationTime.InformPreparationState() == true)
                {
                    //Count = 0;
                    //Debug.Log("Smoke Reset");
                }
                if (TimeManager.MainPhaze.InformMainphaze() == false)
                {
                    return;
                }
                if(Shoot.InformReloadState() == true)
                {
                    return;
                }
                if (ableToUse == false)
                {
                    return;
                }

                if(Count == MaxSmoke)
                {
                    return;
                }
                if(Input.GetKeyDown(KeyCode.G))
                {
                    Debug.Log("Smoke is Out");
                    GameObject Smoke = PhotonNetwork.Instantiate("Smoke", this.transform.position + this.transform.forward * 1f,new Quaternion(0,0,0,0));
                    Rigidbody SmokeRigid = Smoke.GetComponent<Rigidbody>();
                    SmokeRigid.AddForce(this.transform.forward * SmokeSpeed, ForceMode.Impulse);
                    Count++;
                }
            }

            static public void AbletoUseM()
            {
                ableToUse = true;
            }

            static public void DisAbletoUseM()
            {
                ableToUse = false;
            }

            static public int InformSmokeLeft()
            {
                return MaxSmoke - Count;
            }
        }
    }
}
