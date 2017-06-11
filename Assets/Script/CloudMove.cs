using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {

    public AudioClip sound;
    private AudioSource source;
    private Controller controller;

    private float speed = 1f;
    public Transform startMarker;
    public Transform endMarker;
    private float distance;
    private SpriteRenderer spriterenderer;
    private Color start;
    private Color end;
    private float t = 0f;
    private int yMove=0;
    private int state = 0;


    private bool audiostate = false;
    private bool laststate = false;


    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        controller = GetComponent<Controller>();
        spriterenderer = GetComponent<SpriteRenderer>();
        start = spriterenderer.color;
        //start = new Color(start.r, start.g, start.b, 1f);
        end = new Color(start.r, start.g, start.b, -1.0f);
        //yMove = Random.Range(-7, 7);


    }
	
	// Update is called once per frame
	void Update () 
    {
        if (controller.anak == false)
        {
            spriterenderer.material.color = start;
            transform.position = startMarker.position;
            t = 0f;
            yMove = Random.Range(-7, 10);
            state = 0;
            laststate = false;
        }

        if (controller.anak == true)
        {
            endMarker.position = new Vector3(endMarker.position.x, yMove, endMarker.position.z);
            t += Time.deltaTime;
            if (laststate == false)
                audiostate = true;
            laststate = true;
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, t * speed);
            spriterenderer.material.color = Color.Lerp(start, end, t/2);
            if (spriterenderer.material.color.a == -1.0f)
            {
                transform.position = startMarker.position;
                spriterenderer.material.color = Color.Lerp(end, start, t/3);
                if (spriterenderer.material.color.a == 1f)
                {
                    controller.anak = false;
                }
            }
                     
        }

        if (audiostate == true)
        {
            source.PlayOneShot(sound, 2f);
            audiostate = false;
        }



    }

     
	
}
