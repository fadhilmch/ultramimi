using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoundTimer : MonoBehaviour {
	public Text countDown;
	public float timeLeft = 30f;
    
	// Use this for initialization
	void Start () {
		countDown.text = timeLeft.ToString("0");	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.gameState == GameController.GameState.Play) {
			timeLeft -= Time.deltaTime;
			countDown.text = timeLeft.ToString ("0");
			if (timeLeft < 0) {
				countDown.text = "Finished!";
                if(GameStatus.gameOne)
                    GameStatus.playerOneWin = false;
                else
                {
                    if(GameObject.Find("Characters/Character P1/Mimi").GetComponent<Player>().totalScore > GameObject.Find("Characters/Character P2/Elfin").GetComponent<Player>().totalScore)
                    {
                        GameStatus.playerOneWin = true;
                    }
                    else if(GameObject.Find("Characters/Character P1/Mimi").GetComponent<Player>().totalScore < GameObject.Find("Characters/Character P2/Elfin").GetComponent<Player>().totalScore)
                    {
                        GameStatus.playerOneWin = false;
                    }
                    else
                    {
                        GameStatus.playerOneWin = false;
                        GameStatus.bothWin = true;
                    }
                }
                GameController.gameState = GameController.GameState.Finished;
                Debug.Log("Game State : " +  GameController.gameState);
            }
		}
	}
}
