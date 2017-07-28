using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string games_1_scene;
    public string games_2_scene;
    public Interaction interaction;
    private Fading fading;
    private Animator animator;
    private int state = 0;

    private float reactionTime = 1f;
    private float reactionTimer = 0f;
    private bool startReactionTimer = false;
	private	AsyncOperation	asyncOperation;

    bool reactionTimerCount()
    {
        if(startReactionTimer)
        {
            reactionTimer += Time.deltaTime;
            if (reactionTimer > reactionTime)
            {
                reactionTimer = 0;
                startReactionTimer = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    void TimerCount()
    {
        interaction.counter += Time.deltaTime;
        if (interaction.counter > interaction.timeOut)
        {
            interaction.counter = 0;
            interaction.value = false;
            interaction.value2 = false;
            animator.SetInteger("AnimState",0);
            state = 0;
        }
    }

    // Use this for initialization
    void Start () {
        fading = GetComponent<Fading>();
        animator = GetComponent<Animator>();
		//StartCoroutine (LoadSyncAsync("Games1"));
	}
	
	// Update is called once per frame
	void Update () {
        interaction.value = false;
        interaction.value2 = false;

        if (Input.GetKeyDown(interaction.keyCode) || SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
        {
            interaction.value = !interaction.value;
            Debug.Log("masuk 1");
        }
        if (Input.GetKeyDown(interaction.keyCode2) || SerialHandler.getSensorDown((int)interaction.sensorTrigger2))
        {
            interaction.value2 = !interaction.value2;
            Debug.Log("masuk 2");
        }

        if (state == 0)
        {
            if (interaction.value)
            {
                state = 1;
                animator.SetInteger("AnimState",1);
                interaction.value = false;
                interaction.value2 = false;
                startReactionTimer = true;
            }
        }
        else if (animator.GetInteger("AnimState") == 1)
        {
            if(reactionTimerCount())
            {
                if (interaction.value)
                {
                    NewScene(games_1_scene);
					//LoadGames1();
                    animator.SetInteger("AnimState", 0);
                    state = 0;
                }
                else if (interaction.value2)
                {
                    NewScene(games_2_scene);
                    animator.SetInteger("AnimState", 0);
                    state = 0;
                }

                TimerCount();
            }
            
        }
    }

    public void NewScene(string scene)
    {
        float fadeTime =fading.BeginFade(1);
        //yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(scene);
        
    }

	public	IEnumerator	LoadSyncAsync(string	nameScene){

		asyncOperation	=	SceneManager.LoadSceneAsync (nameScene);
		asyncOperation.allowSceneActivation	=	false;
		while (asyncOperation.progress < 0.9f) {
			yield return null;
		}
		//yield	return	new	WaitForEndOfFrame ();
	}

	public	void	LoadGames1(){
		asyncOperation.allowSceneActivation	=	true;
	}
    
}
