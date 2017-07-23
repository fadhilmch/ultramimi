using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour {
	public Transform tBackgroound;
	public int iJumlahBackground;
	public float fSpeedBackground;
	public float fGapBackground;
	public float rRecycleBackground;
	private List<Transform> lQueueBackground;

	public Transform tBackgroound1;
	public int iJumlahBackground1;
	public float fSpeedBackground1;
	public float fGapBackground1;
	public float rRecycleBackground1;
	private List<Transform> lQueueBackground1;

    public GamePlay gamePlay;

	void Start(){
		InstantiateRoad1 ();
		InstantiateRoad2 ();
	}

	void Update(){
		MoveRoad1 ();
		MoveRoad2 ();
	}

	public void InstantiateRoad1(){
		lQueueBackground = new List<Transform>(iJumlahBackground);

		for (int i = 0; i < iJumlahBackground; i++)
		{
			lQueueBackground.Add((Transform)Instantiate(tBackgroound));
		}

		for (int i = 0; i < iJumlahBackground; i++)
		{
			RecycleRoad1();
		}
	}

	public void InstantiateRoad2(){
		lQueueBackground1 = new List<Transform>(iJumlahBackground1);

		for (int i = 0; i < iJumlahBackground1; i++)
		{
			lQueueBackground1.Add((Transform)Instantiate(tBackgroound1));
		}

		for (int i = 0; i < iJumlahBackground1; i++)
		{
			RecycleRoad2();
		}
	}

	private void MoveRoad1(){
		if (!gamePlay.isStart || gamePlay.isGameOver) return;
		if (lQueueBackground[0].localPosition.y < rRecycleBackground)
		{
			RecycleRoad1();
		}

		GameObject[] background = GameObject.FindGameObjectsWithTag("road 1");

		foreach (GameObject go in background)
		{
			go.transform.Translate(fSpeedBackground * Vector3.down * Time.deltaTime);
		}
	}

	private void MoveRoad2(){
		if (!gamePlay.isStart || gamePlay.isGameOver) return;
        if (lQueueBackground1[0].localPosition.y < rRecycleBackground1)
		{
			RecycleRoad2();
		}

		GameObject[] background = GameObject.FindGameObjectsWithTag("road 2");

		foreach (GameObject go in background)
		{
			go.transform.Translate(fSpeedBackground1 * Vector3.down * Time.deltaTime);
		}
	}

	private void RecycleRoad1(){
		lQueueBackground[0].localPosition = new Vector2(tBackgroound.position.x, lQueueBackground[lQueueBackground.Count - 1].localPosition.y + (fGapBackground));
		lQueueBackground.Add(lQueueBackground[0]);
		lQueueBackground.RemoveAt(0);
	}

	private void RecycleRoad2(){
		lQueueBackground1[0].localPosition = new Vector2(tBackgroound1.position.x, lQueueBackground1[lQueueBackground.Count - 1].localPosition.y + (fGapBackground1));
		lQueueBackground1.Add(lQueueBackground1[0]);
		lQueueBackground1.RemoveAt(0);
	}
}
