using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace Player
{
    public class PlayerBodyRotate : MonoBehaviour
    {
        [SerializeField]
        private int PN;
        [SerializeField]
        private GameObject Player;
        private Transform playerTrans;
        [SerializeField]
        private float rotateSpeedx = 40f;
        private float rotateAngle;
        void Start()
        {
            playerTrans = Player.transform;
        }

        void Update()
        {
            BodyRotateM();
        }

        void BodyRotateM()
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() != PN)
            {
                return;
            }
            rotateSpeedx = 40.0f * UI.SettingPanel.MouseSenseController.InformSense();//あんま良くない
            rotateAngle = Input.GetAxis("Mouse X") * rotateSpeedx * Time.deltaTime;
            playerTrans.Rotate(Vector3.up, rotateAngle);
        }
    }
}