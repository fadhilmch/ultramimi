using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WebCamUI : MonoBehaviour {
    public WebCamTexture camStream;
    public Texture2D frameImage;
    public bool snap = false;
    private WebCamDevice[] device;
    private RawImage myRender;
    private Texture2D displayImage;
    private string monthVar;
    
    public IEnumerator CapturePNG()
    {
        yield return new WaitForEndOfFrame();
        monthVar = System.DateTime.Now.ToString("yyyyMMddhhmmss");
        Texture2D _TextureFromCamera = new Texture2D(1920, 1080);
        _TextureFromCamera.SetPixels(camStream.GetPixels());
        _TextureFromCamera = _TextureFromCamera.AlphaBlend(frameImage);
        _TextureFromCamera.Apply();
        byte[] bytes = _TextureFromCamera.EncodeToPNG();
        string filePath = Application.dataPath + "/Snap/SnapShot-" + monthVar + ".png";
        Debug.Log(filePath);
        myRender.material.mainTexture = _TextureFromCamera;
        GameObject.Find("Canvas/WebCamPhoto").SetActive(false);
        GameObject.Find("Canvas/WebCamShare").SetActive(true);
        GameObject.Find("Canvas/Text").GetComponent<Text>().text = "TERIMAKASIH TELAH\nBERKUNJUNG KE BOOTH KAMI";
        camStream.Stop();
        System.IO.File.WriteAllBytes(filePath, bytes);
    }

    // Use this for initialization
    void Start () {
        device = WebCamTexture.devices;
        camStream = new WebCamTexture(device[0].name, 1920, 1080, 30);
        Debug.Log("Connected to " + device[0].name);
        Debug.Log(camStream.deviceName);
        myRender = GetComponent<RawImage>();
        displayImage = new Texture2D(1920, 1080);
        myRender.material.mainTexture = camStream;

        camStream.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A) || snap)
        {
            StartCoroutine(CapturePNG());
        }
        snap = false;
    }
}
