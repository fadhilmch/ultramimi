using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCounterUI : MonoBehaviour {
    
    public float countTime = 6.3f;

    private float counter;
	// Use this for initialization
	void Start () {
        counter = countTime;
        gameObject.GetComponent<Text>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (PhotoController.photoState == PhotoController.PhotoState.Photo)
        {
            gameObject.GetComponent<Text>().enabled = true;
            counter -= Time.deltaTime;
            gameObject.GetComponent<Text>().text = (counter-1f).ToString("0");
            if(counter < 1f)
            {
                
                GameObject.Find("Canvas/CountDown/Flash").SetActive(true);
                gameObject.GetComponent<Text>().enabled = false;
                if(counter < 0f)
                {
                    GameObject.Find("WebCam").GetComponent<WebCamUI>().snap = true;
                    PhotoController.photoState = PhotoController.PhotoState.Share;
                    GameObject.Find("Canvas/CountDown/Flash").SetActive(false);

                }
                
            }
        }

	}
}
