using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace PhotonScriptor
{
    public class LinkProperty : MonoBehaviourPunCallbacks
    {
        static ExitGames.Client.Photon.Hashtable roomHash;
        static private int nowHP1 = 100;
        static private int nowHP2 = 100;
        static private int PlayerHP1 = 100;
        static private int PlayerHP2 = 100;
        static private int[,] BlackMap1 = new int[256, 256];
        static private int[,] BlackMap2 = new int[256, 256];
        private bool mapReady = false;
        static private bool GetReady = false;
        void Start()
        {
            GetReady = false;
            mapReady = false;
            nowHP1 = 100;
            nowHP2 = 100;
            PlayerHP1 = 100;
            PlayerHP2 = 100;
            roomHash = new ExitGames.Client.Photon.Hashtable();
            roomHash.Add("HP1", PlayerHP1);
            roomHash.Add("HP2", PlayerHP2);
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
        }

        void Update()
        {
            
            if(TimeManager.MainPhaze.InformMainphaze() == false)
            {
                return;
            }

            if (Player.PlayerHp.InformPlayerHP() < PlayerHP1)
            {
                PlayerHP1 = (int)Player.PlayerHp.InformPlayerHP();
                roomHash["HP1"] = PlayerHP1;
                PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
            }
            if (Player.PlayerHP2.InformPlayerHP() < PlayerHP2)
            {
                PlayerHP2 = (int)Player.PlayerHP2.InformPlayerHP();
                roomHash["HP2"] = PlayerHP2;
                PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
            }
            if (nowHP1 < PlayerHP1)
            {
                PlayerHP1 = nowHP1;
            }
            if (nowHP2 < PlayerHP2)
            {
                PlayerHP2 = nowHP2;
            }
        }

        //ルームプロパティが変更されたときに呼ばれる
        public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            object value = null;
            if (propertiesThatChanged.TryGetValue("HP1", out value))
            {
                Debug.Log("Link HP1");
                nowHP1 = (int)value;
            }
            if (propertiesThatChanged.TryGetValue("HP2", out value))
            {
                Debug.Log("Link HP2");
                nowHP2 = (int)value;
            }
            /*
            if (propertiesThatChanged.TryGetValue("BM1", out value))
            {
                Debug.Log("Link BlackMap1");
                BlackMap1 = (int[,])value;
                if(PhotonScriptor.ConnectingScript.informPlayerID()==2)
                {
                    GetReady = true;
                }
            }
            if (propertiesThatChanged.TryGetValue("BM2", out value))
            {
                Debug.Log("Link BlackMap2");
                BlackMap2 = (int[,])value;
                if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                {
                    GetReady = true;
                }
            }
            */
        }

        static public int InformNowHP1()
        {
            return nowHP1;
        }

        static public int InformNowHP2()
        {
            return nowHP2;
        }

        static public int[,] InformBlackMap1()
        {
            return BlackMap1;
        }

        static public int[,] InformBlackMap2()
        {
            return BlackMap2;
        }


        static public bool InformgetReady()
        {
            return GetReady;
        }
        static public void ResetLink()
        {
            Debug.Log("reset Link");
            nowHP1 = 100;
            nowHP2 = 100;
            PlayerHP1 = 100;
            PlayerHP2 = 100;
            roomHash["HP1"] = PlayerHP1;
            roomHash["HP2"] = PlayerHP2;
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
        }
    }
}
