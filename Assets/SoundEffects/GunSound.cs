using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace SoundEffects
{
    public class GunSound : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private AudioClip ShotSound;
        [SerializeField]
        private AudioSource GunAudioSource;

        private int MagazineBefore;
        private int MagazineNow;

        private float AudioVolume = 0.5f;

        [SerializeField]
        private float PN;

        private void Start()
        {
            AudioVolume = GunAudioSource.volume;
        }

        void Update()
        {
            GunAudioSource.volume = AudioVolume * UI.SettingPanel.AudioController.InformAudioValue();
            if (PN != PhotonScriptor.ConnectingScript.informPlayerID())
            {
                return;
            }
            
            MagazineBefore = Shoot.InformMagazineLeft();
            if(MagazineBefore < MagazineNow)
            {
                photonView.RPC(nameof(ShootSound), RpcTarget.All);
            }
            MagazineNow = MagazineBefore;
        }

        [PunRPC]
        void ShootSound()
        {
            GunAudioSource.PlayOneShot(ShotSound);
            //Debug.Log("Shot Sound active");
        }

    }
}
