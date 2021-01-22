using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CameraBoxController : MonoBehaviour
    {
        void Start()
        {
            if (Player.WeaponSwap.InformWeapon() == false)
            {
                this.transform.localPosition = new Vector3(0, 0.4f, 0f);
            }
            else
            {
                this.transform.localPosition = new Vector3(0, 0.34f, 0f);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (WeaponSwap.InformWeapon() == false)
            {
                if (WeaponSwap.InformSwap() == true)
                {
                    this.transform.localPosition = new Vector3(0, this.transform.localPosition.y - 0.06f * Time.deltaTime / 0.5f, 0);
                }
                else
                {
                    this.transform.localPosition = new Vector3(0, 0.4f, 0f);
                }
            }
            else
            {
                if (WeaponSwap.InformSwap() == true)
                {
                    this.transform.localPosition = new Vector3(0, this.transform.localPosition.y + 0.06f * Time.deltaTime / 0.5f, 0);
                }
                else
                {
                    this.transform.localPosition = new Vector3(0, 0.34f, 0f);
                }
            }
            
        }
    }
}
