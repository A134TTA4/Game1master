using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    [SerializeField]
    Transform PlayerTrans1;
    [SerializeField]
    Transform PlayerTrans2;
    static private bool cameraSet = false;

    private void Start()
    {
        cameraSet = false;
    }

    void Update()
    {
        if(TimeManager.TimeCounter.InformStartGame() == true)
        {
            if(cameraSet == false)
            {
                cameraSet = true;
                if(PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                {
                    Debug.Log("camera set");
                    this.transform.position = PlayerTrans1.position;
                    this.transform.rotation = PlayerTrans1.rotation;
                    this.transform.parent = PlayerTrans1;
                    GameState.GameStateReset.SetReset();
                }
                if (PhotonScriptor.ConnectingScript.informPlayerID() == 2)
                {
                    this.transform.position = PlayerTrans2.position;
                    this.transform.rotation = PlayerTrans2.rotation;
                    this.transform.parent = PlayerTrans2;
                    GameState.GameStateReset.SetReset();
                }
            }
        }
    }
}
