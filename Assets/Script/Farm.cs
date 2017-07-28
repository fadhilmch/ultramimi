﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{

    private Animator animator;
    public Interaction interaction;
    private FARM_Controller farm;
    private bool timer = false;
    float debounce = 0;
    public float debounceTimeMax = 1;
    public GameObject tangan;
	[SerializeField]
    bool soundState = false;
    public AudioSource source;
    public AudioClip audio1;

    void PlaySound()
    {
        if (soundState == false)
        {
            source.PlayOneShot(audio1);
            soundState = true;
        }

    }
    void TimerCount()
    {
        interaction.counter += Time.deltaTime;
        if (interaction.counter > interaction.timeOut)
        {
            interaction.counter = 0;
            farm.PlayIdle();
            Debug.Log("Farm timeout");
            tangan.SetActive(false);
			soundState = false;
        }
    }

    void ReadInput()
    {
		if (Input.GetKey(interaction.keyCode) || SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("FARM_idle"))
            {
                farm.PlayHide();
                tangan.SetActive(false);
                PlaySound();
                timer = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FARM_showing"))
            {
                farm.PlayIdle();
                tangan.SetActive(false);
                soundState = false;
            }
        }
		/*
        if (SerialHandler.serial_is_open && )
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("FARM_idle"))
            {
                farm.PlayHide();
                tangan.SetActive(false);
                PlaySound();
                timer = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FARM_showing"))
            {
                farm.PlayIdle();
                tangan.SetActive(false);
                soundState = false;
            }
        }*/
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        farm = GetComponent<FARM_Controller>();

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("FARM_idle"))
        {
            tangan.SetActive(true);
            if (debounce < debounceTimeMax)
            {
                debounce += Time.deltaTime;
            }

            if(debounce>=debounceTimeMax)
            {
                ReadInput();
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("FARM_showing"))
        {
            //tangan.SetActive(true);
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