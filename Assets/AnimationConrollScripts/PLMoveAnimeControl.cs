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
        void Update()
        {
            if(PN!=PhotonScriptor.ConnectingScript.informPlayerID())
            {
                return;
            }

            PLanimator.SetBool("Walk", Player.PlayerMove1.InformWalking());

            PLanimator.SetBool("Clouch", Player.PlayerClouch.InformClouch());

            PLanimator.SetBool("ADS", Player.PLCameraFocus.InformForcusState());

            PLanimator.SetBool("isDash", Player.PlayerMove1.InformDash());

            if (Jumping == true)
            {
                countingJump += Time.deltaTime;
                
                if (countingJump >= 1.5f)
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
            
            if (Shoot.InformReloadState() == true && Reloading == false)
            {
                PLanimator.SetBool("Reload", Shoot.InformReloadState());
                Reloading = true;
            }
            else if(Shoot.InformReloadState() == true && Reloading == true)
            {
                PLanimator.SetBool("Reload", false);
            }
            else if(Shoot.InformReloadState() == false && Reloading == true)
            {
                Reloading = false;
            }


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
