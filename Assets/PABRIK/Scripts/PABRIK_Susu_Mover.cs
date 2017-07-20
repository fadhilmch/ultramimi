using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PABRIK_Susu_Mover : MonoBehaviour
{

    public Transform[] Points;

    public Transform[] Objects;
    public float distanceTolerance = 0.001f;

    List<Coroutine> MovingRoutineList = new List<Coroutine>();

    Coroutine startMovingRoutine = null;

    public void StartMoving()
    {
        if (startMovingRoutine != null)
        {
            StopCoroutine(startMovingRoutine);
        }

        for (int i = 0; i < MovingRoutineList.Count; i++)
        {
            StopCoroutine(MovingRoutineList[i]);
        }

        MovingRoutineList.Clear();

        for (int i = 0; i < Objects.Length; i++)
        {
            Transform obj = Objects[i];
            obj.position = Points[0].position;
        }

        startMovingRoutine = StartCoroutine(StartMovingRoutine());
    }

    IEnumerator StartMovingRoutine()
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            Transform obj = Objects[i];
            Coroutine movingRoutine = StartCoroutine(MovingRoutine(obj, Points));
            MovingRoutineList.Add(movingRoutine);
            yield return new WaitForSeconds(0.75f);
        }
    }


    IEnumerator MovingRoutine(Transform movingObject, Transform[] movingPoints)
    {
        int currentPoint = 0;
        while (true)
        {
            yield return null;
            float xPos = movingObject.position.x - (movingObject.position.x - movingPoints[currentPoint].position.x) / 30;
            float yPos = movingObject.position.y - (movingObject.position.y - movingPoints[currentPoint].position.y) / 30;
            movingObject.transform.position = new Vector2(xPos, yPos);
            if (Vector2.Distance(movingObject.position, movingPoints[currentPoint].position) <= distanceTolerance)
            {
                currentPoint++;
                if (currentPoint >= movingPoints.Length)
                {
                    currentPoint = 0;
                    movingObject.transform.position = new Vector2(movingPoints[currentPoint].position.x, movingPoints[currentPoint].position.y);
                    //snap
                }
            }

        }
    }

    // Use this for initialization
    void Start()
    {

    }


    public bool DoMove = false;

    // Update is called once per frame
    void Update()
    {
        if (DoMove)
        {
            DoMove = false;
            StartMoving();
        }
    }
}
