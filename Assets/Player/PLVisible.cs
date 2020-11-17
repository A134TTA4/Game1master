using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLVisible : MonoBehaviour
{
    [SerializeField]
    Material myPlayer1;

    [SerializeField]
    private int PN ;
    private bool visible = true;

    private Color DefaultColor;
    private void Start()
    {
        DefaultColor = myPlayer1.color;
        myPlayer1.SetFloat("_Mode", 1f);
        myPlayer1.SetOverrideTag("RenderType", "TransparentCutout");
        myPlayer1.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        myPlayer1.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        myPlayer1.SetInt("_ZWrite", 1);
        myPlayer1.EnableKeyword("_ALPHATEST_ON");
        myPlayer1.DisableKeyword("_ALPHABLEND_ON");
        myPlayer1.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        myPlayer1.renderQueue = 2450;

    }
    void Update()
    {
        if(PhotonScriptor.ConnectingScript.informStartGame())
        {
            return;
        }
        if (PhotonScriptor.ConnectingScript.informPlayerID() != PN)
        {
            return;
        }
        myPlayer1.color = new Color(1, 1, 1, 0);
    }

    private void OnApplicationQuit()
    {
        myPlayer1.color = DefaultColor;
    }
}
