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
        private int MagazineBefores;
        private int MagazineNow;
        private int MagazineNows;

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
            MagazineBefores = Shoot.InformMagazineLefts();
            if(MagazineBefore < MagazineNow && Player.WeaponSwap.InformWeapon() == false)
            {
                photonView.RPC(nameof(ShootSound), RpcTarget.All);
            }
            if(MagazineBefores < MagazineNows && Player.WeaponSwap.InformWeapon() == true)
            {
                photonView.RPC(nameof(ShootSound), RpcTarget.All);
            }
            MagazineNow = MagazineBefore;
            MagazineNows = MagazineBefores;
        }

        [PunRPC]
        void ShootSound()
        {
            GunAudioSource.PlayOneShot(ShotSound);
            //Debug.Log("Shot Sound active");
        }

    }
}
