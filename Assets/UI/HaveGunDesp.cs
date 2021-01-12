using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HaveGunDesp : MonoBehaviour
    {
        [SerializeField]
        private Text HaveGunText;
        void Update()
        {

            if (TimeManager.MainPhaze.InformMainphaze() == false)
            {
                return;
            }

            if (Player.WeaponSwap.InformWeapon() == false)
            {
                HaveGunText.text = "RIFLE\nMAGAZINE";
            }
            else
            {
                HaveGunText.text = "SIDEARM\nMAGAZINE";
            }
        }
    }
}
