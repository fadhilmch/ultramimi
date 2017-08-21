using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour {
    public int position = 0;
    public int samplerate = 44100;
    public float frequency = 200;

	// Use this for initialization
	void Start () {
        AudioClip myClip = AudioClip.Create("Sinusoid", samplerate * 2, 1, samplerate, true, OnAudioRead);
        AudioSource aud = GetComponent<AudioSource>();
        aud.clip = myClip;
        aud.Play();
	}

    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * frequency * position / samplerate));
            position++;
            count++;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
