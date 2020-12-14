using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class BulletGiveDamage : MonoBehaviour
{
    [SerializeField]
    private float Givedamage = 14;
    private float lifetime = 5.0f;
    private float now = 0f;

    private void Start()
    {
        if(Player.BluePrint.DrawBluePrint.InformPlayerState() == 1)
        {
            Givedamage += 5;
            Debug.Log("Damage Up");
        }
    }

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
            if (PhotonScriptor.ConnectingScript.informPlayerID() != 1)
            {
                PlayerHp.PlayerGetDamage(Givedamage);
            }
        }
        GameObject Player2 = other.gameObject;
        PlayerHP2 component2;
        if ((component2 = Player2.GetComponent<PlayerHP2>()) != null)
        {
            if (PhotonScriptor.ConnectingScript.informPlayerID() != 2)
            {
                PlayerHP2.PlayerGetDamage(Givedamage);
            }
        }
        Destroy(this.gameObject); 

    }
}
