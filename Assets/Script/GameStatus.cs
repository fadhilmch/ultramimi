using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour{
    public static bool gameOne;
    public static bool playerOneWin;
    public static bool bothWin = false;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
