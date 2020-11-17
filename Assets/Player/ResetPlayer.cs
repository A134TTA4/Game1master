using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ResetPlayer : MonoBehaviour
    {
        [SerializeField]
        private GameObject PlayerObj;
        [SerializeField]
        private GameObject PlayerObj2;

        static private bool playerReactive = false;
        private void Update()
        {
            if(playerReactive == true)
            {
                Debug.Log("reset Player");
                PlayerObj.SetActive(true);
                PlayerObj2.SetActive(true);
                playerReactive = false;
            }
        }

        static public void ReactiveReset()
        {
            playerReactive = true;
        }
    }
}
