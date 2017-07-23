using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public GameObject[] players;
    int indexDirection = 0;
    GameObject playerTarget;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || SerialHandler.getSensorDown((int)TouchSensor.Player1Kiri)) { ControllerPressed(0); }
        if (Input.GetKeyDown(KeyCode.D) || SerialHandler.getSensorDown((int)TouchSensor.Player1Kanan)) { ControllerPressed(1); }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || SerialHandler.getSensorDown((int)TouchSensor.Player2Kiri)) { ControllerPressed(2); }
        if (Input.GetKeyDown(KeyCode.RightArrow) || SerialHandler.getSensorDown((int)TouchSensor.Player2Kanan)) { ControllerPressed(3); }
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
