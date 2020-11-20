using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    namespace BrakeWall
    {
        public class BrakeWallGiveDamage : MonoBehaviour
        {
            private float Givedamage = 30;
            private float lifetime = 0.5f;
            private float now = 0f;
            private bool Damaged = false;
            private void Update()
            {
                now += Time.deltaTime;
                if(lifetime <= now)
                {
                    Destroy(this.gameObject);
                }
            }

            private void OnTriggerEnter(Collider other)
            {
                GameObject Player = other.gameObject;
                PlayerHp component;
                if ((component = Player.GetComponent<PlayerHp>()) != null && !Damaged)
                {
                    PlayerHp.PlayerGetDamageforme(Givedamage);
                    Damaged = true;
                }
                GameObject Player2 = other.gameObject;
                PlayerHP2 component2;
                if ((component2 = Player2.GetComponent<PlayerHP2>()) != null && !Damaged)
                {
                    PlayerHP2.PlayerGetDamageforme(Givedamage);
                    Damaged = true;
                }
            }
        }
    }
}
