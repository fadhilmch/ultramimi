using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prolog : MonoBehaviour {

    private Animator animator;
    public Interaction interaction;


    void TimerCount(int state)
    {
        interaction.counter += Time.deltaTime;
        if (interaction.counter > interaction.timeOut)
        {
            interaction.counter = 0;
            interaction.value = false;
            interaction.value2 = false;
            animator.SetInteger("AnimState", state);
        }
    }

    void ReadInput()
    {
        if (Input.GetKey(interaction.keyCode))
        {
            interaction.value = !interaction.value;
        }
        if (Input.GetKey(interaction.keyCode2))
        {
            interaction.value2 = !interaction.value2;
        }
        /*
        if (SerialHandler.serial_is_open && SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
        {
            interaction.value = !interaction.value;
        }
        if (SerialHandler.serial_is_open && SerialHandler.getSensorDown((int)interaction.sensorTrigger2))
        {
            interaction.value2 = !interaction.value2;
        }*/
    }

    void ResetState()
    {
        interaction.value = false;
        interaction.value2 = false;
        interaction.counter = 0;
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PROLOG_MOVIE_0"))
        {
            ReadInput();
            if (interaction.value)
            {
                animator.SetInteger("AnimState",1);
                ResetState();
            }
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PROLOG_MOVIE_2"))
        {
            ReadInput();
            if (interaction.value2)
            {
                animator.SetInteger("AnimState", 2);
                ResetState();
            }

            TimerCount(2);

        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PROLOG_MOVIE_3"))
        {
            TimerCount(0);
        }
    }
}
