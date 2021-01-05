using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineDesp : MonoBehaviour
{
    [SerializeField]
    private Text magazineText;

    private int magazine;
    private int magazines;

    private int magazineMax;
    // Start is called before the first frame update
    void Start()
    {
        magazineMax = Shoot.InformMagazineMax();
    }

    // Update is called once per frame
    void Update()
    {
        magazine = Shoot.InformMagazineLeft();
        magazines = Shoot.InformMagazineLefts();
        if (magazine < 0)
        {
            magazine = 0;
        }
        if (Player.WeaponSwap.InformWeapon() == false)
        {
            magazineText.text = magazine + "";
        }
        else
        {
            magazineText.text = magazines + "";
        }
        if (Shoot.InformReloadState() == true)
        {
            magazineText.text = "#/#";
        }
    }
}
