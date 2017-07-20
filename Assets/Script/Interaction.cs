using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*----------------------------------------------------------------------------------------------------------------------
    Touch:
    0 - Prolog
    1 - Prolog Sub
    2 - Factory
    3 - Store
    4 - Games
  
    5 - Farm Atas
    6 - Farm Bawah
    7 - Rumah Kiri
    8 - Rumah Kanan

    9 - Player 1 Kanan
    10 - Player 1 Kiri
    11 - Player 2 Kanan
    12 - Player 2 Kiri

    Swipe:
    0 - Farm atas 
    1 - Farm bawah
    2 - Rumah Kiri
    3 - Rumah Kanan

    Blow:
    0 - Anak
----------------------------------------------------------------------------------------------------------------------*/
[System.Serializable]
public class Interaction : System.Object
{
    public enum InteractionObject
    {
        Prolog,
        Prolog_Sub,
        Factory,
        Store,
        Games,
        Farm,
        Rumah,
        Balon,
        Size
    };

    public enum SensorTrigger
    {
        Prolog,
        PrologSub,
        Factory,
        Store,
        Games1,
        Games2,
        FarmAtas,
        FarmBawah,
        RumahKiri,
        RumahKanan,
        Player1Kanan,
        Player1Kiri,
        Player2Kanan,
        Player2Kiri,
        Farm,
        Rumah,
        Balon,
        Size
    };

    public KeyCode keyCode;
    public KeyCode keyCode2;
    public SensorTrigger sensorTrigger1;
    public SensorTrigger sensorTrigger2;
    public float timeOut;
    [HideInInspector]
    public bool value;
    [HideInInspector]
    public bool value2;
    [HideInInspector]
    public float counter;
}
