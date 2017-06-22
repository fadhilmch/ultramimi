using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour {
    public int maxPoint = 5;
    public int currentPoint = 0;
    public int lastPoint = 0;
    public static  bool[] pointStatus = new bool[6];
    private Image mimiTemp;
	// Use this for initialization
	void Start () {
        
	    for(int i = 1; i <= maxPoint; i++)
        {
            PointSystem.pointStatus[i] = false;
            mimiTemp = GameObject.Find("Canvas/Points/Mimi" + i).GetComponent<Image>();
            Color c = mimiTemp.color;
            c.a = .5f;
            mimiTemp.color = c;
        }
	}
	
	// Update is called once per frame
	void Update() {
        for (int i = 1; i <= maxPoint; i++)
        {
            currentPoint = 0;
            if(PointSystem.pointStatus[i])
            {
                mimiTemp = GameObject.Find("Canvas/Points/Mimi" + i).GetComponent<Image>();
                Color c = mimiTemp.color;
                c.a = 1f;
                mimiTemp.color = c;
                currentPoint += 1;
            }
        }
        if(lastPoint != currentPoint)
        {
            lastPoint = currentPoint;
        }
        if(lastPoint == maxPoint)
        {
            GameStatus.gameOne = true;
            GameStatus.playerOneWin = true;
            GameController.gameState = GameController.GameState.Finished;
        }
    }
}
