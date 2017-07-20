using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_1Stage : MonoBehaviour
{

    private Animator animator;
    public Interaction interaction;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Read Input from keyboard
        if (Input.GetKeyDown(interaction.keyCode))
        {
            interaction.value = !interaction.value;
            Debug.Log(interaction.value);
        }

        if (SerialHandler.serial_is_open)
        {
            //interaction.value = SerialHandler.getSensorDown((int)interaction.sensorTrigger1);
        }

        if (interaction.value == true)
        {
            animator.SetInteger("AnimState", 1);
            interaction.counter += Time.deltaTime;
            if(interaction.counter > interaction.timeOut)
            {
                interaction.value = !interaction.value;
                interaction.counter = 0;
            }
            
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }

    }

}