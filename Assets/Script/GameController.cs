using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*----------------------------------------------------------------------------------------------------------------------
    Touch:
    0 - Prolog
    1 - Suhu Farm*
    2 - Factory
    3 - Suhu Factory
    4 - Beruang
    5 - Gajah
    6 - Store
    7 - Singa
    8 - Games 1
    9 - Games 2
    10 - P2 Kanan
    11 - P2 Kiri
    12 - P1 Kanan
    13 - P1 Kiri
    14 - 
    15 - 
    16 - Farm Atas
    17 - Farm Bawah
    18 - Rumah Kiri
    19 - Rumah Kanan

    Swipe:
    0 - Farm atas 
    1 - Farm bawah
    2 - Rumah Kiri
    3 - Rumah Kanan
    4 - 
    5 - 

    Blow:
    0 - Anak


----------------------------------------------------------------------------------------------------------------------*/
[System.Serializable]
public class Interraction : System.Object
{
    public enum InterractionObject
    {
        Prolog,
        TempFarm,
        Factory,
        TempFactory,
        Beruang,
        Gajah,
        Store,
        Singa,
        Games1,
        Games2,
        P2Kanan,
        P2Kiri,
        P1Kanan,
        P1Kiri,
        Sesuatu1,
        Sesuatu2,
        FarmAtas,
        FarmBawah,
        RumahKiri,
        RumahKanan,
        FarmSwipe,
        RumahSwipe,
        Anak,
        Size
    };

    public enum TouchSensor
    {
        Prolog,
        TempFarm,
        Factory,
        TempFactory,
        Beruang,
        Gajah,
        Store,
        Singa,
        Games1,
        Games2,
        P2Kanan,
        P2Kiri,
        P1Kanan,
        P1Kiri,
        Sesuatu1,
        Sesuatu2,
        FarmAtas,
        FarmBawah,
        RumahKiri,
        RumahKanan,
        Size
    };

    public enum SwipeSensor
    {
        Farm,
        Rumah,
        Size
    };

    public enum BlowSensor
    {
        Anak,
        Size
    };

    public enum SensorType
    {
        Touch,
        Swipe,
        Blow
    };

    public GameObject game;
    public InterractionObject interractionObject;
    public KeyCode keyCode;
    public SensorType sensorType;
    public float timeOut;
    public bool value;
}

public class GameController : MonoBehaviour
{
    public GameObject serHandler;
    private float counter;

    [SerializeField] private Interraction[] interraction = new Interraction[(int)Interraction.InterractionObject.Size];


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < (int)Interraction.InterractionObject.Size; i++)
        {
            if (Input.GetKeyDown(interraction[i].keyCode))
            {
                if (interraction[i].sensorType == Interraction.SensorType.Blow)
                    interraction[i].value = true;
                else
                    interraction[i].value = !interraction[i].value;
            }

            if (interraction[i].value == true)
            {
                counter += Time.deltaTime;
                if (counter > interraction[i].timeOut)
                {
                    interraction[i].value = false;
                    counter = 0;
                }
            }

            if (serHandler.GetComponent<SerialHandler>().serial_is_open)
            {
                if (interraction[i].sensorType == Interraction.SensorType.Touch)
                {
                    if (serHandler.GetComponent<SerialHandler>().touchSensor[i].sensorIsTriggered)
                    {
                        interraction[i].value = !interraction[i].value;
                    }
                }
                else if (interraction[i].sensorType == Interraction.SensorType.Swipe)
                {
                    if (serHandler.GetComponent<SerialHandler>().swipeSensor[i - (int)Interraction.TouchSensor.Size].sensorIsTriggered)
                    {
                        interraction[i].value = !interraction[i].value;
                    }
                }
                else
                {
                    if (serHandler.GetComponent<SerialHandler>().blowSensor[i - (int)Interraction.TouchSensor.Size - (int)Interraction.SwipeSensor.Size].sensorIsTriggered)
                    {
                        interraction[i].value = !interraction[i].value;
                    }
                }
            }
            if (interraction[i].game.name != "Games Menu" && interraction[i].game.name != "Factory" && interraction[i].game.name != "Prolog")
            {
                if (interraction[i].value == true)
                {
                    interraction[i].game.GetComponent<Animator>().SetInteger("AnimState", 1);
                }
                else
                {
                    interraction[i].game.GetComponent<Animator>().SetInteger("AnimState", 0);
                }
            }
            else if (interraction[i].game.name == "Prolog")
            {
                if (interraction[i].value == true)
                {
                    interraction[i].game.GetComponent<Animator>().SetInteger("AnimState", 1);
                }
                else
                {
                    interraction[i].game.GetComponent<Animator>().SetInteger("AnimState", 0);
                }
            }
        }


    }
}

