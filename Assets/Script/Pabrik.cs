using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pabrik : MonoBehaviour
{

    private Animator animator;
    public Interaction interaction;
    private PABRIK_Controller pabrik;
    private bool timer = false;
    private float temp = 0;
    public float debounceTimeMax = 1;
    float debounce = 0;
    float counterTemp = 0;
    private float counterTempMax = 13;
    public GameObject tap;
	public AudioSource source;
    public AudioClip audio1;
	public AudioClip audio2;
	public AudioSource audio3;
	private bool stateTemp = false;
	private bool stateBell = false;
	public GameObject button;
	int stopwatchCount;

	[SerializeField]
    private bool soundState = false;

    void TimerCount()
    {
        interaction.counter += Time.deltaTime;
        if (interaction.counter > interaction.timeOut)
        {
            interaction.counter = 0;
            pabrik.DoIdle = true;
            temp = 0;
			soundState = false;
			stateTemp = false;
			stateBell = false;
        }

    }

    void PlaySound()
    {
        if (soundState == false)
        {
            source.PlayOneShot(audio1);
            soundState = true;
        }
        
    }
    void ReadInput()
    {
		if (Input.GetKey(interaction.keyCode) || SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
        {
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("PABRIK_idle")) {
				pabrik.DoHide = true;
				PlaySound ();
				tap.SetActive (false);
				timer = true;
			} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("PABRIK_showing")) {
				pabrik.DoIdle = true;
				temp = 0;

				soundState = false;
				stateTemp = false;
				stateBell = false;

			} else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("PABRIK_state2_part3_show")) {
				if (temp < 1) {
					temp += Time.deltaTime / 4f;
					pabrik.SetHotThermometerValue (temp);
					stopwatchCount = (int)(temp*4);
					button.GetComponent<Animator> ().SetInteger ("AnimState", 1);
					pabrik.StopwatchController.SetValue ((4-stopwatchCount), 4,(1-temp));
					if (stateTemp == false) {
						audio3.Play ();
						stateTemp = true;
					}

				}

			}
        }
		else {
			audio3.Stop ();
			button.GetComponent<Animator> ().SetInteger ("AnimState", 0);
			stateTemp = false;
		}

     	/*
        else if (SerialHandler.serial_is_open && SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_idle"))
            {
                pabrik.DoHide = true;
                tap.SetActive(false);
                PlaySound();
                timer = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_showing"))
            {
                pabrik.DoIdle = true;
                temp = 0;

                soundState = false;

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_state2_part3_show"))
            {
                if (temp < 1)
                {
                    temp += Time.deltaTime / 4f;
                    pabrik.SetHotThermometerValue(temp);
                }
            }

        }
		*/
        /*
        else if (Input.GetKey(interaction.keyCode) == false && (SerialHandler.serial_is_open && SerialHandler.getSensorDown((int)interaction.sensorTrigger1) == false))
        {
            
            if (temp < 1)
            {
                if (temp >= 0)
                {
                    temp -= Time.deltaTime/4f;
                }
                pabrik.SetHotThermometerValue(temp);
            }


        }*/


    }


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        pabrik = GetComponent<PABRIK_Controller>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_idle"))
        {
            tap.SetActive(true);
            counterTemp = 0;
            
            if (debounce < debounceTimeMax)
            {
                debounce += Time.deltaTime;
            }

            if (debounce >= debounceTimeMax)
            {
                ReadInput();
            }
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_showing"))
        {
            
            ReadInput();
            debounce = 0;

            if (timer == true)
            {
                interaction.counter = 0;
                timer = false;

            }
            TimerCount();
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_state2_part3_show"))
        {
            ReadInput();
            counterTemp += Time.deltaTime;
			//Debug.Log ("counter " + counterTemp);
            if (counterTemp > counterTempMax)
            {
                temp += Time.deltaTime / 4;
                pabrik.SetHotThermometerValue(temp);
				pabrik.StopwatchController.SetValue ((4-stopwatchCount), 4,(1-temp));
            }
			if (temp >= 1)
			{
				counterTemp = 0;
				if (stateBell == false) {
					source.PlayOneShot (audio2);
					stateBell = true;
				}
				audio3.Stop ();
			}

        }


    }
}
