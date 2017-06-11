using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anakMove : MonoBehaviour
{
    private Controller controller;
    private float speed = 3f;
    public Transform startMarker;
    public Transform endMarker;
    public Transform restartMarker;
    private float distance;
    private Color start;
    private Color end;
    private float t = 0f;
    private int xMove = 0;
    public int state = 0;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<Controller>();
        //yMove = Random.Range(-7, 7);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.anak == false)
        {
            transform.position = startMarker.position;
            t = 0f;
            xMove = Random.Range(5, 23);
        }

        if (controller.anak == true)
        {
            if (state == 0)
            {
                endMarker.position = new Vector3(xMove, endMarker.position.y, endMarker.position.z);
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(startMarker.position, endMarker.position, 0.5f*t * speed);
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
                transform.position = Vector3.Lerp(restartMarker.position, startMarker.position, t*0.75f);
                if (transform.position == startMarker.position)
                {
                    state = 0;
                    controller.anak = false;
                }
            }
        }
    }
}




