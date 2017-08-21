using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace hideandseek
{

    public class Gameplay : MonoBehaviour
    {

        public  float timer;
        public  bool isStart = false;
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
		public GameObject loadingScreen;
        public int[] barScorevalue;
        public int score = 0;

        public AudioSource source;
        public AudioClip audio1;
        public AudioClip audio2;
        private bool audiostate1 = false;
        private bool audiostate2 = false;
        private bool audiostate3 = false;
        private bool audiostate4 = false;
        private bool audiostate5 = false;
        private bool audiostate6 = false;
        private bool audiostate7 = false;
		public	TimerAnimationController	timerAnimatorController;

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
			timerAnimatorController.StartTime ();

        }

        void Update()
        {

            if (SerialHandler.getSensorDown((int)TouchSensor.PrologSub) || Input.GetKey(KeyCode.Q))
            {
                OnSensorTap(0);
                if (audiostate1 == false)
                {
                    source.PlayOneShot(audio1);
                    audiostate1 = true;
                }
            }
            else
                audiostate1 = false;

            if (SerialHandler.getSensorDown((int)TouchSensor.RumahKanan) || Input.GetKey(KeyCode.W))
            {
                OnSensorTap(1);
                if (audiostate2 == false)
                {
                    source.PlayOneShot(audio1);
                    audiostate2 = true;
                }
            }
            else
                audiostate2 = false;

            if (SerialHandler.getSensorDown((int)TouchSensor.Factory) || Input.GetKey(KeyCode.E))
            {
                OnSensorTap(2);
                if (audiostate3 == false)
                {
                    source.PlayOneShot(audio1);
                    audiostate3 = true;
                }
            }
            else
                audiostate3 = false;

            if (SerialHandler.getSensorDown((int)TouchSensor.Prolog) || Input.GetKey(KeyCode.R))
            {
                OnSensorTap(3);
                if (audiostate4 == false)
                {
                    source.PlayOneShot(audio1);
                    audiostate4 = true;
                }
            }
            else
                audiostate4 = false;

            if (SerialHandler.getSensorDown((int)TouchSensor.FarmBawah) || Input.GetKey(KeyCode.T))
            {
                OnSensorTap(4);
                if (audiostate5 == false)
                {
                    source.PlayOneShot(audio1);
                    audiostate5 = true;
                }
            }
            else
                audiostate5 = false;

            if (SerialHandler.getSensorDown((int)TouchSensor.Player1Kanan) || Input.GetKey(KeyCode.Y))
            {
                OnSensorTap(5);
                if (audiostate6 == false)
                {
                    source.PlayOneShot(audio1);
                    audiostate6 = true;
                }
            }
            else
                audiostate6 = false;

            if (SerialHandler.getSensorDown((int)TouchSensor.Store) || Input.GetKey(KeyCode.U))
            {
                OnSensorTap(6);
                if (audiostate7 == false)
                {
                    source.PlayOneShot(audio1);
                    audiostate7 = true;
                }
            }

            else
                audiostate7 = false;


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
			loadingScreen.GetComponent<loadingScreen>().levelToLoad = "GABUNG";
			loadingScreen.GetComponent<loadingScreen>().startLoading = true;
        }

        private IEnumerator GoToMenu()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("GABUNG");
        }

        public void GoToScene(string sceneName)
        {
			loadingScreen.GetComponent<loadingScreen>().levelToLoad = sceneName;
			loadingScreen.GetComponent<loadingScreen>().startLoading = true;
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