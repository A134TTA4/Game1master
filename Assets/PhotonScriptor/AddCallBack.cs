using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace PhotonScriptor
{
    public class AddCallBack : MonoBehaviour, IPunOwnershipCallbacks
    {
        private bool addcallBacks = false;
        private bool TakeOverOwnership = false;
        [SerializeField]
        private int PN;
        [SerializeField]
        PhotonView _photonView;

        /*void Start()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }*/

        void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        
        
        private void Update()
        {
            if (PhotonScriptor.ConnectingScript.informConnected() == true)
            {
                if (addcallBacks == false)
                {
                    addcallBacks = true;
                    Debug.Log("add call back");
                    PhotonNetwork.AddCallbackTarget(this);
                }
            }

            if (_photonView.IsMine == true)
            {
                return;
            }
            
            if (TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true)
            {
                if (PN == ConnectingScript.informPlayerID())
                {
                    if (TakeOverOwnership == false)
                    {
                        Debug.Log("owmership requested");
                        _photonView.RequestOwnership();
                        TakeOverOwnership = true;
                    }
                }
            }
        }

        void IPunOwnershipCallbacks.OnOwnershipRequest(PhotonView targetView, Photon.Realtime.Player requestingPlayer)
        {
            if (targetView.IsMine)
            {
                bool acceptsRequest = true;
                if (acceptsRequest)
                {
                    Debug.Log("accept request");
                    targetView.TransferOwnership(requestingPlayer);
                }
                
            }
        }

        void IPunOwnershipCallbacks.OnOwnershipTransfered(PhotonView targetView, Photon.Realtime.Player previousOwner)
        {
            string id = targetView.ViewID.ToString();

            Debug.Log($"ViewID {id} の所有権が移譲されました");
        }
    }
}