using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeRumah : MonoBehaviour {
    private Animator animator;
    private Controller controller;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.rumah == true)
            animator.SetInteger("AnimState", 1);
        else if (controller.rumah == false)
            animator.SetInteger("AnimState", 0);
    }
}
