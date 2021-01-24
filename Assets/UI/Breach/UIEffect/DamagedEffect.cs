using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DamagedEffect : MonoBehaviour
    {
        [SerializeField]
        GameObject Effect;
        [SerializeField]
        GameObject StartTextEffect;
        [SerializeField]
        GameObject VSTextEffect;
        void Start()
        {
            Effect.SetActive(false);
        }

        void Update()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() ==1)
            {
                if(Player.PlayerHp.InformHitStop() == true)
                {
                    Effect.SetActive(true);
                    StartTextEffect.SetActive(true);
                    VSTextEffect.SetActive(true);
                    
                }
                else
                {
                    Effect.SetActive(false);
                    StartTextEffect.SetActive(false);
                    VSTextEffect.SetActive(false);
                }
            }
            else
            {
                if (Player.PlayerHP2.InformHitStop() == true)
                {
                    Effect.SetActive(true);
                    StartTextEffect.SetActive(true);
                    VSTextEffect.SetActive(true);
                }
                else
                {
                    Effect.SetActive(false);
                    StartTextEffect.SetActive(false);
                    VSTextEffect.SetActive(false);
                }
            }
        }
    }
}
