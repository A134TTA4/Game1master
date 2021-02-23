using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixHeight : MonoBehaviour
{
    [SerializeField]
    GameObject Player1;
    [SerializeField]
    GameObject Player2;

    void Update()
    {
        if(PhotonScriptor.ConnectingScript.informPlayerID() ==1)
        {
            if(Player2.transform.position.y < 0.89)
            {
                Player2.transform.position = new Vector3(Player2.transform.position.x, 0.89f, Player2.transform.position.z);
            }
        }
        else
        {
            if (Player1.transform.position.y < 0.9)
            {
                Player1.transform.position = new Vector3(Player1.transform.position.x, 0.9f, Player1.transform.position.z);
            }
        }
    }
}
