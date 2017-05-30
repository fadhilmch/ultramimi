using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jepret : MonoBehaviour {
    public AudioClip sound;
    private AudioSource source;



    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("QUIZ  (foto)").GetComponent<Photo>().captureWebcam == true)
        {
            source.PlayOneShot(sound, 0.5f);
            Debug.Log("mss");    
        }
    }
}
