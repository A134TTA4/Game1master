using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Player
{
    public class PLCameraFocus : MonoBehaviour
    {
        [SerializeField]
        private Camera PlayerCamera;

        private float Count = 0f;
        private float Max = 0.5f;
        private float Fov ;
        static private bool FocusState = false;

        private void Start()
        {
            Fov = PlayerCamera.fieldOfView;
        }

        void Update()
        {
            Fov = UI.SettingPanel.FovController.InformFovValue();

            PlayerCamera.fieldOfView = Fov;

            if (TimeManager.PreParationTime.InformPreparationState() == true)
            {
                return;
            }

            if (TimeManager.MainPhaze.InformMainphaze() != true)
            {
                return;
            }

            if(Shoot.InformReloadState() == true)
            {
                FocusState = false;
                return;
            }

            if(Input.GetKey(KeyCode.Mouse1))
            {
                Count += Time.deltaTime * 4;
                if (Count >= Max)
                {
                    Count = Max;
                    FocusState = true;
                }
            }
            else if(Count != 0)
            { 
                Count -= Time.deltaTime * 4;
                if(Count <= 0)
                {
                    Count = 0f;
                    FocusState = false;
                }
            }


            PlayerCamera.fieldOfView = Fov / ( Count + 1.0f);
        }

        static public bool InformForcusState()
        {
            return FocusState;
        }
    }
}
