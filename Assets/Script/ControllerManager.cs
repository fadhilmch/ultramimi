using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour
{
    public GameObject[] players;
    int indexDirection = 0;
    GameObject playerTarget;
    public Color colorBtnNormal;
    public Color colorBtnPressed;
    public Image[] mimiControllers;
    public Image[] leonControllers;
    AudioSource source;
    public AudioClip clipPlayer1;
    public AudioClip clipPlayer2;
    private bool audio1Kiri = false;
    private bool audio1Kanan = false;
    private bool audio2Kiri = false;
    private bool audio2Kanan = false;
	private float responseTimer1 = 0f;
	private const float responseTime1 = 0.2f; 

	private float responseTimer2 = 0f;
	private const float responseTime2 = 0.2f;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
		if (responseTimer1 > responseTime1) {
			
			if(SerialHandler.getSensorDown((int)TouchSensor.Player1Kiri) || Input.GetKey(KeyCode.A))
			{
				responseTimer1 = 0f;
				ControllerPressed (0);
				mimiControllers[0].color = colorBtnPressed;
				if(audio1Kiri == false)
				{
					source.PlayOneShot(clipPlayer1);
					audio1Kiri = true;
				}
			}

			else
			{
				mimiControllers[0].color = colorBtnNormal;
				audio1Kiri = false;
			}

			if (SerialHandler.getSensorDown((int)TouchSensor.Player1Kanan) || Input.GetKey(KeyCode.D))
			{
				responseTimer1 = 0f;
				ControllerPressed (1);
				mimiControllers[1].color = colorBtnPressed;
				if (audio1Kanan == false)
				{
					source.PlayOneShot(clipPlayer1);
					audio1Kanan = true;
				}
			}
			else
			{
				mimiControllers[1].color = colorBtnNormal;
				audio1Kanan = false;
			}
		} else {
			responseTimer1 += Time.deltaTime;
		}

		if (responseTimer2 > responseTime2) {
			
			if (SerialHandler.getSensorDown((int)TouchSensor.Player2Kiri) || Input.GetKey(KeyCode.LeftArrow))
			{
				responseTimer2 = 0f;
				ControllerPressed (2);
				leonControllers[0].color = colorBtnPressed;
				if (audio2Kiri == false)
				{
					source.PlayOneShot(clipPlayer1);
					audio2Kiri = true;
				}
			}
			else
			{
				leonControllers[0].color = colorBtnNormal;
				audio2Kiri = false;
			}

			if (SerialHandler.getSensorDown((int)TouchSensor.Player2Kanan) || Input.GetKey(KeyCode.RightArrow))
			{
				responseTimer2 = 0f;
				ControllerPressed (3);
				leonControllers[1].color = colorBtnPressed;
				if (audio2Kanan == false)
				{
					source.PlayOneShot(clipPlayer1);
					audio2Kanan = true;
				}
			}
			else
			{
				leonControllers[1].color = colorBtnNormal;
				audio2Kanan = false;
			}
		} else {
			responseTimer2 += Time.deltaTime;
		}



    }

    private void FixedUpdate()
    {
        
    }

    public void ControllerPressed(int indexController)
    {


        switch (indexController)
        {
            case 0:
                playerTarget = players[0];
                indexDirection = -1;
                break;
            case 1:
                playerTarget = players[0];
                indexDirection = 1;
                break;
            case 2:
                playerTarget = players[1];
                indexDirection = -1;
                break;
            case 3:
                playerTarget = players[1];
                indexDirection = 1;
                break;
            default:
                break;
        }
        playerTarget.GetComponent<Player>().Move(indexDirection);
    }

}
