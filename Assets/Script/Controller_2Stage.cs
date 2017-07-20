using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_2Stage : MonoBehaviour {

    private Animator animator;
    public Interaction interaction;
    private int state = 0;


    void TimerCount()
    {
        interaction.counter += Time.deltaTime;
        if (interaction.counter > interaction.timeOut)
        {
            interaction.counter = 0;
            interaction.value = false;
            interaction.value2 = false;
            animator.SetTrigger("idle");
        }
    }

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(interaction.keyCode))
        {
            interaction.value = !interaction.value;
        }
        if (Input.GetKeyDown(interaction.keyCode2))
        {
            interaction.value2 = !interaction.value2;
        }
        
        if (SerialHandler.serial_is_open && SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
        {
            interaction.value = !interaction.value;
        }
        if (SerialHandler.serial_is_open && SerialHandler.getSensorDown((int)interaction.sensorTrigger2))
        {
            interaction.value2 = !interaction.value2;
        }
        

        if (state == 0)
        {
            if (interaction.value)
            {
                animator.SetTrigger("state1");
                state = 1;
                interaction.value2 = false;
                interaction.value = false;
            }
        }

        else if(state == 1)
        {
            if(interaction.value2)
            {
                animator.SetTrigger("state2");
                state = 2;
                interaction.value2 = false;
                interaction.value = false;
                interaction.counter = 0;
            }

            TimerCount();
            
        }

        else if (state == 2)
        {
            TimerCount();
        }
	}
}
