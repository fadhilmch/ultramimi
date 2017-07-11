using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenSaverTimer : MonoBehaviour {
    public float counterMaxTimer = 300f;
    private float counter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("ScreenSaver");
        }
	}
}
