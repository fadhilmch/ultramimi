using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class serialchange : MonoBehaviour {

    public string scene;
    private Fading fading;

    // Use this for initialization
    void Start () {
        fading = GetComponent<Fading>();
        float fadeTime = fading.BeginFade(1);
        //yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(scene);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
