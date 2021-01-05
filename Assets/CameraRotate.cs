using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerCamera;
    [SerializeField]
    private float YSpeed = 40.0f;
    [SerializeField]
    private int PN;


    static private float recoilSpeed = 0.02f;
    static private float maxRecoil_x = 0;
    static private float recoil = 0.0f;

    private float LimitXAxizAngle = 90;
    static private Quaternion beforeQuart = new Quaternion();
    private float FixTime = 0.1f;
    private float count = 0f;
    private bool fixing = false;
    //首の縦の動きを反映させるためのvector3
    private Vector3 mXAxiz;
    private void Start()
    {
        mXAxiz = PlayerCamera.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(PN!=PhotonScriptor.ConnectingScript.informPlayerID())
        {
            return;
        }

        YSpeed = 40.0f * UI.SettingPanel.MouseSenseController.InformSense();//あんま良くない

        if (Shoot.shot == true)
        {
            if(recoil == 0)
            {
                beforeQuart = PlayerCamera.transform.rotation;
                fixing = true;
                Debug.Log("quart get");
            }
            PlusRecoil();
        }
        Quaternion nowQuart = RecoilM(PlayerCamera.transform);
        if (nowQuart.x == 1000f )
        { 
           nowQuart.x  = 0f;
        }
        float x = nowQuart.x;

        CameraRotateM(x);
    }

    void CameraRotateM(float recoil)
    {
        
        float Y_Rotation = -1 * Input.GetAxis("Mouse Y") * YSpeed * Time.deltaTime;
        var x = mXAxiz.x + Y_Rotation + recoil * Time.deltaTime * 70f;
    
        if (x >= -LimitXAxizAngle && x <= LimitXAxizAngle)
        { 
            mXAxiz.x = x;
            PlayerCamera.localEulerAngles = mXAxiz;
        }
    }

    

    private void PlusRecoil()
    {
        if (Player.WeaponSwap.InformWeapon() == false)
        {
            recoil += 0.05f;//リコイルタイム
        }
        else
        {
            recoil += 0.1f;
        }
    }

    private Quaternion RecoilM(Transform Camera)
    {
        if (recoil > 0)
        {
            if (Player.WeaponSwap.InformWeapon() == false)
            {
                maxRecoil_x -= 30;//一回のリコイル大きさ
                FixTime += 0.01f;
            }
            else
            {
                maxRecoil_x -= 150;
            }
            recoil -= Time.deltaTime;
            Quaternion CameraQuart = new Quaternion(0, 0, 0, 0f);
            Quaternion maxRecoil = new Quaternion( maxRecoil_x, 0, 0, 0);
            Quaternion nowQuart = Quaternion.Slerp(CameraQuart, maxRecoil, Time.deltaTime * recoilSpeed);
            return nowQuart;

        }
        if (recoil < 0.001f)
        {
            recoil = 0f;
            maxRecoil_x = 0;
            if (fixing == true)
            {
                Debug.Log("Fixing");
                count += Time.deltaTime;
                Quaternion CameraQuart = new Quaternion(0, 0, 0, 0f);
                Quaternion maxRecoil = new Quaternion(2.3f, 0, 0, 0);
                if(Player.WeaponSwap.InformWeapon() == false)
                {
                    maxRecoil = new Quaternion(PlayerCamera.rotation.x - beforeQuart.x, 0, 0, 0);
                }
                Quaternion nowQuart = Quaternion.Slerp(CameraQuart, maxRecoil, Time.deltaTime);
                if(count > FixTime)
                {
                    count = 0;
                    fixing = false;
                }
                return nowQuart;
            }
            
        }
        

        return new Quaternion(1000, 0, 0, 0);

    }

}
