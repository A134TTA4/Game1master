using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    namespace BluePrint
    {
        public class CameraControll : MonoBehaviour
        {
            [SerializeField]
            private GameObject Position1;
            [SerializeField]
            private GameObject Position2;

            private Quaternion Quaternion1 = new Quaternion(90f,135f,0f,0f);
            private Quaternion Quaternion2 = new Quaternion(90f,45f,0f,0f);

            void Update()
            {
                if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true)
                {
                    if(PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                    {
                        this.gameObject.transform.position = Position1.transform.position;
                        this.gameObject.transform.rotation = Position1.transform.rotation;
                    }
                    else
                    {
                        this.gameObject.transform.position = Position2.transform.position;
                        this.gameObject.transform.rotation = Position2.transform.rotation;
                    } 
                }
            }
        }
    }
}
