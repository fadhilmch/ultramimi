using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensaverTimer : MonoBehaviour {
	public static float timer = 0f;
	private const float screenSaverTimer = 600f;
	private const string screenSaverScene = "ScreenSaver";
	private SceneTransition sceneTransition;
	public static void resetTimer()
	{
		timer = 0f;
	}

	// Use this for initialization
	void Start () {
		sceneTransition = GetComponent<SceneTransition> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
			resetTimer ();
		//Debug.Log ("timer " + timer);
		if (timer < screenSaverTimer) {
			timer += Time.deltaTime;
		} else {
			timer = 0f;
			sceneTransition.loadScene ();
			//load screensaver
		}
	}
}
