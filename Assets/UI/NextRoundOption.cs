using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class NextRoundOption : MonoBehaviour
    {
        static private bool OptionSelected = false;
        static private bool MagSelected = false;
        static private bool CubSelected = false;

        [SerializeField]
        private GameObject MagincreasedDesp;
        [SerializeField]
        private GameObject CubincreasedDesp;
        void Start()
        {
            OptionSelected = false;
            MagSelected = false;
            CubSelected = false;
        }


        void Update()
        {

            if (MagSelected == true)
            {
                MagincreasedDesp.SetActive(true);
            }
            else
            {
                MagincreasedDesp.SetActive(false);
            }
            if (CubSelected == true)
            {
                CubincreasedDesp.SetActive(true);
            }
            else
            {
                CubincreasedDesp.SetActive(false);
            }

        }

        public void MagIncrease()
        {
            if (OptionSelected == false)
            {
                Player.BrakeWall.PLShootBrake.IncreaseBrakeWall();
                OptionSelected = true;
                MagSelected = true;
            }
        }

        public void CubIncrease()
        {
            if (OptionSelected == false)
            {
                Cube.CubeManager.IncreaseCube();
                OptionSelected = true;
                CubSelected = true;
            }
        }

        static public void ResetSelect()
        {
            OptionSelected = false;
            MagSelected = false;
            CubSelected = false;
        }
    }
}
