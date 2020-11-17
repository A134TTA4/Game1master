using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PLCrosshairState : MonoBehaviour
    {
        [SerializeField]
        private GameObject CrossHair;
        // Start is called before the first frame update
        void Start()
        {
            CrossHair.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(PLCameraFocus.InformForcusState() == true)
            {
                CrossHair.SetActive(true);
            }
            else
            {
                CrossHair.SetActive(false);
            }
        }
    }
}
