using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour {
	float maxTime = 4.0f;
	void Update(){
		float timerInFloat = maxTime -= Time.deltaTime;
		int timerInInteger = (int)timerInFloat;
		if (timerInInteger < 0) {
			timerInInteger = 0;
		} 
		GetComponent<Text> ().text = timerInInteger.ToString();
	
	}
}
