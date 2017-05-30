using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAppearPabrik : MonoBehaviour {
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
        if (controller.factory == true)
            animator.SetInteger("AnimState", 1);
        else if (controller.factory == false)
            animator.SetInteger("AnimState", 0);
    }
}
