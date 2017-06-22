using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayWebCam : MonoBehaviour {
	public WebCamTexture camStream;
    public Texture2D frameImage;
    private WebCamDevice[] device;
	private Renderer myRender;
    private Texture2D displayImage;
	private string monthVar;
    private Texture2D _TextureFromCamera = new Texture2D(1920, 1080);
    private Texture2D _savedTexture = new Texture2D(1920, 1080);

    public IEnumerator CapturePNG()
	{
		yield return new WaitForEndOfFrame ();
		monthVar = System.DateTime.Now.ToString ("yyyyMMddhhmmss");
        _TextureFromCamera.SetPixels (camStream.GetPixels());
        _savedTexture = _TextureFromCamera.AlphaBlend(frameImage);
        _TextureFromCamera.Apply();
        byte[] bytes = _savedTexture.EncodeToPNG ();
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
        displayImage = new Texture2D(1920, 1080);
        myRender.material.mainTexture = camStream;

        camStream.Play ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
		{
			StartCoroutine (CapturePNG ());
		}
	}
}
