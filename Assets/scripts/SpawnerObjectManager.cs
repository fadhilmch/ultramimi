using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace hideandseek
{
    public class SpawnerObjectManager : MonoBehaviour
    {

        public GameObject[] pointSpawnerObjects;
        private Gameplay gamePlay;
        public GameObject player;
        private int lastIndexPoints = 0;

        void Start()
        {
            gamePlay = GetComponent<Gameplay>();
            gamePlay.isChangeObject = true;
        }

        public void StartGame()
        {
            InvokeRepeating("DisplayObject", 0, 3);
        }

        private void DisplayObject()
        {
            if (!gamePlay.isGameOver && gamePlay.isStart)
            {
                int indexPoint = GenerateIndexPoints();
                if (indexPoint == lastIndexPoints)
                {
                    indexPoint = GenerateIndexPoints();
                }
                else
                {
                    lastIndexPoints = indexPoint;
                }
                Vector3 newPosition = pointSpawnerObjects[indexPoint].transform.position;
                if (indexPoint == 3 || indexPoint == 4 || indexPoint == 6 || indexPoint == 2)
                    player.transform.localScale = new Vector3(-1, 1, 1);
                else
                    player.transform.localScale = new Vector3(1, 1, 1);
    
                player.transform.position = newPosition;
                gamePlay.ActiveDisactivePoints(indexPoint);
                if (gamePlay.isChangeObject)
                {
                    player.GetComponent<Player>().ChangeObject();
                    gamePlay.isChangeObject = false;
                }
            }
            else
            {
                CancelInvokeDisplayObject();
            }
        }

        private int GenerateIndexPoints()
        {
            int indexPoints = UnityEngine.Random.Range(0, pointSpawnerObjects.Length);
            return indexPoints;
        }

        public void CancelInvokeDisplayObject()
        {
            CancelInvoke("DisplayObject");
        }
    }
}
