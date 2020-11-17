using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecoil : MonoBehaviour
{

    static private float recoilSpeed = 0.1f;
    static private float maxRecoil_x = 20;
    static private float recoil = 0.0f;


    static public Quaternion RecoilM(Transform Camera)
    {
        if (recoil > 0)
        {
            recoil -= Time.deltaTime;
            var maxRecoil = new Quaternion(Camera.rotation.x + maxRecoil_x, Camera.rotation.y, Camera.rotation.z, Camera.rotation.w);
            // Dampen towards the target rotation
            
            Quaternion nowQuart = Quaternion.Slerp(new Quaternion(Camera.rotation.x, 0f, 0f, Camera.rotation.w), maxRecoil, Time.deltaTime * recoilSpeed);
            return nowQuart;

        }   
        if(recoil < 0.001f)
        {
            recoil = 0f;
        }

        return new Quaternion(1000, 0, 0, 0);
        
    }

    static public void RecoilFIX(Transform Camera)
    {
        if (recoil <= 0)
        {
            recoil = 0;
            var minRecoil = Quaternion.Euler(0, 0, 0);
            Camera.rotation = Quaternion.Slerp(Camera.rotation, minRecoil, Time.deltaTime * recoilSpeed / 2);
        }
    }

    static public void PlusRecoil()
    {
        recoil += 0.01f;
    }
}
