using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InterractionType : System.Object
{
    public GameObject interactionObject;
    public KeyCode interactionKey;
    public int index;
    // Use this for initialization
}

public class InteractionInterface : MonoBehaviour {
    public InterractionType[] interraction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
