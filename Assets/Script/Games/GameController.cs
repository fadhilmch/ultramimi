using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour {
	public enum GameState
	{
		Stop = 0,
		Play = 1,
		Finished = 2
	}
	public static GameState gameState = GameState.Stop;
	public float countDown = 3f;
    public enum GameType
    {
        GameOne,
        GameTwo
    };
    public GameType gameType;
    public string photoScene = "Photo";

    private float counter;
	// Use this for initialization
	void Start () {
		counter = countDown;
        if (gameType == GameType.GameOne)
            GameStatus.gameOne = true;
        else
            GameStatus.gameOne = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(gameState == GameState.Stop)
        {
            if (counter < 0f)
            {
                gameState = GameState.Play;
                GameObject.Find("Systems/Start/Countdown").SetActive(false);
            }
            else
            {
                counter -= Time.deltaTime;
            }
        }
		
        if(gameState == GameState.Finished)
        {
            Debug.Log("Finished");
            
            SceneManager.LoadScene(photoScene);
        }
	}
}
