using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundEffects
{
    public class ReloadSoundEffect : MonoBehaviour
    {
        [SerializeField]
        private AudioClip ReloadSoundClip;
        [SerializeField]
        private AudioSource ReloadSource;
        [SerializeField]
        private int PN;
        private bool ReloadReady = true;
        private float AudioVolume = 0.2f;
        void Update()
        {
            ReloadSource.volume = AudioVolume * UI.SettingPanel.AudioController.InformAudioValue();
            if (PN != PhotonScriptor.ConnectingScript.informPlayerID())
            {
                return;
            }
            if (Shoot.InformReloadState() == true && ReloadReady == true)
            {
                ReloadSource.PlayOneShot(ReloadSoundClip);
                ReloadReady = false;
            }
            if (ReloadReady == false && Shoot.InformReloadState() == false)
            {
                ReloadReady = true;
            }
        }
    }
}
