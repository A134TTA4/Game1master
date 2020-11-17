using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager;
public class GunManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Gun;
    [SerializeField]
    private GameObject Gun2;
    private bool Gunstate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PreParationTime.InformPreparationState() == true)
        {
            Gunstate = false;
            Gun.SetActive(false);
            Gun2.SetActive(false);
            return;
        }
        if (Gunstate == false)
        {
            Gunstate = true;
            Gun.SetActive(true);
            Gun2.SetActive(true);
        }
    }
}
