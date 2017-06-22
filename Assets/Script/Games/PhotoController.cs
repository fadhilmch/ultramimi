using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PhotoController : MonoBehaviour {
    public enum PhotoState
    {
        Enter = 0,
        Photo = 1,
        Share = 2,
        Exit = 3
    };
    public static PhotoState photoState = PhotoState.Enter;
    public bool isTriggered = false;
    public string nextSceneName;

    private Text congratulationText;
    private string[] congratulation = { "SELAMAT ANDA BERHASIL MENEMUKAN\nSEMUA MIMI YANG BERSEMBUNYI", "MAAF ANDA BELUM BERHASIL\nMENEMUKAN SEMUA MIMI YANG BERSEMBUNYI","SELAMAT PLAYER 1\nANDA MENANG!","SELAMAT PLAYER 2\nANDA MENANANG!", "SELAMAT ANDA BERDUA\nTELAH MENANG!"};


	// Use this for initialization
	void Start () {
        congratulationText = GameObject.Find("Canvas/Text").GetComponent<Text>();
        Debug.Log(GameStatus.gameOne + " " + GameStatus.playerOneWin);
        if(GameStatus.gameOne)
        {
            if(GameStatus.playerOneWin)
            {
                congratulationText.text = congratulation[0];
            }
            else
            {
                congratulationText.text = congratulation[1];
            }
        }
        else
        {
            if(GameStatus.bothWin)
            {
                congratulationText.text = congratulation[4];
            }
            else
            {
                if(GameStatus.playerOneWin)
                {
                    congratulationText.text = congratulation[2];
                }
                else
                {
                    congratulationText.text = congratulation[3];
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(isTriggered)
        {
            if(photoState == PhotoState.Enter)
            {
                photoState = PhotoState.Photo;
            }
            else if(photoState == PhotoState.Share)
            {
                photoState = PhotoState.Exit;
            }
            isTriggered = false;

        }
        if(photoState == PhotoState.Exit)
        {
            SceneManager.LoadScene(nextSceneName);
        }
	}
}
