using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LobbyScript
{
    public class ClickReady : MonoBehaviour
    {
        public void ClickedReady()
        {
            SceneManager.LoadScene(1);
        }
    }
}
