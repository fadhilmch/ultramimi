using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAnak : MonoBehaviour {

    private anakMove anak;
    private Animator animator;
    public Controller controller;

	// Use this for initialization
	void Start () {
        anak = GetComponent<anakMove>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (controller.anak == true)
            animator.SetInteger("AnimState", 1);
        else
            animator.SetInteger("AnimState", 0);
    }
}
