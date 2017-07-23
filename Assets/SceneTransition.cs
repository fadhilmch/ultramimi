using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {
    public string scene;
    private Fading fading;
    public KeyCode key;

    // Use this for initialization
    void Start()
    {
        fading = GetComponent<Fading>();

        // Use this for initialization
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(key))
        {
            float fadeTime = fading.BeginFade(1);
            //yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene(scene);
        }
    }
}
