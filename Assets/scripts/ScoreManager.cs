using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public GameObject[] scores;

	public void SetFoundScore(int index){
		scores [index].SetActive (false);
	}
}
