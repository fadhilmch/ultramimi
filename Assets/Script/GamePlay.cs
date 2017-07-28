using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour {
	public GameObject panelAttention;
	public GameObject panelGo;
    public GameObject panelGameOver;
    public Text lblTimer;
	public float timer;
	public float timerSlider = 0;
	public bool isStart = false;
	public bool isGameOver = false;
	private int scoreMimi = 0;
	private int scoreSinga = 0;
	public Text lblScoreMimi;
	public Text lblScoreSinga;
    public bool isPlayer1AllowMove = false;
    public bool isPlayer2AllowMove = false;
    public SpawnerManager[] spawnerManager;
	public Slider[] sliderKarakter;
	public GameOverManager gameOverManager;
	public GameObject mimi;
	public GameObject leo;
	public	TimerAnimationController	timerAnimatorController;

    IEnumerator Start(){
		yield return new WaitForSeconds (3);
		panelGo.SetActive (true);
		panelAttention.SetActive(false);
		Invoke("StartGame", 3);
	}

	private void StartGame()
    {
		timerAnimatorController.StartTime ();
		panelGo.SetActive (false);
        panelAttention.SetActive(false);
        spawnerManager[0].StartGame();
        spawnerManager[1].StartGame();
        isPlayer1AllowMove = true;
        isPlayer2AllowMove = true;
        isStart = true;
		mimi.GetComponent<Player> ().AnimPlayer (true);
		leo.GetComponent<Player> ().AnimPlayer (true);
    }

	void Update(){
		if (isStart && !isGameOver) {
			timer -= Time.deltaTime;
			timerSlider += Time.deltaTime;
			if (timer < 0) {
				timer = 0;
				GameOver ();
			}

			if (timerSlider > 30) {
				timerSlider = 30;
			}

			int timeInINteger = (int)timer;
			lblTimer.text = timeInINteger.ToString();
			sliderKarakter [0].value = timerSlider;
			sliderKarakter [1].value = timerSlider;
		}
	}

	private void GameOver(){
		mimi.GetComponent<Player> ().AnimPlayer (false);
		leo.GetComponent<Player> ().AnimPlayer (false);
        panelGameOver.SetActive(true);
        isGameOver = true;
        isPlayer1AllowMove = false;
        isPlayer2AllowMove = false;

		if (scoreMimi > scoreSinga) {
			gameOverManager.SetWin (0);
		} else if (scoreMimi < scoreSinga) {
			gameOverManager.SetWin (1);
		} else if (scoreMimi == scoreSinga) {
			gameOverManager.SetWin (2);
		}
	}

	public void UpdateScore(int indexKarakter, int scoreAddition){
		switch (indexKarakter) {
		// score mimi
		case 0:
			scoreMimi = scoreMimi + scoreAddition;
			lblScoreMimi.text = scoreMimi.ToString ();
			break;
		// score singa
		case 1:
			scoreSinga = scoreSinga + scoreAddition;
			lblScoreSinga.text = scoreSinga.ToString ();
			break;
		}
	}

    public IEnumerator FreezeMimi()
    {
        isPlayer1AllowMove = false;
        yield return new WaitForSeconds(2);
        isPlayer1AllowMove = true;
    }

    public IEnumerator FreezeSinga()
    {
        isPlayer2AllowMove = false;
        yield return new WaitForSeconds(2);
        isPlayer2AllowMove = true;
    }

    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);        
    }
}
