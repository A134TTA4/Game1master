using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
namespace Shield
{
    public class ShieldRotate : MonoBehaviour
    {

        float shieldTimer = 0;
        Vector3 axiz;
        Quaternion shieldRot;

        void Start()
        {
            shieldRot = this.transform.rotation;
            axiz = new Vector3(0.0f, 1.0f, 0.0f);
        }

        void Update()
        {
            shieldTimer += 1.0f * Time.deltaTime;//タイマー更新
            shieldRot = this.transform.rotation;
            Quaternion rot = Quaternion.AngleAxis(90.0f * Time.deltaTime / 10, axiz);
            this.transform.rotation = shieldRot * rot;

            //Debug.Log(shieldTimer);
        }
    }
}
