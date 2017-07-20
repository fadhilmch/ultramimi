using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prolog : MonoBehaviour
{
    public AudioClip sound;
    private AudioSource source;
    public bool tapActiva = false;
    private Animator animator;
    private Controller controller;


    private bool audiostate = false;
    private bool laststate = false;
    private bool laststate2 = false;
    // Use this for initialization
    void Start()
    {

        source = GetComponent<AudioSource>();
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
                if (laststate == false)
                    audiostate = true;
                laststate = true;
            }
        }

        if (controller.prolog == false)
        {
            animator.SetInteger("AnimState", 0);
            controller.tapActive = false;
            laststate = false;
            laststate2 = false;
        }

        if (animator.GetInteger("AnimState") == 1)
        {
            if (controller.bendera == true)
            {
                animator.SetInteger("AnimState", 2);
                controller.tapActive = true;
                if (laststate2 == false)
                    audiostate = true;
                laststate2 = true;
            }
        }


        if (audiostate == true)
        {
            source.PlayOneShot(sound, .7f);
            audiostate = false;
        }
    }
}
