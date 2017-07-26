using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace hideandseek
{

    public class Gameplay : MonoBehaviour
    {

        public float timer;
        public bool isStart = false;
        public bool isGameOver = false;
        public Text timerText;
        public bool[] pointActive;
        private bool isTap = false;
        private Player player;
        public GameObject panelAttention;
        public GameObject panelGameOver;
        public GameObject countDownManager;
        public RawImage[] iconFound;
        public Color[] colorIconFounds;
        public bool isChangeObject { get; set; }
        public Text[] barScore;
        public GameObject[] barScoreDone;
        public int[] barScorevalue;
        public int score = 0;

        public AudioSource source;
        public AudioClip audio1;
        public AudioClip audio2;

        void Start()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            StartCoroutine(HidePanelAttention());
            source = GetComponent<AudioSource>();
        }

        private IEnumerator HidePanelAttention()
        {
            yield return new WaitForSeconds(3);
            panelAttention.SetActive(false);
            countDownManager.SetActive(true);
            countDownManager.GetComponent<CountDownManager>().isCountDown = true;
            Invoke("StartGame", 5);
        }

        void StartGame()
        {
            isStart = true;
            GetComponent<SpawnerObjectManager>().StartGame();
            CancelInvoke("StartGame");
        }

        void Update()
        {
            
            if (SerialHandler.getSensorDown((int)TouchSensor.PrologSub) || Input.GetKeyDown(KeyCode.Q))
                OnSensorTap(0);
            if (SerialHandler.getSensorDown((int)TouchSensor.RumahKanan) || Input.GetKeyDown(KeyCode.W))
                OnSensorTap(1);
            if (SerialHandler.getSensorDown((int)TouchSensor.Factory) || Input.GetKeyDown(KeyCode.E))
                OnSensorTap(2);
            if (SerialHandler.getSensorDown((int)TouchSensor.Prolog) || Input.GetKeyDown(KeyCode.R))
                OnSensorTap(3);
            if (SerialHandler.getSensorDown((int)TouchSensor.FarmAtas) || Input.GetKeyDown(KeyCode.T))
                OnSensorTap(4);
            if (SerialHandler.getSensorDown((int)TouchSensor.Player1Kanan) || Input.GetKeyDown(KeyCode.Y))
                OnSensorTap(5);
            if (SerialHandler.getSensorDown((int)TouchSensor.Store) || Input.GetKeyDown(KeyCode.U))
                OnSensorTap(6);
                

            if (isStart && !isGameOver)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    timer = 0;
                    Gameover(false);
                }
                int timeInINteger = (int)timer;
                timerText.text = timeInINteger.ToString();
                //print("s");
            }
        }

        public void OnSensorTap(int indexSensor)
        {
            if (indexSensor == GetPointsActive())
            {
                isChangeObject = true;
                player.SetDie();
                source.PlayOneShot(audio1);
            }
            else
            {
            }
        }

        private int GetPointsActive()
        {
            int point = 0;
            for (int i = 0; i < pointActive.Length; i++)
            {
                if (pointActive[i] == true)
                {
                    point = i;
                }
            }
            return point;
        }

        public void ActiveDisactivePoints(int indexPoint)
        {
            for (int i = 0; i < pointActive.Length; i++)
            {
                if (i != indexPoint)
                {
                    pointActive[i] = false;
                }
                else
                {
                    pointActive[i] = true;
                }
            }
        }

        private void Gameover(bool state)
        {
            isGameOver = true;
            panelGameOver.SetActive(true);
            panelGameOver.GetComponent<PanelGameover>().SetGameoverCondition(state);
            GetComponent<SpawnerObjectManager>().CancelInvokeDisplayObject();
            StartCoroutine(GoToMenu());
        }

        private IEnumerator GoToMenu()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("GABUNG");
        }

        public void GoToScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ChangeValueBar(int index)
        {
            barScorevalue[index]--;
            barScore[index].text = barScorevalue[index].ToString();
            if (barScorevalue[index] <= 0)
            {
                barScoreDone[index].SetActive(true);
                source.PlayOneShot(audio2,5f);
            }
            score++;
            if (score >= 8)
            {
                Gameover(true);
            }
        }
    }
}