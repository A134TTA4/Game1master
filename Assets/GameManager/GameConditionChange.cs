using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace UI
{
    public class GameConditionChange : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
    {
        [SerializeField]
        private PhotonView _photonView;

        public void AreaSpeedHigh()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
            {
                if (WinorLose.InformRedPoint() > WinorLose.InformBluePoint())
                {
                    _photonView.RequestOwnership();
                    photonView.RPC(nameof(High), RpcTarget.All);
                }
            }
            else if (PhotonScriptor.ConnectingScript.informPlayerID() == 2)
            {
                if (WinorLose.InformRedPoint() < WinorLose.InformBluePoint())
                {
                    _photonView.RequestOwnership();
                    photonView.RPC(nameof(High), RpcTarget.All);
                }
            }
            else
            {
                Debug.Log("You Cannnot Change State");
            }
        }


        [PunRPC]
        public void High()
        {
            Shield.shieldScale.RateHigh();
            Debug.Log("RPC Executed");
        }

        
        void AreaSpeedMidium()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
            {
                if (WinorLose.InformRedPoint() > WinorLose.InformBluePoint())
                {
                    _photonView.RequestOwnership();
                    photonView.RPC(nameof(Midium), RpcTarget.All);
                }
            }
            else if (PhotonScriptor.ConnectingScript.informPlayerID() == 2)
            {
                if (WinorLose.InformRedPoint() < WinorLose.InformBluePoint())
                {
                    _photonView.RequestOwnership();
                    photonView.RPC(nameof(Midium), RpcTarget.All);
                }
            }
            else
            {
                Debug.Log("You Cannnot Change State");
            }
        }


        [PunRPC]
        void Midium()
        {
            Shield.shieldScale.RateMidium();
            Debug.Log("RPC Executed");
        }

        public void AreaSpeedLow()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
            {
                if (WinorLose.InformRedPoint() > WinorLose.InformBluePoint())
                {
                    _photonView.RequestOwnership();
                    photonView.RPC(nameof(Low), RpcTarget.All);
                }
            }
            else if (PhotonScriptor.ConnectingScript.informPlayerID() == 2)
            {
                if (WinorLose.InformRedPoint() < WinorLose.InformBluePoint())
                {
                    _photonView.RequestOwnership();
                    photonView.RPC(nameof(Low), RpcTarget.All);
                }
            }
            else
            {
                Debug.Log("You Cannnot Change State");
            }
        }

        [PunRPC]
        void Low()
        {
            Shield.shieldScale.RateLow();
            Debug.Log("RPC Executed");
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
            string p1 = previousOwner.NickName;
            string p2 = targetView.Owner.NickName;
            Debug.Log($"ViewID {id} の所有権が {p1} から {p2} に移譲されました");
        }
    }
}
