using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour {

    private Animator animator;
    private Controller controller;

    private int state = 0;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        controller = GetComponent<Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == 0 && controller.factory == true)
        {
            Debug.Log("Masuk");
            animator.SetInteger("AnimState", 1);
            state = 1;
            Reset();
        }
        if (state == 1 && controller.factory == true )
        {
            animator.SetBool("Jawaban", false);
            state = 2;
            Reset();
        }
        if (state == 1 && controller.jawaban == true)
        {
            animator.SetBool("Jawaban", true);
            state = 2;
            Reset();
        }
	}

    private void Reset()
    {
        controller.factory = false;
        controller.jawaban = false;
    }
}
