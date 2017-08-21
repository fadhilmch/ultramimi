using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PABRIK_Part4_Controller : MonoBehaviour {

    [SerializeField]
    PABRIK_Susu_Mover SusuMover;

    public void StartMovingSusu()
    {
        SusuMover.StartMoving();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
