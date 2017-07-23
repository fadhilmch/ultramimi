﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour {

    private Animator animator;
    public Interaction interaction;
    private STORE_Controller store;
    private bool timer = false;
    float debounce = 0;
    public float debounceTimeMax = 1;
    public GameObject tangan;


    void TimerCount()
    {
        interaction.counter += Time.deltaTime;
        if (interaction.counter > interaction.timeOut)
        {
            interaction.counter = 0;
            store.PlayIdle();
            tangan.SetActive(false);
            Debug.Log("Farm timeout");
        }
    }

    void ReadInput()
    {
        if (Input.GetKey(interaction.keyCode))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("STORE_idle"))
            {
                store.PlayHide();
                tangan.SetActive(false);
                timer = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("STORE_showing"))
            {
                store.PlayIdle();
                tangan.SetActive(false);
            }
        }

        if (SerialHandler.serial_is_open && SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("STORE_idle"))
            {
                store.PlayHide();
                tangan.SetActive(false);
                timer = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("STORE_showing"))
            {
                store.PlayIdle();
                tangan.SetActive(false);
            }
        }
    }


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        store = GetComponent<STORE_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("STORE_idle"))
        {
            tangan.SetActive(true);
            if (debounce < debounceTimeMax)
            {
                debounce += Time.deltaTime;
            }

            if (debounce >= debounceTimeMax)
            {
                ReadInput();
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("STORE_showing"))
        {
            tangan.SetActive(true);
            ReadInput();
            debounce = 0;
            if (timer == true)
            {
                interaction.counter = 0;
                timer = false;
            }
            TimerCount();
        }

    }
}
