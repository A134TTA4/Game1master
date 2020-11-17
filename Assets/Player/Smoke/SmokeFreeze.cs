using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    namespace Smoke
    {
        public class SmokeFreeze : MonoBehaviour
        {

            [SerializeField]
            private Rigidbody SmokeRigid;
            private float Count = 0f;
            private float LifeTime = 10f;
            void Start()
            {
                SmokeRigid.freezeRotation = true;
            }

            private void Update()
            {
                Count += Time.deltaTime;
                if(Count >= LifeTime)
                {
                    this.gameObject.SetActive(false);
                }
            }

        }
    }
}
