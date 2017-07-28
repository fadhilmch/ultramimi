using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonMove : MonoBehaviour
{
    public float speed = 1f;
    public Interaction interaction;
    public Transform startMarker;
    public Transform endMarker;
    public Transform restartMarker;
    private float distance;
    private float t = 0f;
    private int xMove = 0;
    public int state = 0;

    private AudioSource source;
    public AudioClip audio1;
    public float volume =1;

    public GameObject awan;
	public GameObject sign;
    private Animator animator;

    private bool play = false;

    void ReadInput()
    {
        //interaction.value = (Input.GetKeyDown(interaction.keyCode));
        //|| (SerialHandler.serial_is_open && SerialHandler.getSensorDown((int)interaction.sensorTrigger1));
        if ((Input.GetKeyDown(interaction.keyCode)) || SerialHandler.getSensorDown((int)interaction.sensorTrigger1))
            interaction.value = true;
    }

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        animator = awan.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
        Debug.Log(interaction.value);
        if (interaction.value == false)
        {
            transform.position = startMarker.position;
            t = 0f;
            xMove = Random.Range(-15, 15);
			sign.SetActive (true);
        }

        if (interaction.value == true)
        {
            if (play == false)
            {
                source.PlayOneShot(audio1, volume);
                //Debug.Log("a");
                animator.SetTrigger("burst");
				sign.SetActive (false);
                play = true;
            }
            if (state == 0)
            {
                
                endMarker.position = new Vector3(xMove, endMarker.position.y, endMarker.position.z);
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(startMarker.position, endMarker.position, 0.5f * t * speed);
                if (transform.position.y == endMarker.position.y)
                {
                    state = 1;
                    t = 0;
                    transform.position = restartMarker.position;
                }
            }
            if (state == 1)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(restartMarker.position, startMarker.position, t * 0.75f);
                if (transform.position == startMarker.position)
                {
                    state = 0;
                    interaction.value = false;
                    play = false;
                }
            }
        }
    }
}





