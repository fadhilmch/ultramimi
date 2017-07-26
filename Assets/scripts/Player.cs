using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace hideandseek
{
    public class Player : MonoBehaviour
    {
        public bool[] charsType;
        public Sprite[] sprites;
        public SpriteRenderer spriteRenderer;
        private ScoreManager scoreManager;
        public Gameplay gamePlay;
        public int currentIndexType = 0;
        private int indexSpriteKarakter = 0;

        void Start()
        {
            scoreManager = GameObject.Find("scores").GetComponent<ScoreManager>();
            gamePlay = GameObject.Find("Main Camera").GetComponent<Gameplay>();
        }

        public void SetDie()
        {
            if (!charsType[currentIndexType]) return;
            gameObject.transform.position = new Vector3(90, 90, 90);
            switch (currentIndexType)
            {
                case 0:
                    gamePlay.ChangeValueBar(0);
                    break;
                case 1:
                    gamePlay.ChangeValueBar(1);
                    break;
                case 2:
                    gamePlay.ChangeValueBar(2);
                    break;
                case 3:
                    gamePlay.ChangeValueBar(3);
                    break;
                case 4:
                    gamePlay.ChangeValueBar(0);
                    break;
                case 5:
                    gamePlay.ChangeValueBar(1);
                    break;
                case 6:
                    gamePlay.ChangeValueBar(2);
                    break;
                case 7:
                    gamePlay.ChangeValueBar(3);
                    break;
            }
            charsType[currentIndexType] = false;
            gamePlay.iconFound[currentIndexType].GetComponent<RawImage>().color = gamePlay.colorIconFounds[0];
        }

        public void ChangeObject()
        {
            int numRand = UnityEngine.Random.Range(0, charsType.Length);
            if (charsType[numRand] == false)
            {
                ChangeObject();
            }
            else
            {
                currentIndexType = numRand;
                spriteRenderer.sprite = sprites[numRand];
                /*
                if (numRand == 0 || numRand == 2 || numRand == 4 || numRand == 6)
                {
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }*/
            }
            indexSpriteKarakter = numRand;
        }

        public int getPlayerSpriteIndex()
        {
            return indexSpriteKarakter;
        }
    }
}