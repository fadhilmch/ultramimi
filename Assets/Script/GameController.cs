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
public class Interaction : System.Object
{
    public enum InteractionObject
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

    public enum SensorType
    {
        Touch,
        Swipe,
        Blow
    };

    public GameObject game;
    public AudioClip sound_effect1;
    public AudioClip sound_effect2;
    public AudioSource source;
    public Animator animator;
    public InteractionObject interactionObject;
    public KeyCode keyCode;
    public SensorType sensorType;
    public float timeOut;
    public bool value;
}

public class GameController : MonoBehaviour
{
    private float counter;

    [SerializeField] private Interaction[] interaction = new Interaction[(int)Interaction.InteractionObject.Size];

    void Toggle(bool value)
    {
        value = !value;
    }

    // Use this for initialization
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < (int)Interaction.InteractionObject.Size; i++)
        {
            // Read input from keyboard
            if (Input.GetKeyDown(interaction[i].keyCode))
            {
                if (interaction[i].sensorType == Interaction.SensorType.Blow)
                    interaction[i].value = true;
                else
                    Toggle(interaction[i].value); 
            }

            // Time out control
            if (interaction[i].value == true)
            {
                counter += Time.deltaTime;
                if (counter > interaction[i].timeOut)
                {
                    Toggle(interaction[i].value);
                    counter = 0;
                }
            }

            // Read input from serial handler
            if (SerialHandler.serial_is_open)
            {
                interaction[i].value = SerialHandler.getSensorTrig(i);
            }
            
        }


    }
}

