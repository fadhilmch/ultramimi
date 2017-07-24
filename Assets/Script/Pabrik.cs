using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pabrik : MonoBehaviour
{

    private Animator animator;
    public Interaction interaction;
    private PABRIK_Controller pabrik;
    private bool timer = false;
    private float counter = 0;
    public float debounceTimeMax = 1;
    float debounce = 0;
    float counterTemp = 0;
    public float counterTempMax = 8;
    public GameObject hold;
    public GameObject tap;

    void TimerCount()
    {
        interaction.counter += Time.deltaTime;
        if (interaction.counter > interaction.timeOut)
        {
            interaction.counter = 0;
            tap.SetActive(false);
            hold.SetActive(false);
            pabrik.DoIdle = true;
            Debug.Log("Pabrik Timeout");
        }
    }

    void ReadInput()
    {
        if (Input.GetKey(interaction.keyCode))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_idle"))
            {
                tap.SetActive(false);
                hold.SetActive(false);
                pabrik.DoHide = true;

                timer = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_showing"))
            {
                tap.SetActive(false);
                hold.SetActive(false);
                pabrik.DoIdle = true;

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_state2_part3_show"))
            {
                counter += Time.deltaTime / 2;
                pabrik.SetHotThermometerValue(counter);
                if(counter >= 1f)
                {
                    hold.SetActive(false);
                    tap.SetActive(false);
                    Debug.Log("masuk");
                }
            }
        }


        if (SerialHandler.serial_is_open && SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_idle"))
            {
                tap.SetActive(false);
                hold.SetActive(false);
                pabrik.DoHide = true;

                timer = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_showing"))
            {
                tap.SetActive(false);
                hold.SetActive(false);
                pabrik.DoIdle = true;

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_state2_part3_show"))
            {
                counter += Time.deltaTime / 2;
                pabrik.SetHotThermometerValue(counter);
                if (counter >= 1f)
                {
                    hold.SetActive(false);
                    tap.SetActive(false);
                }
            }

        }

        else if (Input.GetKey(interaction.keyCode) == false && (SerialHandler.serial_is_open && SerialHandler.getSensorDown((int)interaction.sensorTrigger1) == false))
        {
            if (counter < 1)
            {
                if (counter >= 0)
                {
                    counter -= Time.deltaTime;
                }
                pabrik.SetHotThermometerValue(counter);
            }
        }


    }


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        pabrik = GetComponent<PABRIK_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_idle"))
        {
            tap.SetActive(true);
            hold.SetActive(false);
            counterTemp = 0;
            //counter = 0;
            if (debounce < debounceTimeMax)
            {
                debounce += Time.deltaTime;
                Debug.Log(debounce);
            }

            if (debounce >= debounceTimeMax)
            {
                ReadInput();
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_showing"))
        {
            tap.SetActive(true);
            hold.SetActive(false);
            ReadInput();
            debounce = 0;

            if (timer == true)
            {
                interaction.counter = 0;
                timer = false;

            }
            TimerCount();
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_state2_part3_show"))
        {
            ReadInput();
            if(counter < 1f)
                hold.SetActive(true);
            tap.SetActive(false);
            /*
            counterTemp += Time.deltaTime;
            Debug.Log(counterTemp);
            if (counterTemp > counterTempMax)
            {
                counter += Time.deltaTime / 2;
                pabrik.SetHotThermometerValue(counter);
            }*/
        }


    }
}
