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
        [SerializeField]
        private AudioClip SideArmAudioSource;
        [SerializeField]
        private AudioClip WeaponSwapSound;
        private int MagazineBefore;
        private int MagazineBefores;
        private int MagazineNow;
        private int MagazineNows;

        private float AudioVolume = 0.5f;
        private bool WeaponState = false;
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
            
            if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true)
            {
                return;
            }

            if(TimeManager.IntercalTimeManager.InformIntervalState() == true)
            {
                return;
            }

            if(Player.WeaponSwap.InformSwap() == true && WeaponState == false)
            {
                GunAudioSource.PlayOneShot(WeaponSwapSound);
                WeaponState = true;
            }
            if(Player.WeaponSwap.InformSwap() == false && WeaponState == true)
            {
                WeaponState = false;
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
            if (Player.WeaponSwap.InformWeapon() == false)
            {
                GunAudioSource.PlayOneShot(ShotSound);
            }
            else
            {
                GunAudioSource.PlayOneShot(SideArmAudioSource);
            }
        }

    }
}
