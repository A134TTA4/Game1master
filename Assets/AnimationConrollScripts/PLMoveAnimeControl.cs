using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationConrollScripts
{
    public class PLMoveAnimeControl : MonoBehaviour
    {
        [SerializeField]
        Animator PLanimator;
        [SerializeField]
        private int PN;

        private int Smoke = 2;
        private float countingSmoke = 0f;
        private float countingJump = 0f;
        private float max = 2f;
        static private bool SmokeOut = false;
        private bool Reloading = false;
        static private bool Jumping = false;

        private bool swapnow = false;
        void Update()
        {
            if(PN!=PhotonScriptor.ConnectingScript.informPlayerID())
            {
                return;
            }

            PLanimator.SetBool("A", Player.PlayerMove1.InformA());
            PLanimator.SetBool("D", Player.PlayerMove1.InfromD());

            PLanimator.SetBool("Walk", Player.PlayerMove1.InformWalking());

            PLanimator.SetBool("Clouch", Player.PlayerClouch.InformClouch());

            PLanimator.SetBool("ADS", Player.PLCameraFocus.InformForcusState());

            PLanimator.SetBool("isDash", Player.PlayerMove1.InformDash());
            
            
            
            if (Player.WeaponSwap.InformSwap() == false)
            {
                PLanimator.SetBool("Swap", false);
                swapnow = false;
            }
            if (Player.WeaponSwap.InformSwap() == true && swapnow == false)
            {
                swapnow = true;
                PLanimator.SetBool("Swap", true);
            }
            
            
            if(Player.WeaponSwap.InformWeapon() == false)
            {
                PLanimator.SetFloat("SideArm", 0);
            }
            else
            {
                PLanimator.SetFloat("SideArm", 1);
            }


            if (Jumping == true)
            {
                countingJump += Time.deltaTime;
                
                if (countingJump >= 1.2f)
                {
                    Jumping = false;
                    Player.PlayerJump.ReSetJumpBool();
                    //PLanimator.SetBool("Jump", false);
                    countingJump = 0f;
                }
            }
            if (Player.PlayerJump.InformJump() == true)
            {
                PLanimator.SetBool("Jump", true);
                Jumping = true;
            }

            if (countingJump >= 0.7f)
            {
                PLanimator.SetBool("Jump", false);
            }

            if (SmokeOut == true)
            {
                countingSmoke += Time.deltaTime;
                if (countingSmoke >= 1.0f)
                {
                    SmokeOut = false;
                    PLanimator.SetBool("Thorough", false);
                    countingSmoke = 0f;
                }
            }
            if(Smoke > Player.Smoke.UseSmoke.InformSmokeLeft())
            {
                PLanimator.SetBool("Thorough", true);
                SmokeOut = true;
            }

            PLanimator.SetBool("Reload", Shoot.InformReloadState());

           


            if (Player.PlayerMove1.InformWalking() == true)
            {
                PLanimator.SetFloat("ReloadBlend", 1f);
                if (Player.PlayerClouch.InformClouch() == true)
                {
                    PLanimator.SetFloat("ReloadBlend", 2f);
                }
            }
            else
            {
                PLanimator.SetFloat("ReloadBlend", 0f);
            }

            Smoke = Player.Smoke.UseSmoke.InformSmokeLeft();


        }

        static public bool InformSmoking()
        {
            return SmokeOut;
        }
        
        static public bool InformJumping()
        {
            return Jumping;
        }


    }
}
