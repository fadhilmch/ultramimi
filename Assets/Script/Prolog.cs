using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prolog : MonoBehaviour {

    private Animator animator;
    public Interaction interaction;
    public GameObject tap;
    public GameObject tapSub;

    private AudioSource source;
    public AudioClip audio1;
    public AudioClip audio2;

    void TimerCount(int state)
    {
        interaction.counter += Time.deltaTime;
        if (interaction.counter > interaction.timeOut)
        {
            ResetState();
            animator.SetInteger("AnimState", state);
        }
    }

    void ReadInput()
    {
        if (Input.GetKey(interaction.keyCode) || SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
        {
            interaction.value = !interaction.value;
        }
        if (Input.GetKey(interaction.keyCode2) || SerialHandler.getSensorDown((int)interaction.sensorTrigger2))
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
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PROLOG_MOVIE_0"))
        {
            ReadInput();
            if (interaction.value)
            {
                animator.SetInteger("AnimState", 1);
                source.PlayOneShot(audio1);
                ResetState();
            }
            tap.SetActive(true);
            tapSub.SetActive(false);
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PROLOG_MOVIE_1"))
        {
            tap.SetActive(false);
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PROLOG_MOVIE_2"))
        {
            ReadInput();
            if (interaction.value2)
            {
                animator.SetInteger("AnimState", 2);
                source.PlayOneShot(audio2   );
                ResetState();
            }
            tapSub.SetActive(true);
            TimerCount(2);

        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PROLOG_MOVIE_3"))
        {
            tapSub.SetActive(false);
            TimerCount(0);
        }
    }
}
