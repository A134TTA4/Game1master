using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAxisFreez : MonoBehaviour
    {
        [SerializeField]
        GameObject PlayerObject;
        Rigidbody PlayerRigid;
        void Start()
        {
            PlayerRigid = PlayerObject.GetComponent<Rigidbody>();
            PlayerRigid.freezeRotation = true;
        }


    }
}
