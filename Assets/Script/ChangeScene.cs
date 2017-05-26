using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string scene;

    private Controller controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        if (controller.change == true)
            NewScene();
	}

    void NewScene()
    {
        SceneManager.LoadScene(scene);
    }
}
