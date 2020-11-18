using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;
public class BulletGiveDamage : MonoBehaviour
{
    private float Givedamage = 14;
    private float lifetime = 1.0f;
    private float now;

    void Update()
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
        if ((component = Player.GetComponent<PlayerHp>()) != null)
        {
            PlayerHp.PlayerGetDamage(Givedamage);
        }
        GameObject Player2 = other.gameObject;
        PlayerHP2 component2;
        if ((component2 = Player2.GetComponent<PlayerHP2>()) != null)
        {
            PlayerHP2.PlayerGetDamage(Givedamage);
        }
        if (!other.CompareTag("Shield"))
        {
            Destroy(this.gameObject);
        }

    }
}
