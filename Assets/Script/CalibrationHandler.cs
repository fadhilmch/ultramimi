using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.SceneManagement;

public class CalibrationHandler : MonoBehaviour {
	[SerializeField]
	public GameObject[] sensorPlacement = new GameObject[(int)TouchSensor.Size];
	public static GameObject[] static_sensorPlacement = new GameObject[(int)TouchSensor.Size];
	char buff;

	// 0x0A is new line

	private bool start = true;
	private int sensorNum = (int)TouchSensor.Size;
	private int sensorIndex = 0;

	private float timer = 0f;
	private const float timerCount = 5f;


	void Start()
	{

		for (int i = 0; i < sensorNum; i++) {
			static_sensorPlacement [i] = sensorPlacement [i];
			sensorPlacement [i].SetActive (false);
		}
		// Initialize serial protocol

	}

	// Update is called once per frame
	void Update()
	{
		




	}
	void OnApplicationQuit()
	{
		
	}
}
