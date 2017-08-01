using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class loadingScreen : MonoBehaviour {
	public string levelToLoad;

	public GameObject background;
	public GameObject text;
	public GameObject progressBar;
	public GameObject animation;
	private int loadProgress = 0;
	public bool startLoading = false;
	// Use this for initialization
	void Start () {
		background.SetActive (false);
		text.SetActive (false);
		progressBar.SetActive (false);
		animation.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (startLoading == true) {
			StartCoroutine (DisplayLoadingScreen (levelToLoad));
			startLoading = false;
		}
	}

	IEnumerator DisplayLoadingScreen(string level)
	{
		background.SetActive (true);
		text.SetActive (true);
		progressBar.SetActive (true);
		animation.SetActive (true);

		progressBar.transform.localScale = new Vector3 (loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
		AsyncOperation async = SceneManager.LoadSceneAsync(levelToLoad);
		while (!async.isDone) {
			loadProgress = (int)(async.progress * 100);
			Debug.Log (loadProgress);
			progressBar.transform.localScale = new Vector3 (loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
			yield return null;
		}
	}
}
