using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ROundEndTextDesp : MonoBehaviour
{
    [SerializeField]
    Text RoundOver;

    private int RoundCount;
 
    void Update()
    {
        RoundCount = GameManager.GameRoundManager.InformRoundCounter();
        RoundOver.text = "ROUND" + RoundCount + "END";
    }
}
