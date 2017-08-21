using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PABRIK_Part_Controller : MonoBehaviour {

    [SerializeField]
    PABRIK_Controller PabrikController;

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = PabrikController.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveToPart1()
    {
        animator.Play("PABRIK_state2_part1_show");
    }

    public void MoveToPart2()
    {
        animator.Play("PABRIK_state2_part2_show");
    }

    public void MoveToPart3()
    {
        animator.Play("PABRIK_state2_part3_show");
    }

    public void MoveToPart4()
    {
        animator.Play("PABRIK_state2_part4_show");
    }

    public void ShowBackAll()
    {
        animator.Play("ShowBackAll");
    }
}
