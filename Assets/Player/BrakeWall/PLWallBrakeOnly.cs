using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    namespace BrakeWall
    {
        public class PLWallBrakeOnly : MonoBehaviour
        {
            private void OnTriggerEnter(Collider other)
            {
                if (other.CompareTag("Cube"))
                {
                    Destroy(other.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
