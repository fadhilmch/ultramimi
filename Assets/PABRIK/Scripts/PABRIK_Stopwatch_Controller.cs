using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PABRIK_Stopwatch_Controller : MonoBehaviour
{

    [SerializeField]
    Text StopwatchText;

    [SerializeField]
    Image BGImage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentValue">Ini nilai berapa yang mau dilihatkan</param>
    /// <param name="maxValue">Nilai maksimum si stopwatch</param>
	public void SetValue(float currentValue, int maxValue, float floatValue)
    {

        if (currentValue < 10)
        {
            StopwatchText.text = "0" + currentValue.ToString();

        }
        else
        {
            StopwatchText.text = currentValue.ToString();
        }

        BGImage.fillAmount = (float)floatValue;
    }
}
