using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChange : MonoBehaviour
{
    [SerializeField]
    private Collider PLCollider;
    [SerializeField]
    private Rigidbody PLRigid;
    [SerializeField]
    private int PNE;

    // Update is called once per frame
    void Update()
    {
        if(PNE == PhotonScriptor.ConnectingScript.informPlayerID())
        {
            PLCollider.isTrigger = true;
            PLRigid.useGravity = false;
        }
        else
        {
            PLCollider.isTrigger = false;
            PLRigid.useGravity = true;
        }
    }
}
