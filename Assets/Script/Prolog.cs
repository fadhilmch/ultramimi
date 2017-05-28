using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prolog : MonoBehaviour {

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
        if (controller.prolog == true)
            animator.SetInteger("AnimState", 1);
        else if (controller.prolog == false)
            animator.SetInteger("AnimState", 0);
        else if (controller.bendera == true && controller.prolog == true)
            animator.SetInteger("AnimState", 2);
        else if (controller.bendera == false && controller.prolog == true)
        {
            animator.SetInteger("AnimState", 0);
            controller.prolog =false;
        }

    }
}
