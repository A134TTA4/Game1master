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
        static private int CreateLimit = 4;
        static private int Created = 0;
        private float rotSpeed = 120.0f;
        private int SetMode = 1;

        private bool collision = false;
        private void Start()
        {
            CreateLimit = 4;
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

            CanCreate = true;
            if (TimeManager.MainPhaze.InformMainphaze() == true)
            {
                CanCreate = true;
                Created = 0;
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

            if(collision == true)
            {
                CanCreate = false;
            }

            if (Created == CreateLimit)
            {
                CanCreate = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetMode = 1;
                this.gameObject.transform.localScale = new Vector3(2,1,0.5f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (CubeLeftInformer() >= 2)
                {
                    SetMode = 2;
                    this.gameObject.transform.localScale = new Vector3(4, 1, 0.5f);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (CubeLeftInformer() >= 3)
                {
                    SetMode = 3;
                    this.gameObject.transform.localScale = new Vector3(6, 1, 0.5f);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (CubeLeftInformer() >= 4)
                {
                    SetMode = 4;
                    this.gameObject.transform.localScale = new Vector3(8, 1, 0.5f);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (CubeLeftInformer() >= 5)
                {
                    SetMode = 5;
                    this.gameObject.transform.localScale = new Vector3(10, 1, 0.5f);
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
                        cube.transform.localScale = new Vector3(2 * SetMode, 1.25f, 0.5f);
                        Created += 1 * SetMode;
                        ResetSetMode();
                    }
                    else
                    {
                        GameObject cube = PhotonNetwork.Instantiate("CreatedCubeSmallBlue", this.transform.position, transform.rotation);
                        cube.transform.localScale = new Vector3(2 * SetMode, 2, 0.5f);
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
                        cube.transform.localScale = new Vector3(2 * SetMode, 1.25f, 0.5f);
                        Created += 1 * SetMode;
                        ResetSetMode();
                    }
                    else
                    {
                        GameObject cube = PhotonNetwork.Instantiate("CreatedCubeSmall", this.transform.position, transform.rotation);
                        cube.transform.localScale = new Vector3(2 * SetMode, 2, 0.5f);
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
                this.transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {
                this.transform.Rotate(-1 * Vector3.up, rotSpeed * Time.deltaTime);
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
                collision = true;
            }
            if (other.CompareTag("Separate"))
            {
                CanCreate = false;
                collision = true;
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Cube"))
            {
                CanCreate = true;
                collision = false;
            }
            if (other.CompareTag("Separate"))
            {
                CanCreate = true;
                collision = false;
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
            this.gameObject.transform.localScale = new Vector3(2, 1, 0.2f);
        }

    }
}
