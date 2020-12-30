using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager;
using Photon.Pun;

public class Shoot : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int PN;
    [SerializeField]
    GameObject PlayerCamera;
    [SerializeField]
    GameObject Mazzle;
    
    [SerializeField]
    Transform Player1;
    [SerializeField]
    Transform Player2;

    private float cooltime = 0.05f;
    private float cooltimeCount = 0f;
    private float bulletSpeed = 100f;
    static private bool shootable = true;
    private bool coolCountStart = false;
    static private int magazineMax = 25;
    static private int magazine = 25;
    static private bool reloadBool = false;
    private float reloadtime = 3.0f;//銃のリロードモーションは個別で設定し直す
    static private float reloadingtime = 0.0f;

    static public bool shot = false;

    private bool bulletPlusBool = false;

    private float delayCount = 0f;

    private int wannaToShoot = 0;

    private bool shooting = false;
    private void Start()
    {
        cooltime = 0.05f;
        cooltimeCount = 0f;
        bulletSpeed = 100f;
        shootable = true;
        coolCountStart = false;
        magazineMax = 25;
        magazine = 25;
        reloadBool = false;
        reloadtime = 3.0f;//銃のリロードモーションは個別で設定し直す
        reloadingtime = 0.0f;
    }
    void Update()
    {
        if(PN != PhotonScriptor.ConnectingScript.informPlayerID())
        {
            return;
        }

        if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState())
        {
            return;
        }

        if(IntercalTimeManager.InformIntervalState() == true)
        {
            return;
        }

        if(UI.SettingPanel.SettingPanelController.InformPanelState() == true)
        {
            return;
        }

        if(PreParationTime.InformPreparationState() == true)
        {
            magazine = magazineMax;
        }

        if(Player.BluePrint.DrawBluePrint.InformPlayerState() == 1)
        {
            bulletPlusBool = true;
        }
        else
        {
            bulletPlusBool = false;
        }

        shot = false;

        if(MainPhaze.InformMainphaze() == false)
        {
            return;
        }
        if(PreParationTime.InformPreparationState() == true)
        {
            return;
        }

        if(AnimationConrollScripts.PLMoveAnimeControl.InformSmoking() == true)
        {
            return;
        }

        

        ReloadCheck();
        if(magazine < magazineMax)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                reloadBool = true;
            }
        }

        if(ShootableM() == true)
        {
            ShootM();
        }
        
    }
    [PunRPC]
    void ShootM()
    {
        if(PN != PhotonScriptor.ConnectingScript.informPlayerID())
        {
            return;
        }
        if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
        {
            if (Input.GetKey(KeyCode.Mouse0) || wannaToShoot > 0)
            {
                coolCountStart = true;
                delayCount += Time.deltaTime;
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    wannaToShoot++;
                    Debug.Log(wannaToShoot);
                }
                if (delayCount >= 0.032f|| (wannaToShoot != 0 && shooting == true))
                {
                    shootable = false;
                    
                    magazine--;
                    wannaToShoot--;
                    delayCount = 0f;
                    shooting = true;
                    if (bulletPlusBool == true)
                    {
                        GameObject bulletSpawn = PhotonNetwork.Instantiate("BulletPlus", Mazzle.transform.position + PlayerCamera.transform.forward, Quaternion.Euler(this.transform.localEulerAngles.x, Player1.localEulerAngles.y, Player1.localEulerAngles.z));
                        Rigidbody BulletRigid = bulletSpawn.GetComponent<Rigidbody>();
                        BulletRigid.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                    }
                    else
                    {
                        GameObject bulletSpawn = PhotonNetwork.Instantiate("Bullet", Mazzle.transform.position + PlayerCamera.transform.forward, Quaternion.Euler(this.transform.localEulerAngles.x, Player1.localEulerAngles.y, Player1.localEulerAngles.z));
                        Rigidbody BulletRigid = bulletSpawn.GetComponent<Rigidbody>();
                        BulletRigid.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                    }
                    shot = true;
                }
                
            }
            else if(wannaToShoot == 0)
            {
                delayCount = 0f;
                shooting = false;
            }
            
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0) || wannaToShoot > 0)
            {
                delayCount += Time.deltaTime;
                coolCountStart = true;
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    wannaToShoot++;
                }
                if (delayCount >= 0.032f || (wannaToShoot != 0 && shooting == true))
                {
                    shootable = false;
                    
                    magazine--;
                    wannaToShoot--;
                    delayCount = 0f;
                    shooting = true;
                    if (bulletPlusBool == true)
                    {
                        GameObject bulletSpawn = PhotonNetwork.Instantiate("BulletPlus", Mazzle.transform.position + PlayerCamera.transform.forward, Quaternion.Euler(this.transform.localEulerAngles.x, Player2.localEulerAngles.y, Player2.localEulerAngles.z));
                        Rigidbody BulletRigid = bulletSpawn.GetComponent<Rigidbody>();
                        BulletRigid.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                    }
                    else
                    {
                        GameObject bulletSpawn = PhotonNetwork.Instantiate("Bullet", Mazzle.transform.position + PlayerCamera.transform.forward, Quaternion.Euler(this.transform.localEulerAngles.x, Player2.localEulerAngles.y, Player2.localEulerAngles.z));
                        Rigidbody BulletRigid = bulletSpawn.GetComponent<Rigidbody>();
                        BulletRigid.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                    }
                    shot = true;
                }
                
            }
            else if(wannaToShoot == 0)
            {
                delayCount = 0f;
                shooting = false;
            }
            
        }
    }

    bool ShootableM()
    {
        if (CoolTimeCountM() == false)
        {
            return false;
        }
        if(MagagineCheckM() == false)
        {
            return false;
        }
        if(ReloadCheck() == false)
        {
            return false;
        }
        shootable = true;
        return true;
    }

    bool CoolTimeCountM()
    {
        if(coolCountStart == false)
        {
            return true;
        }
        if(coolCountStart == true)
        {
            cooltimeCount += Time.deltaTime;
        }
        if(cooltimeCount >= cooltime)
        {
            coolCountStart = false;
            cooltimeCount = 0;
            return true;
        }
        return false;
        
    }

    bool MagagineCheckM()
    {
        if(magazine <= 0)
        {
            return false;
        }
        return true;
    }

    bool ReloadCheck()
    {
        if(reloadBool == false)
        {
            return true;
        }
        if (reloadBool == true)
        {
            reloadingtime += Time.deltaTime;
            wannaToShoot = 0;
            delayCount = 0f;
        }
        if(reloadingtime >= reloadtime)
        {
            magazine = magazineMax;
            reloadingtime = 0f;
            reloadBool = false;
            return true;
        }
        return false;
    }

    public bool InformShootable()
    {
        return shootable;
    }

    static public int InformMagazineLeft()
    {
        return magazine;
    }
    
    static public int InformMagazineMax()
    {
        return magazineMax;
    }

    static public bool InformReloadState()
    {
        return reloadBool;
    }

    static public float InformReloadingTime()
    {
        return reloadingtime;
    }
}
