using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
public class PlayWebCam : MonoBehaviour {
	WebCamTexture camStream;
	Renderer myRender;

	IEnumerator CapturePNG()
	{
		yield return new WaitForEndOfFrame ();
		Texture2D _TextureFromCamera = new Texture2D (GetComponent<Renderer> ().material.mainTexture.width, GetComponent<Renderer> ().material.mainTexture.height);
		_TextureFromCamera.SetPixels ((GetComponent<Renderer> ().material.mainTexture as WebCamTexture).GetPixels ());
		_TextureFromCamera.Apply ();
		byte[] bytes = _TextureFromCamera.EncodeToPNG ();
		string filePath = "SavedPhoto.png";

		System.IO.File.WriteAllBytes (filePath, bytes);
	}

	// Use this for initialization
	void Start () {
		camStream = new WebCamTexture ();
		myRender = GetComponent<Renderer> ();
		myRender.material.mainTexture = camStream;
		camStream.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A))
		{
			StartCoroutine (CapturePNG ());
		}
	}
}
