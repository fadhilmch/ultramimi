using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace hideandseek
{
    public class CountDownManager : MonoBehaviour
    {

        public bool isCountDown = false;
        public float maxTime;
        private string teks;

        void Update()
        {
            if (isCountDown)
            {
                float timerInFloat = maxTime -= Time.deltaTime;
                int timerInInteger = (int)timerInFloat;
                teks = timerInInteger.ToString();
                if (timerInInteger == 0)
                {
                    teks = "GO!";
                }
                if (timerInInteger < 0)
                {
                    timerInInteger = 0;
                    isCountDown = false;
                    gameObject.SetActive(false);
                }
                GetComponent<Text>().text = teks;
            }
        }
    }
}