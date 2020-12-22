using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SoundEffects
{
    public class MovingSoundEffect1 : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private AudioClip WalkingSound;
        [SerializeField]
        private AudioSource PlayerAudioSource;
        private bool SoundPlaying = false;
        private float Count = 0f;
        private float Max = 0.2f;
        private bool RPCis = false;
        private float AudioVolume = 0.5f;
        [SerializeField]
        private float PN;

        private void Start()
        {
            AudioVolume = PlayerAudioSource.volume;
        }
        void Update()
        {
            PlayerAudioSource.volume = AudioVolume * UI.SettingPanel.AudioController.InformAudioValue();
            if (PhotonScriptor.ConnectingScript.informPlayerID() != PN)
            {
                return;
            }
            if (Player.PlayerMove1.InformWalking() ==true)
            {
                RPCis = true;
                Count += Time.deltaTime;
                if (SoundPlaying == false && Count >= Max)
                {
                    photonView.RPC(nameof(WalkingSoundM), RpcTarget.All);
                }
            }
            else if(RPCis == true)
            {
                RPCis = false;
                Count = 0;
                photonView.RPC(nameof(WalkingSoundStop), RpcTarget.All);
            }
        }

        [PunRPC]
        void WalkingSoundM()
        {
            PlayerAudioSource.PlayOneShot(WalkingSound);
            SoundPlaying = true;
            //Debug.Log("walking sound");
        }

        [PunRPC]
        void WalkingSoundStop()
        {
            PlayerAudioSource.Stop();
            SoundPlaying = false;
        }
    }
}
