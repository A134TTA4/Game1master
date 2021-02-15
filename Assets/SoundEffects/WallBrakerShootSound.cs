using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace SoundEffects
{
    public class WallBrakerShootSound : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private AudioClip ShootBrakeSound;
        [SerializeField]
        private AudioSource ShootBrakeAudioSource;
        [SerializeField]
        private float PN;
        private float SoundVolume = 0.5f;
        static private bool shot = false;
        void Start()
        {
            SoundVolume = ShootBrakeAudioSource.volume;
        }

        // Update is called once per frame
        void Update()
        {
            
            if(PN!=PhotonScriptor.ConnectingScript.informPlayerID())
            {
                return;
            }
            
            if (shot == true)
            {
                photonView.RPC(nameof(GrenadeLaunchSoundM), RpcTarget.All);
                shot = false;
            }
        }

        [PunRPC]
        private void GrenadeLaunchSoundM()
        {
            ShootBrakeAudioSource.PlayOneShot(ShootBrakeSound);
        }

        static public void shotTrueM()
        {
            shot = true;
        }
    }
}
