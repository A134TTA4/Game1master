using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundEffects
{
    public class HitSound : MonoBehaviour
    {
        [SerializeField]
        private GameObject HitImage;
        [SerializeField]
        private AudioClip HitSoundClip;
        [SerializeField]
        private AudioSource GunAudioSource;
        [SerializeField]
        private int PN;

        private bool HitImageBool= false;
        private float Count = 0f;
        private float CountMax = 0.7f;

        private float AudioVolume;

        private void Start()
        {
            HitImageBool = false;
            HitImage.SetActive(false);
            Count = 0f;
            AudioVolume = GunAudioSource.volume;
        }
        void Update()
        {
            if(HitImageBool == true)
            {
                Count += Time.deltaTime;
                if(Count >= CountMax)
                {
                    HitImage.SetActive(false);
                    HitImageBool = false;
                    Count = 0f;
                }
            }

            GunAudioSource.volume = AudioVolume * UI.SettingPanel.AudioController.InformAudioValue();
            if (PN!=PhotonScriptor.ConnectingScript.informPlayerID())
            {
                return;
            }
            if(PN == 1)
            {
                if(Player.PlayerHP2.InformPlayerGetDamage() == true)
                {
                    GunAudioSource.PlayOneShot(HitSoundClip);
                    HitImage.SetActive(true);
                    HitImageBool = true;
                }
            }
            if(PN == 2)
            {
                if(Player.PlayerHp.InformPlayerGetDamage() == true)
                {
                    GunAudioSource.PlayOneShot(HitSoundClip);
                    HitImage.SetActive(true);
                    HitImageBool = true;
                }
            }
        }
    }
}
