using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace UI
{
    public class ReturnToMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject ReturnButton;
        
        void Update()
        {
            if (TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == true)
            {
                ReturnButton.SetActive(false);
            }
        }

        public void ReturnM()
        {
            SceneManager.LoadScene(0);
        }
    }
}
