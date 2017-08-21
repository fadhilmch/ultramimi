using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class serialchange : MonoBehaviour {
	private bool firstTime = true;
    public string scene;
    private Fading fading;
	public GameObject loadingScreen;
    // Use this for initialization
    void Start () {
        fading = GetComponent<Fading>();
        float fadeTime = fading.BeginFade(1);
        //yield return new WaitForSeconds(fadeTime);

    }
	
	// Update is called once per frame
	void Update () {
		if (firstTime) {
			loadingScreen.GetComponent<loadingScreen> ().levelToLoad = scene;
			loadingScreen.GetComponent<loadingScreen> ().startLoading = true;
			firstTime = false;
		}
	}
}
