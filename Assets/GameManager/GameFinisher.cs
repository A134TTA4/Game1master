using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameFinisher : MonoBehaviour
    {
        static private bool GameFinish = false;

        private void Start()
        {
            GameFinish = false;
        }
        void Update()
        {
            if(GameFinish == true)
            {
                Debug.Log("GameIsFinished");
                Finish();
            }
        }

        static public void GameFinishSet()
        {
            GameFinish = true;
        }


        private void Finish()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
            {
                if (UI.WinorLose.InformBluePoint() >= 3)
                {
                    PlayerPrefs.SetFloat("YourPoint", PlayerPrefs.GetFloat("BluePoint"));
                    PlayerPrefs.SetFloat("EnemyPoint", PlayerPrefs.GetFloat("RedPoint"));
                    PlayerPrefs.Save();
                    UI.WinorLose.ResetRed();
                    UI.WinorLose.ResetBlue();
                    PhotonScriptor.ConnectingScript.LeaveGame();
                    SceneManager.LoadScene("WinnersScene");

                }
                if (UI.WinorLose.InformRedPoint() >= 3)
                {
                    PlayerPrefs.SetFloat("YourPoint", PlayerPrefs.GetFloat("BluePoint"));
                    PlayerPrefs.SetFloat("EnemyPoint", PlayerPrefs.GetFloat("RedPoint"));
                    PlayerPrefs.Save();
                    UI.WinorLose.ResetRed();
                    UI.WinorLose.ResetBlue();
                    PhotonScriptor.ConnectingScript.LeaveGame();
                    SceneManager.LoadScene("LosersScene");
                }
            }
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 2)
            {
                if (UI.WinorLose.InformBluePoint() >= 3)
                {
                    PlayerPrefs.SetFloat("YourPoint", PlayerPrefs.GetFloat("RedPoint"));
                    PlayerPrefs.SetFloat("EnemyPoint", PlayerPrefs.GetFloat("BluePoint"));
                    PlayerPrefs.Save();
                    UI.WinorLose.ResetRed();
                    UI.WinorLose.ResetBlue();
                    PhotonScriptor.ConnectingScript.LeaveGame();
                    SceneManager.LoadScene("LosersScene");
                }
                if (UI.WinorLose.InformRedPoint() >= 3)
                {
                    PlayerPrefs.SetFloat("YourPoint", PlayerPrefs.GetFloat("RedPoint"));
                    PlayerPrefs.SetFloat("EnemyPoint", PlayerPrefs.GetFloat("BluePoint"));
                    PlayerPrefs.Save();
                    UI.WinorLose.ResetRed();
                    UI.WinorLose.ResetBlue();
                    PhotonScriptor.ConnectingScript.LeaveGame();
                    SceneManager.LoadScene("WinnersScene");
                }
            }
        }
    }
}