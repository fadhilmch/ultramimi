using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FARM_Part_Controller : MonoBehaviour {

    [SerializeField]
    FARM_Controller FarmController;

    Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = FarmController.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToPart1()
    {
        animator.Play("FARM_state2_part1_show");
    }

    public void MoveToPart2()
    {
        animator.Play("FARM_state2_part2_show");
    }

    public void MoveToPart3()
    {
        animator.Play("FARM_state2_part3_show");
    }
    
    public void ShowBackAll()
    {
        animator.Play("ShowBackAll");
    }
}
