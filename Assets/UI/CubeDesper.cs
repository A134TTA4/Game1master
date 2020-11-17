using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeDesper : MonoBehaviour
{
    [SerializeField]
    private Text CubeText;

    private int cube;

    void Update()
    {
        cube = Cube.CubeSet.CubeLeftInformer();
        if (cube < 0)
        {
            cube = 0;
        }
        CubeText.text ="CREATIVE\nCUBE" + cube + "/4";
    }
}
