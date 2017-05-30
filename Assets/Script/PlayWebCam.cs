using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayWebCam : MonoBehaviour {
	public WebCamTexture camStream;
    WebCamDevice[] device;
	Renderer myRender;

	public IEnumerator CapturePNG()
	{
		yield return new WaitForEndOfFrame ();
		
		Texture2D _TextureFromCamera = new Texture2D (GetComponent<Renderer> ().material.mainTexture.width, GetComponent<Renderer> ().material.mainTexture.height);
		_TextureFromCamera.SetPixels ((GetComponent<Renderer> ().material.mainTexture as WebCamTexture).GetPixels ());
		_TextureFromCamera.Apply ();
		byte[] bytes = _TextureFromCamera.EncodeToPNG ();
		string filePath = Application.dataPath + "/SnapShot.png";
        myRender.material.mainTexture = _TextureFromCamera;
        camStream.Pause();
		System.IO.File.WriteAllBytes (filePath, bytes);
	}

	// Use this for initialization
	void Start () {
        device = WebCamTexture.devices;
		camStream = new WebCamTexture(device[device.Length-1].name);
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
