using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GamesMenu : MonoBehaviour {

    public string games1scene;
    public string games2scene;

    private Animator animator;
    private Controller controller;
    private Fading fading;
    float tgames = 0f;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        controller = GetComponent<Controller>();
        fading = GetComponent<Fading>();
    }
	
	// Update is called once per frame
	void Update () {
        if (animator.GetInteger("AnimState") == 0)
        {
            if (controller.games1 == true || controller.games2 ==true)
            {
                animator.SetInteger("AnimState", 1);
                controller.games2 = false;
                controller.games1 = false;
            }
        }


        if (animator.GetInteger("AnimState") == 1)
        {
            if (controller.games1 == true)
            {
                float fadeTime = fading.BeginFade(1);
                SceneManager.LoadScene(games1scene);

            }
            else if (controller.games2 == true)
            {
                float fadeTime = fading.BeginFade(1);
                SceneManager.LoadScene(games2scene);

            }

            tgames += Time.deltaTime;
            if (tgames > 10f)
            {
                animator.SetInteger("AnimState", 0);
                controller.games1 = false;
                controller.games2 = false;
                tgames = 0;
            }
            
        }
    }
}
