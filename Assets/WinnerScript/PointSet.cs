using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WinnerScript
{
    public class PointSet : MonoBehaviour
    {
        [SerializeField]
        Text YourPoint;
        [SerializeField]
        Text EnemyPoint;
        [SerializeField]
        Text RateInCrease;
        [SerializeField]
        Text NowRate;
        [SerializeField]
        Text RatePace;


        private int YourGetPoint = 0;
        private int YourRate = 0;

        void Start()
        {
            YourPoint.text = "YOUR POINT:" + PlayerPrefs.GetFloat("YourPoint");
            EnemyPoint.text = "ENEMY POINT:" + PlayerPrefs.GetFloat("EnemyPoint");
            YourRate = PlayerPrefs.GetInt("YourRate", YourRate);
            YourGetPoint = (int)(PlayerPrefs.GetFloat("YourPoint") - PlayerPrefs.GetFloat("EnemyPoint")) * 100;
            YourRate += YourGetPoint;
            if (YourRate < 0)
            {
                YourRate = 0;
            }
            PlayerPrefs.SetInt("YourRate", YourRate);
            RateInCrease.text = "YOUR RATE INCREASE +" + YourGetPoint;
            NowRate.text = "NOW RATE:" + YourRate;
             
            

            if(YourRate >= 0 && YourRate < 1000)
            {
                RatePace.text = "BRONZE";
                RatePace.color = new Color(132, 73, 73);
            }
            else if (YourRate > 1000 && YourRate < 2000)
            {
                RatePace.text = "SILVER";
                RatePace.color = Color.gray;
            }
            else if (YourRate > 2000 && YourRate < 3000)
            {
                RatePace.text = "GOLD";
                RatePace.color = new Color(255, 173, 0);
            }
            else if (YourRate > 3000 && YourRate < 4000)
            {
                RatePace.text = "PLATINUM";
                RatePace.color = new Color(0, 142, 97);
            }
            else if (YourRate > 5000 && YourRate < 6000)
            {
                RatePace.text = "DIAMOND";
                RatePace.color = new Color(4, 255, 253);
            }
            else if (YourRate > 7000 && YourRate < 20000)
            {
                RatePace.text = "PROFESSIONAL";
                RatePace.color = new Color(255, 0, 74);
            }
            else
            {
                RatePace.text = "MORPHEUS";
                RatePace.color = new Color(0, 73, 73);
            }



        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
