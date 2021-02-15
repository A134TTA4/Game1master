using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Player
{
    namespace BrakeWall
    {
        public class PLShootBrake : MonoBehaviour
        {
            [SerializeField]
            private int PN;
            [SerializeField]
            GameObject Mazzle;
            [SerializeField]
            GameObject PlayerCamera;

            [SerializeField]
            Transform Player;
            [SerializeField]
            Transform Player2;

            static private bool BrakeMode = false; 
            static private int magazine = 0;
            static private int magazineMax = 1;
            private float bulletSpeed = 40f;
            private bool ResetTime = false;
            static private bool magagineIncreased = false;
            void Start()
            {
                magagineIncreased = false;
                magazine = magazineMax;
                BrakeMode = false;
            }

            void Update()
            {
                if (PN != PhotonScriptor.ConnectingScript.informPlayerID())
                {
                    return;
                }

                if (TimeManager.PreParationTime.InformPreparationState() == true)
                {
                    magazine = magazineMax;
                    if(magagineIncreased == false && magazineMax == 2 && ResetTime == true)
                    {
                        magazineMax = 1;
                        magazine = magazineMax;
                    }
                    BrakeMode = false;
                    magagineIncreased = false;
                    ResetTime = false;
                }

                if(Shoot.InformReloadState() == true)
                {
                    return;
                }
                if(AnimationConrollScripts.PLMoveAnimeControl.InformSmoking() == true)
                {
                    return;
                }

                if (TimeManager.MainPhaze.InformMainphaze() == false)
                {
                    return;
                }
                else
                {
                    ResetTime = true;
                }


                if (TimeManager.PreParationTime.InformPreparationState() == true)
                {
                    return;
                }

                if(magazine == 0)
                {
                    return;
                }

                BrakeWallShootM();

            }

            void BrakeWallShootM()
            {
                if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                {
                    if (WeaponSwap.InformQ() == true && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        magazine--;
                        GameObject bulletSpawn = PhotonNetwork.Instantiate("BrakeWallBullet", Mazzle.transform.position + PlayerCamera.transform.forward, Quaternion.Euler(this.transform.localEulerAngles.x + 90f, Player.localEulerAngles.y, Player.localEulerAngles.z));
                        Rigidbody BulletRigid = bulletSpawn.GetComponent<Rigidbody>();
                        BulletRigid.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                        SoundEffects.WallBrakerShootSound.shotTrueM();
                    }
                }
                else
                {
                    if (WeaponSwap.InformQ() == true && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        magazine--;
                        GameObject bulletSpawn = PhotonNetwork.Instantiate("BrakeWallBullet", Mazzle.transform.position + PlayerCamera.transform.forward, Quaternion.Euler(this.transform.localEulerAngles.x + 90f, Player2.localEulerAngles.y, Player2.localEulerAngles.z));
                        Rigidbody BulletRigid = bulletSpawn.GetComponent<Rigidbody>();
                        BulletRigid.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);
                        SoundEffects.WallBrakerShootSound.shotTrueM();
                    }
                }
            }

            static public bool InformBrakeMode()
            {
                return BrakeMode;
            }

            static public int InformBrakeMagazineLeft()
            {
                return magazine;
            }

            static public void IncreaseBrakeWall()
            {
                magazineMax = 2;
                magagineIncreased = true;
            }
        }
    }
}