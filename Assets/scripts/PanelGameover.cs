using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGameover : MonoBehaviour {

    public GameObject[] containerFinish;
	public void SetGameoverCondition(bool isWin){
        containerFinish[0].SetActive(isWin);
        containerFinish[1].SetActive(!isWin);
	}
}
