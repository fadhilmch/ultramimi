using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string games_1_scene;
    public string games_2_scene;
    public Interaction interaction;
    private Fading fading;
    private Animator animator;
    private int state = 0;

    void TimerCount()
    {
        interaction.counter += Time.deltaTime;
        if (interaction.counter > interaction.timeOut)
        {
            interaction.counter = 0;
            interaction.value = false;
            interaction.value2 = false;
            animator.SetInteger("AnimState",0);
        }
    }

    // Use this for initialization
    void Start () {
        fading = GetComponent<Fading>();
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
                state = 1;
                animator.SetInteger("AnimState",1);
                interaction.value = false;
                interaction.value2 = false;
            }
        }
        if (state == 1)
        {
            if (interaction.value)
            {
                NewScene(games_1_scene);
            }
            else if (interaction.value2)
            {
                NewScene(games_2_scene);
            }
            TimerCount();
        }
    }

    public void NewScene(string scene)
    {
        float fadeTime =fading.BeginFade(1);
        //yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(scene);
        
    }


    
}
