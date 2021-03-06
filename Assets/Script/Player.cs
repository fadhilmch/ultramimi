﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float[] targetPosX;
	public int currentPos = 1;
	public int indexKarakter = 0;
	public GamePlay gamePlay;
	public GameObject pusing;
    public AudioClip audioCrash;
    public AudioClip audioScore;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Move(int targetPos){
        if (currentPos >= 0 && currentPos < targetPosX.Length) {
			currentPos += targetPos;
			if (currentPos < 0)
				currentPos = 0;
			if (currentPos > targetPosX.Length - 1)
				currentPos = targetPosX.Length - 1;
			gameObject.transform.position = new Vector3 (targetPosX[currentPos], gameObject.transform.position.y, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
        Destroy(col.gameObject);
        if (col.gameObject.tag == "obstacle") {
            CancelInvoke("HidePusing");
            Invoke("HidePusing", 2);
            pusing.SetActive(true);
			gamePlay.UpdateScore (indexKarakter, -5);
            source.PlayOneShot(audioCrash);
		} else if (col.gameObject.tag == "point 1") {
			gamePlay.UpdateScore (indexKarakter, 5);
            source.PlayOneShot(audioScore);
		} else if (col.gameObject.tag == "point 2") {
			gamePlay.UpdateScore (indexKarakter, 10);
            source.PlayOneShot(audioScore);
        }
	}

	private void HidePusing(){
		pusing.SetActive (false);
	}

	public void AnimPlayer(bool run){
		Animator animPlayer = GetComponent<Animator> ();
		if (run) {
			animPlayer.SetBool ("New Bool", true);
		} else {
			animPlayer.SetBool ("New Bool", false);
		}
	}
}
