using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	public GameObject panelMimiWin;
	public GameObject panelLeoWin;
	public GameObject panelDraw;
	public GameObject loadingScreen;

	public void SetWin(int index){
		switch (index) {
		case 0:
			panelMimiWin.SetActive (true);
			break;
		case 1:
			panelLeoWin.SetActive (true);
			break;
		case 2:
			panelDraw.SetActive (true);
			break;
		}
		StartCoroutine (GoToMainMenu());
	}

	private IEnumerator GoToMainMenu(){

		yield return new WaitForSeconds (5);
		loadingScreen.GetComponent<loadingScreen>().levelToLoad = "GABUNG";
		loadingScreen.GetComponent<loadingScreen>().startLoading = true;;
	}
}
