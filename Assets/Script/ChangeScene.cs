using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string scene;

    private Controller controller;

    private Fading fading;

	// Use this for initialization
	void Start () {
        controller = GetComponent<Controller>();
        fading = GetComponent<Fading>();
	}
	
	// Update is called once per frame
	void Update () {
        if (controller.change == true)
            //Reset();
            NewScene();
	}

    public void NewScene()
    {
        float fadeTime =fading.BeginFade(1);
        //yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(scene);
        
    }

    private void Reset()
    {
        controller.farm = false;
        controller.factory = false;
        controller.store = false;
        controller.rumah = false;
        controller.prolog = false;
    }
}
