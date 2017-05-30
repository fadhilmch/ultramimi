using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prolog : MonoBehaviour
{
    public bool tapActiva = false;
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
        if (animator.GetInteger("AnimState") == 0)
        {
            if (controller.prolog == true)
            {
                animator.SetInteger("AnimState", 1);
                controller.bendera = false;
            }
        }

        if (controller.prolog == false)
        {
            animator.SetInteger("AnimState", 0);
            controller.tapActive = false;
        }

        if (animator.GetInteger("AnimState") == 1)
        {
            if (controller.bendera == true)
            {
                animator.SetInteger("AnimState", 2);
                controller.tapActive = true;
            }
        }


    }
}
