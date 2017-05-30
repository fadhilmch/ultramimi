﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour {

    private Animator animator;
    private Controller controller;
	private PlayWebCam webCam;
	public GameObject plane;
	public bool captureWebcam = false;
	//public bool play = true;
	private int state = 0;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        controller = GetComponent<Controller>();
		webCam = plane.GetComponent<PlayWebCam> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == 0 && controller.factory == true)
        {
            animator.SetInteger("AnimState", 1);
            state = 1;
        }
        if (state == 1 && controller.factory == false && controller.jawaban==false)
        {
            Debug.Log("Masuk");
            animator.SetInteger("Jawaban", 1);
            state = 2;
            Reset();
        }
        if (state == 1 && controller.jawaban == true && controller.factory == true)
        {
            Debug.Log("Masuk 2");
            animator.SetInteger("Jawaban", 2);
            state = 2;
            Reset();
        }
		if (animator.GetInteger ("AnimState") == 5 ||animator.GetInteger ("AnimState") == 6 ) {
			plane.SetActive (true);

			if (captureWebcam) {
				captureWebcam = false;
				StartCoroutine (webCam.CapturePNG());
				animator.SetInteger ("AnimState", 6);
				Debug.Log("AnimState = " + animator.GetInteger ("AnimState")); 
			}
		} else {
			plane.SetActive (false);
		}
	}

    private void Reset()
    {
        controller.factory = false;
        controller.jawaban = false;
        //animator.SetInteger("Jawaban", 0);
    }
}