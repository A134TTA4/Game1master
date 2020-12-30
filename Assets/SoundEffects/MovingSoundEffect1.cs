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
        [SerializeField]
        private AudioClip RunAudio;
        [SerializeField]
        private AudioSource RunAudioSource;
        private bool SoundPlaying = false;
        private float Count = 0f;
        private float Max = 0.2f;
        private bool RPCis = false;
        private float AudioVolume = 0.5f;
        [SerializeField]
        private float PN;

        private bool Running = false;
        private void Start()
        {
            AudioVolume = PlayerAudioSource.volume;
        }
        void Update()
        {
            PlayerAudioSource.volume = AudioVolume * UI.SettingPanel.AudioController.InformAudioValue();
            RunAudioSource.volume = AudioVolume * UI.SettingPanel.AudioController.InformAudioValue() * 2.0f;
            if (PhotonScriptor.ConnectingScript.informPlayerID() != PN)
            {
                return;
            }
            if (Player.PlayerMove1.InformWalking() == true)
            {
                RPCis = true;
                Count += Time.deltaTime;
                if (SoundPlaying == false && Count >= Max)
                {
                    photonView.RPC(nameof(WalkingSoundM), RpcTarget.All);
                }
                if(Player.PlayerMove1.InformDash() == true && Running == false)
                {
                    photonView.RPC(nameof(WalkingSoundStop), RpcTarget.All);
                    photonView.RPC(nameof(RunSoundStop), RpcTarget.All);
                    photonView.RPC(nameof(RunSoundM), RpcTarget.All);
                    Running = true;
                    Count = 0;
                }
                if (Player.PlayerMove1.InformDash() == false && Running == true)
                {
                    photonView.RPC(nameof(WalkingSoundStop), RpcTarget.All);
                    photonView.RPC(nameof(RunSoundStop), RpcTarget.All);
                    photonView.RPC(nameof(WalkingSoundM), RpcTarget.All);
                    Running = false;
                }
            }
            else if (RPCis == true)
            {
                RPCis = false;
                Count = 0;
                photonView.RPC(nameof(WalkingSoundStop), RpcTarget.All);
                photonView.RPC(nameof(RunSoundStop), RpcTarget.All);
            }
            if (AnimationConrollScripts.PLMoveAnimeControl.InformJumping() == true)
            {
                RPCis = false;
                Count = 0;
                photonView.RPC(nameof(WalkingSoundStop), RpcTarget.All);
                photonView.RPC(nameof(RunSoundStop), RpcTarget.All);
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

        [PunRPC]
        void RunSoundM()
        {
            RunAudioSource.Play(0);
            SoundPlaying = true;
        }

        [PunRPC]
        void RunSoundStop()
        {
            RunAudioSource.Stop();
            SoundPlaying = false;
        }

        
    }
}
