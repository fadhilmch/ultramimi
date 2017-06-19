using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoundTimer : MonoBehaviour {
	public Text countDown;
	public float timeLeft = 30f;
	// Use this for initialization
	void Start () {
		countDown.text = timeLeft.ToString("0");	
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		countDown.text = timeLeft.ToString ("0");
		if (timeLeft < 0)
			countDown.text = "Finished!";
	}
}
