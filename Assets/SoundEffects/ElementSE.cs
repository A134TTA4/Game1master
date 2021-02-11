using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundEffects
{
    
    
    public class ElementSE : MonoBehaviour
    {
        [SerializeField]
        private AudioClip ElementSoundClip;
        [SerializeField]
        private AudioSource ElementAudioSource;
        
        private float AudioVolume = 0.5f;
        private float count = 0f;
        private bool Play = false;
        void Start()
        {
            AudioVolume = ElementAudioSource.volume;
        }

        void Update()
        {
            count += Time.deltaTime;
            ElementAudioSource.volume = AudioVolume * UI.SettingPanel.AudioController.InformAudioValue();
            if (Play == false && count > 0.3f)
            {
                Play = true;
                ElementAudioSource.PlayOneShot(ElementSoundClip);
            }
        }
    }
}
