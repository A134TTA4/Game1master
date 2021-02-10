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
            private int[,] WrotePosition = new int[256, 256];
            private bool Stagemake = false;
            //private bool StagemakeE = false;
            private bool delay = false;
            private float count = 0f;
            private float max = 0.01f;
            void Start()
            {

            }

            void Update()
            {
                if (TimeManager.PreParationTime.InformPreparationState() == false)
                {
                    return;
                }
                if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == false)
                {
                    //return;
                }
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //delay = true;
                }
                
                
                instatineteStageElementM(DrawBluePrint.InformBlackMap());
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
                            if (WrotePosition[i, j] != 4)
                            {
                                PhotonNetwork.Instantiate("Element", new Vector3((float)(i) / 256 * 40 - 20, 0 + Random.Range(0,0.5f), (float)(j) / 256 * 40 - 20), new Quaternion(0, 0, 0, 0));
                                WrotePosition[i, j] = 4;
                                for (int n = 0; n <= 2; n++)
                                {
                                    for (int m = -2; m <= 2; m++)
                                    {
                                        WrotePosition[i + m, j + n] = 4;
                                    }
                                }
                                return;
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
