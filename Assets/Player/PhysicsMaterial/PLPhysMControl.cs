using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    namespace PhysicsMaterial {
        public class PLPhysMControl : MonoBehaviour
        {
            [SerializeField]
            private PhysicMaterial PLPhysM;
            [SerializeField]
            private int PN;


            private float MaxFliction = 1.0f;
            private float MinFliction = 0.1f;
            void Start()
            {
                PLPhysM.dynamicFriction = MaxFliction;
            }

            // Update is called once per frame
            void Update()
            {
                if(PhotonScriptor.ConnectingScript.informPlayerID() != PN)
                {
                    return;
                }
                if (PlayerClouch.InformClouch() == true)
                {
                    PLPhysM.dynamicFriction = MinFliction;
                }
                else
                {
                    PLPhysM.dynamicFriction = MaxFliction;
                }
        }
        }
    }
}
