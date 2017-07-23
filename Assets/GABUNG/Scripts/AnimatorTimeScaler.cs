using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTimeScaler : MonoBehaviour {

    Animator animator;

    public float speed = 1;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.speed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
