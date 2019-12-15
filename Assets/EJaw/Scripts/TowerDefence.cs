using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace EJaw
{
    public class TowerDefence : MonoBehaviour
    {
        public enum States { Init, Play, Pause, PlayerWin, PlayerLose };
        public States state = States.Init;

        public float balance = 200;
        public float health = 100;
        public int currentWave = 1;
        public float delta = 10;
        public float koefficientEnemySpeed = 1.0f;

        public GameObject towerBuildingMenu;
        public GameObject towerBuildingSellMenu;

        public TextMeshProUGUI textBalance;
        public TextMeshProUGUI textHealth;
        public TextMeshProUGUI textWave;

        public GameObject panelPause;
        public TextMeshProUGUI gameMessage;

        public EnemyWaveScriptableObject[] enemyWaves;
        public Transform[] enemyWavePath;
        public Transform enemiesRoot;
        public GameObject[] enemyPrefabs;

        public Transform bombsRoot;
        public GameObject bombPrefab;

        public List<GameObject> enemies;

        private float lastTime;

        private GameObject _tmpObj;
        private int _i;

        private static TowerDefence _instance;
        public static TowerDefence Instance
        {
            get
            {
                return _instance;
            }
        }

        void Awake()
        {
            _instance = this;
            enemies = new List<GameObject>();
        }

        public void BackgroundAction()
        {
            if (state == States.Play)
            {
                towerBuildingMenu.gameObject.SetActive(false);
                towerBuildingSellMenu.gameObject.SetActive(false);
            }
        }

        public void Restart()
        {
            if (state == States.Play)
            {
                gameMessage.transform.parent.gameObject.SetActive(false);
                return;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ActionPause()
        {
            state = States.Pause;
            panelPause.SetActive(true);
        }

        public void ActionPlay()
        {
            state = States.Play;
            panelPause.SetActive(false);
        }

        void Update()
        {
            UpdateText();
        }

        void UpdateText()
        {
            textBalance.text = balance.ToString();
            textHealth.text = health.ToString();
            textWave.text = (currentWave - 1).ToString() + " / " + enemyWaves.Length.ToString();
        }

        void FixedUpdate()
        {
            if (States.Play == state)
            {
                FixedUpdatePlay();
            }
        }

        void FixedUpdatePlay()
        {
            if (currentWave <= enemyWaves.Length && Time.timeSinceLevelLoad <= lastTime + enemyWaves[currentWave - 1].duration)
            {
                return;
            }

            currentWave++;
            if (currentWave > enemyWaves.Length)
            {
                currentWave = enemyWaves.Length + 1;

                return;
            }

            lastTime = Time.timeSinceLevelLoad;

            for (_i = 0; _i < enemyWaves[currentWave - 1].enemisCount; _i++)
            {
                _tmpObj = Instantiate<GameObject>(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
                _tmpObj.transform.parent = enemiesRoot;
                _tmpObj.transform.localScale = Vector3.one;
                _tmpObj.GetComponent<Enemy>().Init();
                enemies.Add(_tmpObj);
            }
        }

        public static float Distance2D(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(
                new Vector3(a.x, a.y, 0),
                new Vector3(b.x, b.y, 0)
            );
        }
    }
}