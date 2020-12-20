using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DamagedEffect : MonoBehaviour
    {
        [SerializeField]
        GameObject Effect;
        void Start()
        {
            Effect.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() ==1)
            {
                if(Player.PlayerHp.InformHitStop() == true)
                {
                    Effect.SetActive(true);
                }
                else
                {
                    Effect.SetActive(false);
                }
            }
            else
            {
                if (Player.PlayerHP2.InformHitStop() == true)
                {
                    Effect.SetActive(true);
                }
                else
                {
                    Effect.SetActive(false);
                }
            }
        }
    }
}
