using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Player
{
    namespace BrakeWall
    {
        public class PLBrakeWall : MonoBehaviour
        {
            private void OnTriggerEnter(Collider other)
            {
                //Debug.Log("Got Trigger Enter");
                if (other.CompareTag("Cube"))
                {
                    PhotonNetwork.Instantiate("BreachEffect", this.transform.position, this.transform.rotation);
                    //Debug.Log("Destroy");
                    Destroy(other.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
