using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FARM_Sapi_Controller : MonoBehaviour {

    Animator animator;

    public float animateDelay;
    public float speed = 1;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.enabled = false;

        StartCoroutine(DelayAndPlay());
	}

    IEnumerator DelayAndPlay()
    {
        yield return new WaitForSeconds(animateDelay);
        animator.enabled = true;
        animator.speed = speed;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
