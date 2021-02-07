using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinorLose : MonoBehaviour
    {
        static private int BluePoint = 0;
        static private int RedPoint = 0;
        [SerializeField]
        Text BluePointText;
        [SerializeField]
        Text RedPointText;


        void Update()
        {
            if (BluePoint == 2)
            {
                BluePointText.text = "MATCH POINT";
            }
            else
            {
                BluePointText.text = "" + BluePoint;
            }
            if (RedPoint == 2)
            {
                RedPointText.text = "MATCH POINT";
            }
            else
            {
                RedPointText.text = "" + RedPoint;
            }
        }

        static public int InformBluePoint()
        {
            return BluePoint;
        }

        static public int InformRedPoint()
        {
            return RedPoint;
        }

        static public void IncrementRed()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 2)
            {
                UI.UIEffect.RoundWinEffect.WinEffectOnM();
            }
            else
            {
                UI.UIEffect.RoundLostEffect.LoseEffectOnM();
            }
            RedPoint++;
            PlayerPrefs.SetFloat("RedPoint", RedPoint);
            PlayerPrefs.SetFloat("BluePoint", BluePoint);
        }
        static public void IncrementBlue()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
            {
                UI.UIEffect.RoundWinEffect.WinEffectOnM();
            }
            else
            {
                UI.UIEffect.RoundLostEffect.LoseEffectOnM();
            }
            BluePoint++;
            PlayerPrefs.SetFloat("RedPoint", RedPoint);
            PlayerPrefs.SetFloat("BluePoint", BluePoint);
        }

        static public void ResetRed()
        {
            RedPoint = 0;
        }

        static public void ResetBlue()
        {
            BluePoint = 0;
        }
    }
}
