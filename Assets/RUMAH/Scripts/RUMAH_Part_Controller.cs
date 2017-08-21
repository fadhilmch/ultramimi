using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RUMAH_Part_Controller : MonoBehaviour {

    [SerializeField]
    RUMAH_Controller RumahController;

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = RumahController.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveToPart1()
    {
        animator.Play("RUMAH_state2_part1_show");
    }

    public void ShowBackAll()
    {
        animator.Play("ShowBackAll");
    }
}
