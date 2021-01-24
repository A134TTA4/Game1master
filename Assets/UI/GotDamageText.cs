using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class GotDamageText : MonoBehaviour
    {
        [SerializeField]
        GameObject NormalText;
        [SerializeField]
        GameObject ErrorText;
        void Start()
        {
            NormalText.SetActive(true);
            ErrorText.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Player.PlayerHP2.InformPlayerGetDamage() == true)
            {
                ErrorText.SetActive(true);
                NormalText.SetActive(false);
            }
            else
            {
                ErrorText.SetActive(false);
                NormalText.SetActive(true);
            }
        }
    }
}
