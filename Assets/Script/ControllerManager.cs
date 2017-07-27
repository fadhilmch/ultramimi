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


    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || SerialHandler.getSensorDown((int)TouchSensor.Player1Kiri)){ControllerPressed(0);}
        if (Input.GetKeyDown(KeyCode.D) || SerialHandler.getSensorDown((int)TouchSensor.Player1Kanan)) { ControllerPressed(1); }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || SerialHandler.getSensorDown((int)TouchSensor.Player2Kiri)) { ControllerPressed(2); }
        if (Input.GetKeyDown(KeyCode.RightArrow) || SerialHandler.getSensorDown((int)TouchSensor.Player2Kanan)) { ControllerPressed(3); }
    }

    private void FixedUpdate()
    {
        if(SerialHandler.getSensorDown((int)TouchSensor.Player1Kiri) || Input.GetKey(KeyCode.A))
        {
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

        if (SerialHandler.getSensorDown((int)TouchSensor.Player2Kiri) || Input.GetKey(KeyCode.LeftArrow))
        {
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
