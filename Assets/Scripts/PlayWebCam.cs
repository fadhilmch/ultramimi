using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayWebCam : MonoBehaviour {
	WebCamTexture camStream;
	Renderer myRender;
	// Use this for initialization
	void Start () {
		camStream = new WebCamTexture ();
		myRender = GetComponent<Renderer> ();
		myRender.material.mainTexture = camStream;
		camStream.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
