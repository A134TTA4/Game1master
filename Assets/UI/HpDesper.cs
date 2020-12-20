using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpDesper : MonoBehaviour
{
    [SerializeField]
    private Text hpText;
    private float hp;

    void Update()
    {
        
        if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
        {
            hp = PlayerHp.InformPlayerHP();
            if (hp < 0)
            {
                hp = 0;
            }
            hpText.text = "";
            int HpBar = (int)hp / 5;
            for (int n = 0; n < HpBar; n++)
            {
                hpText.text += "|";
            }
        }
        if (PhotonScriptor.ConnectingScript.informPlayerID() == 2)
        {
            hp = PlayerHP2.InformPlayerHP();
            if (hp < 0)
            {
                hp = 0;
            }
            hpText.text = "";
            int HpBar = (int)hp / 5;
            for (int n = 0; n < HpBar; n++)
            {
                hpText.text += "|";
            }
        }
    }

    
}
