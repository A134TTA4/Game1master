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
    private float cooltimeS = 0.2f;
    private float cooltimeCount = 0f;
    private float bulletSpeed = 100f;
    static private bool shootable = true;
    private bool coolCountStart = false;
    static private int magazineMax = 25;
    static private int magazine = 25;
    static private int magazineMaxs = 10;
    static private int magazines = 10;

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
        bulletSpeed = 100f;
        shootable = true;
        coolCountStart = false;
        magazineMax = 25;
        magazine = 25;
        magazines = 10;
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
            magazines = magazineMaxs;
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
        if(magazine < magazineMax && Player.WeaponSwap.InformWeapon() == false)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                reloadBool = true;
            }
        }
        if (magazines < magazineMaxs && Player.WeaponSwap.InformWeapon() == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloadBool = true;
            }
        }

        if(Player.WeaponSwap.InformSwap() == true)
        {
            reloadBool = false;
        }

        if (ShootableM() == true)
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
        if (Player.WeaponSwap.InformWeapon() == false)
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
            {
                if (Input.GetKey(KeyCode.Mouse0) || wannaToShoot > 0)
                {
                    coolCountStart = true;
                    delayCount += Time.deltaTime;
                    //Debug.LogError(Time.deltaTime);
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        wannaToShoot++;
                        //Debug.Log(wannaToShoot);
                    }
                    if (delayCount >= 0.00325f || (wannaToShoot != 0 && shooting == true))
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
                else if (wannaToShoot == 0)
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
                    if (delayCount >= 0.00325f || (wannaToShoot != 0 && shooting == true))
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
                else if (wannaToShoot == 0)
                {
                    delayCount = 0f;
                    shooting = false;
                }

            }
        }
        else
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || wannaToShoot > 0)
                {
                    coolCountStart = true;
                    delayCount += Time.deltaTime;
                    //Debug.LogError(Time.deltaTime);
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        wannaToShoot++;
                        Debug.Log(wannaToShoot);
                    }
                    if (delayCount >= 0.00325f || (wannaToShoot != 0 && shooting == true))
                    {
                        shootable = false;

                        magazines--;
                        wannaToShoot--;
                        delayCount = 0f;
                        shooting = true;
                        if (bulletPlusBool == true)
                        {
                            GameObject bulletSpawn = PhotonNetwork.Instantiate("BulletPlus", Mazzle.transform.position + PlayerCamera.transform.forward * 2, Quaternion.Euler(this.transform.localEulerAngles.x, Player1.localEulerAngles.y, Player1.localEulerAngles.z));
                            Rigidbody BulletRigid = bulletSpawn.GetComponent<Rigidbody>();
                            BulletRigid.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                        }
                        else
                        {
                            GameObject bulletSpawn = PhotonNetwork.Instantiate("Bullet", Mazzle.transform.position + PlayerCamera.transform.forward * 2, Quaternion.Euler(this.transform.localEulerAngles.x, Player1.localEulerAngles.y, Player1.localEulerAngles.z));
                            Rigidbody BulletRigid = bulletSpawn.GetComponent<Rigidbody>();
                            BulletRigid.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                        }
                        shot = true;
                    }

                }
                else if (wannaToShoot == 0)
                {
                    delayCount = 0f;
                    shooting = false;
                }

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || wannaToShoot > 0)
                {
                    delayCount += Time.deltaTime;
                    coolCountStart = true;
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        wannaToShoot++;
                    }
                    if (delayCount >= 0.00325f || (wannaToShoot != 0 && shooting == true))
                    {
                        shootable = false;

                        magazines--;
                        wannaToShoot--;
                        delayCount = 0f;
                        shooting = true;
                        if (bulletPlusBool == true)
                        {
                            GameObject bulletSpawn = PhotonNetwork.Instantiate("BulletPlusSideArm", Mazzle.transform.position + PlayerCamera.transform.forward*2, Quaternion.Euler(this.transform.localEulerAngles.x, Player2.localEulerAngles.y, Player2.localEulerAngles.z));
                            Rigidbody BulletRigid = bulletSpawn.GetComponent<Rigidbody>();
                            BulletRigid.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                        }
                        else
                        {
                            GameObject bulletSpawn = PhotonNetwork.Instantiate("BulletSideArm", Mazzle.transform.position + PlayerCamera.transform.forward * 2, Quaternion.Euler(this.transform.localEulerAngles.x, Player2.localEulerAngles.y, Player2.localEulerAngles.z));
                            Rigidbody BulletRigid = bulletSpawn.GetComponent<Rigidbody>();
                            BulletRigid.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                        }
                        shot = true;
                    }

                }
                else if (wannaToShoot == 0)
                {
                    delayCount = 0f;
                    shooting = false;
                }

            }
        }
    }

    bool ShootableM()
    {
        if (CoolTimeCountM() == false && Player.WeaponSwap.InformWeapon() == false)
        {
            return false;
        }
        if (CoolTimeCountMs() == false && Player.WeaponSwap.InformWeapon() == true)
        {
            return false;
        }
        if (MagagineCheckM() == false)
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

    bool CoolTimeCountMs()
    {
        if (coolCountStart == false)
        {
            return true;
        }
        if (coolCountStart == true)
        {
            cooltimeCount += Time.deltaTime;
        }
        if (cooltimeCount >= cooltimeS)
        {
            coolCountStart = false;
            cooltimeCount = 0;
            return true;
        }
        return false;

    }

    bool MagagineCheckM()
    {
        if(magazine <= 0 && Player.WeaponSwap.InformWeapon() ==false)
        {
            return false;
        }
        if(magazines <= 0 && Player.WeaponSwap.InformWeapon() == true)
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
            if (Player.WeaponSwap.InformWeapon() == false)
            {
                magazine = magazineMax;
            }
            else
            {
                magazines = magazineMaxs;
            }
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
    static public int InformMagazineLefts()
    {
        return magazines;
    }
    
    static public int InformMagazineMax()
    {
        return magazineMax;
    }
    static public int InformMagazineMaxs()
    {
        return magazineMaxs;
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
