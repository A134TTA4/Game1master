using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LobbyScript
{
    public class BGMController : MonoBehaviour
    {
        [SerializeField]
        AudioSource BGMSource;
        [SerializeField]
        private float volume = 0.5f;
        void Start()
        {
            BGMSource.volume = volume * UI.SettingPanel.AudioController.InformAudioValue();
        }

        // Update is called once per frame
        void Update()
        {
            BGMSource.volume = volume * UI.SettingPanel.AudioController.InformAudioValue();
        }
    }
}
