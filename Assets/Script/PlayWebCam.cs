using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayWebCam : MonoBehaviour {
	public WebCamTexture camStream;
    private WebCamDevice[] device;
	private Renderer myRender;
	private string monthVar;

	public IEnumerator CapturePNG()
	{
		yield return new WaitForEndOfFrame ();
		monthVar = System.DateTime.Now.ToString ("yyyyMMddhhmmss");
		Texture2D _TextureFromCamera = new Texture2D (camStream.width, camStream.height);
		_TextureFromCamera.SetPixels (camStream.GetPixels());
		_TextureFromCamera.Apply ();
		byte[] bytes = _TextureFromCamera.EncodeToPNG ();
		string filePath = Application.dataPath + "/Snap/SnapShot-"+ monthVar +".png";
		Debug.Log (filePath);
        myRender.material.mainTexture = _TextureFromCamera;
        camStream.Pause();
		System.IO.File.WriteAllBytes (filePath, bytes);
	}

	// Use this for initialization
	void Start () {
        device = WebCamTexture.devices;
		camStream = new WebCamTexture(device[0].name,1920,1080,30);
		Debug.Log ("Connected to " + device [0].name);
		Debug.Log (camStream.deviceName);
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
