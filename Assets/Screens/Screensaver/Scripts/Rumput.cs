using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Rumput : MonoBehaviour {

    public string AnimationName;

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.Play(AnimationName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
