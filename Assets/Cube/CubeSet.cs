using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;

namespace Cube
{
    public class CubeSet : MonoBehaviour
    {
        [SerializeField]
        private int PN;
        [SerializeField]
        GameObject CreatedCubeBig;
        [SerializeField]
        GameObject CreatedCubeSmall;
        [SerializeField]
        Material CubeMaterial;
        
        private bool CanCreate = true;
        static private int CreateLimit = 4;
        static private int Created = 0;
        private float rotSpeed = 1.0f;

        private void Start()
        {
            Created = 0;
        }

        void Update()
        {
            CreateCubeRotM();
            CreateCubeM();
        }

        void CreateCubeM()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() != PN)
            {
                return;
            }

            if (UI.SettingPanel.SettingPanelController.InformPanelState() == true)
            {
                return;
            }

            if (UI.RoomNoPanel.RoomNoPanelController.InformPanelState() == true)
            {
                return;
            }

            if (TimeManager.PreParationTime.InformPreparationState() == false)
            {
                return;
            }

            if(CanCreate == false)
            {
                CubeMaterial.color = new Color(1, 0, 0, 0.7f);
                return;
            }
            else
            {
                CubeMaterial.color = new Color(0, 0, 1, 0.7f);
            }

            if (Created == CreateLimit)
            {
                return;
            }
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        PhotonNetwork.Instantiate("CreatedCubeSmallBlue", this.transform.position, transform.rotation);
                        Created++;
                    }
                    else
                    {
                        PhotonNetwork.Instantiate("CreatedCubeBigBlue", this.transform.position, transform.rotation);
                        Created++;
                    }
                }
            }
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 2)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        PhotonNetwork.Instantiate("CreatedCubeSmall", this.transform.position, transform.rotation);
                        Created++;
                    }
                    else
                    {
                        PhotonNetwork.Instantiate("CreatedCubeBig", this.transform.position, transform.rotation);
                        Created++;
                    }
                }
            }

        }

        void CreateCubeRotM()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() != PN)
            {
                return;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                this.transform.Rotate(Vector3.up, rotSpeed);
            }
            if (Input.GetKey(KeyCode.E))
            {
                this.transform.Rotate(-1 * Vector3.up, rotSpeed);
            }
        }

        static public void resetCreated()
        {
            Created = 0;
        }

        static public int CubeLeftInformer()
        {
            return CreateLimit - Created;
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.CompareTag("Cube"))
            {
                CanCreate = false;
            }
            if(other.CompareTag("Separate"))
            {
                CanCreate = false;
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Cube"))
            {
                CanCreate = true;
            }
            if(other.CompareTag("Separate"))
            {
                CanCreate = true;
            }
        }

    }
}
