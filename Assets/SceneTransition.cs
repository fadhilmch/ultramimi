using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {
    public  string scene;
    private Fading fading;
    public KeyCode key;
    private AudioSource source;
	public Interaction interaction;
    public AudioClip audio1;
    public AudioClip audio2;
	public GameObject loadingScreen;

    public float volume1 = 1;
    public float volume2 = 1;
    
	public void loadScene()
	{
		source.PlayOneShot(audio1, volume1);
		float fadeTime = fading.BeginFade(1);
		//yield return new WaitForSeconds(fadeTime);
		loadingScreen.GetComponent<loadingScreen>().levelToLoad = scene;
		loadingScreen.GetComponent<loadingScreen>().startLoading = true;
		source.PlayOneShot(audio2, volume2);
	}

    // Use this for initialization
    void Start()
    {
        fading = GetComponent<Fading>();
        source = GetComponent<AudioSource>();
        // Use this for initialization
    }


    // Update is called once per frame
    void Update () {
        /*if (SerialHandler.serial_is_open && SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
        {
            source.PlayOneShot(audio1,volume1);
            float fadeTime = fading.BeginFade(1);
            //yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene(scene);
            //source.PlayOneShot(audio2, volume2);
        }*/
		if (Input.GetKeyDown(key) || SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
        {
            source.PlayOneShot(audio1, volume1);
            float fadeTime = fading.BeginFade(1);
            //yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene(scene);
            source.PlayOneShot(audio2, volume2);

        }
    }
}
