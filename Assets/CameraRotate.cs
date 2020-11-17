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
        var x = mXAxiz.x + Y_Rotation + recoil;
    
        if (x >= -LimitXAxizAngle && x <= LimitXAxizAngle)
        { 
            mXAxiz.x = x;
            PlayerCamera.localEulerAngles = mXAxiz;
        }
    }

    

    static public void PlusRecoil()
    {
        recoil += 0.05f;//リコイルタイム
    }

    public Quaternion RecoilM(Transform Camera)
    {
        if (recoil > 0)
        {
            maxRecoil_x -= 30;//一回のリコイル大きさ
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
        }

        return new Quaternion(1000, 0, 0, 0);

    }

}
