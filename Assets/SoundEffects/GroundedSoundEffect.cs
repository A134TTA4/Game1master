using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundEffects
{
    public class GroundedSoundEffect : MonoBehaviour
    {
        [SerializeField]
        private AudioClip GroundedSoundClip;
        [SerializeField]
        private AudioSource GroundedSource;
        [SerializeField]
        private int PN;

        static private bool junpends = true;
        static private bool intheAir = false;
        private float AudioVolume = 0.5f;

        private void Start()
        {
            intheAir = true;
        }
        void Update()
        {
            GroundedSource.volume = AudioVolume * UI.SettingPanel.AudioController.InformAudioValue();
            if (PN != PhotonScriptor.ConnectingScript.informPlayerID())
            {
                return;
            }


            if (Player.PlayerJump.InformJump() == true)
            {
                intheAir = true;
                //Debug.Log("intheAir = true");
            }

            if (intheAir == true && junpends == true)
            {
                //GroundedSource.PlayOneShot(GroundedSoundClip);
                intheAir = false;
                junpends = false;
            }
        }

        static public void InTheAirTrue()
        {
            intheAir = true;
        }

        static public void IntheAirFalse()
        {
            junpends = true;
        }
    }
}
