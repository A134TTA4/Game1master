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

    private float notshotcount = 0;
    private bool Getkeydown = false;

    private float LimitXAxizAngle = 90;
    static private Quaternion beforeQuart = new Quaternion();
    private float FixTime = 0f;
    private bool fixing = false;
    private Vector3 mXAxiz;
    private void Start()
    {
        mXAxiz = PlayerCamera.localEulerAngles;
    }


    void Update()
    {
        if(PN!=PhotonScriptor.ConnectingScript.informPlayerID())
        {
            return;
        }

        YSpeed = 40.0f * UI.SettingPanel.MouseSenseController.InformSense();//あんま良くない

        if (Shoot.shot == true)
        {
            if(recoil == 0 && fixing == false)
            {
                beforeQuart = PlayerCamera.transform.rotation;
            }
            PlusRecoil();
        }
        //Debug.Log(FixTime);
        //Debug.Log(fixing);
        if (Input.GetKey(KeyCode.Mouse0) && Shoot.shot == true)
        {
            Getkeydown = true;
        }
        else if(Shoot.shot == true)
        {
            Getkeydown = false;
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
            //Debug.Log(beforeQuart.x);
            if (fixing == false && Y_Rotation < 0)
            {
                beforeQuart.x += Y_Rotation;
            }
            //Debug.Log(beforeQuart.x);
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
            recoil += 0.08f;
        }
    }

    private Quaternion RecoilM(Transform Camera)
    {
        if (recoil > 0)
        {
            if (Player.WeaponSwap.InformWeapon() == false)
            {
                maxRecoil_x =  -50 * Random.Range(0.8f,1.3f);//一回のリコイル大きさ
                FixTime += 0.03f;
            }
            else
            {
                maxRecoil_x =  -120;
                FixTime += 0.07f;
            }
            recoil -= Time.deltaTime;
            if(/*recoil < 0 &&*/ !Input.GetKey(KeyCode.Mouse0))
            {
                Debug.Log("infix");
                fixing = true; 
            }
            if(Shoot.InformMagazineLeft() == 0 )
            {
                fixing = true;
            }
            Quaternion CameraQuart = new Quaternion(0, 0, 0, 0f);
            Quaternion maxRecoil = new Quaternion( maxRecoil_x, 0, 0, 0);
            Quaternion nowQuart = Quaternion.Slerp(CameraQuart, maxRecoil, Time.deltaTime * recoilSpeed);
            return nowQuart;

        }
        else if(fixing == true)
        {
            recoil = 0f;
            maxRecoil_x = 0;
            if (fixing == true && PlayerCamera.transform.rotation.x - beforeQuart.x < -0.000001f)
            {
                FixTime -= Time.deltaTime;
                Debug.Log("Fixing");
                Quaternion CameraQuart = new Quaternion(0, 0, 0, 0f);
                Quaternion fixRecoil = new Quaternion(2.3f, 0, 0, 0);
                if(Player.WeaponSwap.InformWeapon() == false)
                {
                    fixRecoil = new Quaternion(4.3f, 0, 0, 0);
                }
                Quaternion nowQuart = Quaternion.Slerp(CameraQuart, fixRecoil, Time.deltaTime);
                if(0 > FixTime)
                {
                    FixTime = 0f;
                    fixing = false;
                }
                return nowQuart;
            }
            else //if(fixing == true && Getkeydown == false)
            {
                Debug.Log("fix end");
                Debug.Log(PlayerCamera.transform.rotation.x - beforeQuart.x);
                FixTime = 0f;
                fixing = false;
            }
        }
        return new Quaternion(1000, 0, 0, 0);
    }

}
