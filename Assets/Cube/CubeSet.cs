using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

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
        static private int CreateLimit = 6;
        static private int Created = 0;
        private float rotSpeed = 1.0f;
        private int SetMode = 1;
        private void Start()
        {
            CreateLimit = 6;
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

            if (TimeManager.MainPhaze.InformMainphaze() == true)
            {
                CanCreate = true;
                
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

            if (Created == CreateLimit)
            {
                CanCreate = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetMode = 1;
                this.gameObject.transform.localScale = new Vector3(2,1,1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (CubeLeftInformer() >= 2)
                {
                    SetMode = 2;
                    this.gameObject.transform.localScale = new Vector3(4, 1, 1);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (CubeLeftInformer() >= 3)
                {
                    SetMode = 3;
                    this.gameObject.transform.localScale = new Vector3(6, 1, 1);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (CubeLeftInformer() >= 4)
                {
                    SetMode = 4;
                    this.gameObject.transform.localScale = new Vector3(8, 1, 1);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (CubeLeftInformer() >= 5)
                {
                    SetMode = 5;
                    this.gameObject.transform.localScale = new Vector3(10, 1, 1);
                }
            }



            if (CanCreate == false)
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
                        GameObject cube = PhotonNetwork.Instantiate("CreatedCubeSmallBlue", this.transform.position, transform.rotation);
                        cube.transform.localScale = new Vector3(2 * SetMode, 1.25f, 1);
                        Created += 1 * SetMode;
                        ResetSetMode();
                    }
                    else
                    {
                        GameObject cube = PhotonNetwork.Instantiate("CreatedCubeSmallBlue", this.transform.position, transform.rotation);
                        cube.transform.localScale = new Vector3(2 * SetMode, 2, 1);
                        Created += 1 * SetMode;
                        ResetSetMode();
                    }
                }
            }
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 2)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        GameObject cube =  PhotonNetwork.Instantiate("CreatedCubeSmall", this.transform.position, transform.rotation);
                        cube.transform.localScale = new Vector3(2 * SetMode, 1.25f, 1);
                        Created += 1 * SetMode;
                        ResetSetMode();
                    }
                    else
                    {
                        GameObject cube = PhotonNetwork.Instantiate("CreatedCubeSmall", this.transform.position, transform.rotation);
                        cube.transform.localScale = new Vector3(2 * SetMode, 2, 1);
                        Created += 1 * SetMode;
                        ResetSetMode();
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
            if (other.CompareTag("Cube"))
            {
                CanCreate = false;
            }
            if (other.CompareTag("Separate"))
            {
                CanCreate = false;
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Cube"))
            {
                CanCreate = true;
            }
            if (other.CompareTag("Separate"))
            {
                CanCreate = true;
            }
        }

        static public void DeclimentLimit()
        {
            CreateLimit--;
        }

        static public int InformLimit()
        {
            return CreateLimit;
        }

        private void ResetSetMode()
        {
            SetMode = 1;
            this.gameObject.transform.localScale = new Vector3(2, 1, 1);
        }

    }
}
