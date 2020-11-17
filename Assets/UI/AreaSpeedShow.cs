using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class AreaSpeedShow : MonoBehaviour
{
    [SerializeField]
    private Text AreaSpeed;
    void Update()
    {
        if(Shield.shieldScale.InforSmallerRate() == 3.0f)
        {
            AreaSpeed.text = "AREA SPEED IS HIGH";
        }
        else if(Shield.shieldScale.InforSmallerRate() == 1.0f)
        {
            AreaSpeed.text = "AREA SPEED IS MIDIUM";
        }
        else
        {
            AreaSpeed.text = "AREA SPEED IS LOW";
        }
    }
}
