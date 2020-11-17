using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    namespace Breach
    {
        public class BreachLeftCountroller : MonoBehaviour
        {
            [SerializeField]
            private Text BreachLeftText;

            void Update()
            {
                BreachLeftText.text =  Player.BrakeWall.PLShootBrake.InformBrakeMagazineLeft().ToString();
            }
        }
    }
}
