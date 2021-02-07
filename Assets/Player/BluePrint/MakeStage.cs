using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace Player
{
    namespace BluePrint
    {
        public class MakeStage : MonoBehaviourPunCallbacks
        {
            [SerializeField]
            GameObject Element;
            private int[,] mapList = new int[256, 256];
            private int[,] BlackPosition = new int[800, 800];
            private bool Stagemake = false;
            //private bool StagemakeE = false;
            void Start()
            {

            }

            void Update()
            {
                if (TimeManager.PreParationTime.InformPreparationState() == false)
                {
                    //return;
                }
                if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == false)
                {
                    return;
                }
                if(Input.GetKey(KeyCode.Mouse0))
                {
                    return;
                }
                if (Stagemake == false)
                {
                    instatineteStageElementM(DrawBluePrint.InformBlackMap());
                    //Stagemake = true;
                }

            }

            [PunRPC]
            private void instatineteStageElementM(int[,] mapList)
            {
                for (int i = 0; i < 256; i++)
                {
                    for (int j = 0; j < 256; j++)
                    {
                        if (mapList[i, j] == 4)
                        {
                            PhotonNetwork.Instantiate("Element", new Vector3((float)(i) / 256 * 40 - 20, 0, (float)(j) / 256 * 40 - 20), new Quaternion(0, 0, 0, 0));
                            for (int n = 0; n <= 2; n++)
                            {
                                for (int m = -2; m <= 2; m++)
                                {
                                    mapList[i + m, j + n] = 0;
                                }
                            }
                        }
                    }
                }
            }



            //////////////////////////////////////
            private void instatineteStageElementEM()
            {
                if(PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                {
                    mapList = PhotonScriptor.LinkProperty.InformBlackMap2();
                }
                else
                {
                    mapList = PhotonScriptor.LinkProperty.InformBlackMap1();
                }
                
                for (int i = 0; i < 256; i++)
                {
                    for (int j = 0; j < 256; j++)
                    {
                        if (mapList[i, j] == 4)
                        {
                            Instantiate(Element, new Vector3((float)(i) / 256 * 40 - 20, 0, (float)(j) / 256 * 40 - 20), new Quaternion(0, 0, 0, 0));
                            for (int n = 0; n <= 2; n++)
                            {
                                for (int m = -2; m <= 2; m++)
                                {
                                    mapList[i + m, j + n] = 0;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
