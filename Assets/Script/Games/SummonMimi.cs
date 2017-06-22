using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMimi : MonoBehaviour {
    private Vector3[] summonCoordinate = new Vector3[]
    {
        new Vector3(-7.63f,  2.90f, -9f),
        new Vector3( 2.51f,  1.77f, -9f),
        new Vector3(-5.04f, -3.12f, -9f),
        new Vector3(-7.66f, -3.36f, -9f),
        new Vector3( 5.28f, -1.18f, -9f),
        new Vector3( 7.32f, -3.17f, -9f),
        new Vector3( 4.51f, -2.91f, -9f),
        new Vector3( 4.25f,  2.78f, -9f),
        new Vector3(-3.57f,  3.05f, -9f)
    };
    private float[] summonRotation =
        {
           -80.024f,
            68.778f,
           -55.741f,
           -86.719f,
             8.968f,
             1.194f,
             0.814f,
           -18.756f,
           - 3.729f
        };
    public float summonInterval = 5f;
    public bool[] sensorDummy;
    public GameObject mimiSample;

    private float counter;
    [SerializeField] private int[] coordinateToSensor = {0,4,7,11,10,12,13,19,21};
    private int summonrange = 9;
    private int currentPos;
    private int miminum;
    private GameObject summonedMimi;
    private void summonMimiAt(int sensorNum)
    {
        summonedMimi = Instantiate(mimiSample) as GameObject;
        summonedMimi.transform.position = (Vector3) summonCoordinate.GetValue(sensorNum);
        summonedMimi.transform.rotation = Quaternion.AngleAxis(summonRotation[sensorNum],Vector3.back);
        Debug.Log(summonedMimi.transform.rotation.eulerAngles.z);
        summonedMimi.name = "randomMimi";
        Destroy(summonedMimi, summonInterval);
    }

	// Use this for initialization
	void Start () {
        counter =-1;
        miminum = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.gameState == GameController.GameState.Play)
        {
            if(counter > 0f)
            {
                counter -= Time.deltaTime;
                if(sensorDummy[coordinateToSensor[currentPos]])
                {
                    PointSystem.pointStatus[miminum] = true;
                    miminum = miminum + 1;
                    Debug.Log(miminum);
                    counter = -1;
                    Destroy(summonedMimi);
                    if (miminum == 6)
                    {
                        GameController.gameState = GameController.GameState.Finished;
                        counter = 1;
                        Debug.Log("Game State : " + GameController.gameState);
                    }
                    sensorDummy[coordinateToSensor[currentPos]] = false;
                }
            }
            else
            {
                counter = summonInterval;
                currentPos = Random.Range(0, 8);
                summonMimiAt(currentPos);
                Debug.Log(coordinateToSensor[currentPos]);
            }
        }
	}
}
