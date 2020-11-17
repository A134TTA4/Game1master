using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeparateManager {
    public class SeparateSet : MonoBehaviour
    {
        [SerializeField]
        private GameObject Separator;
        void Start()
        {
            Separator.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (TimeManager.MainPhaze.InformMainphaze() == true)
            {
                Separator.SetActive(false);
            }
            else
            {
                Separator.SetActive(true);
            }
        }
    }
}