using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {
    public AudioClip sound;
    private AudioSource source;
    private Animator animator;
    private Controller controller;

    private bool audiostate = false;
    private bool laststate = false;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        controller = GetComponent<Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        if (controller.farm == true)
        {
            animator.SetInteger("AnimState", 1);
            if (laststate == false)
                audiostate = true;
            laststate = true;
        }
        else if (controller.farm == false)
        {
            animator.SetInteger("AnimState", 0);
            laststate = false;
        }

        if (audiostate == true)
        {
            source.PlayOneShot(sound, .05f);
            audiostate = false;
        }

    }
}
