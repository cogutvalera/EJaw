using UnityEngine;

namespace EJaw
{
    public class Enemy : MonoBehaviour
    {
        public float health;
        public EnemyScriptableObject enemyData;

        private Vector2 _deltaRandom;
        private int _currentIndex;
        private float _distance;
        private GameObject _obj;
        private Bomb _bomb;

        public void Init()
        {
            health = enemyData.healthAmount;

            _currentIndex = 0;
            transform.localPosition = new Vector3(
                TowerDefence.Instance.enemyWavePath[_currentIndex].localPosition.x,
                TowerDefence.Instance.enemyWavePath[_currentIndex].localPosition.y,
                transform.position.z
            );
            _deltaRandom = Vector3.zero;
        }

        public void Damage(float damage)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;

                TowerDefence.Instance.balance += Random.Range(enemyData.randCoinsMin, enemyData.randCoinsMax);

                if (1 == TowerDefence.Instance.enemies.Count && TowerDefence.Instance.enemies[0] == gameObject)
                {
                    if (TowerDefence.Instance.currentWave == TowerDefence.Instance.enemyWaves.Length + 1)
                    {
                        TowerDefence.Instance.state = TowerDefence.States.PlayerWin;
                        TowerDefence.Instance.gameMessage.text = "YOU WIN";
                        TowerDefence.Instance.gameMessage.color = Color.green;
                        TowerDefence.Instance.gameMessage.transform.parent.gameObject.SetActive(true);
                    }
                }

                Finish();
            }
        }

        void DoDeltaRandom()
        {
            _deltaRandom = new Vector2(
                Random.Range(0.5f, 1.5f) * TowerDefence.Instance.delta * (Random.value > 0.5f ? 1 : -1),
                Random.Range(0.5f, 1.5f) * TowerDefence.Instance.delta * (Random.value < 0.5f ? 1 : -1)
            );
        }

        void FixedUpdate()
        {
            if (TowerDefence.States.Play != TowerDefence.Instance.state)
            {
                return;
            }

            _distance = TowerDefence.Distance2D(
                transform.localPosition, 
                TowerDefence.Instance.enemyWavePath[_currentIndex].localPosition + new Vector3(_deltaRandom.x, _deltaRandom.y, 0)
            );

            if (_distance < TowerDefence.Instance.delta)
            {
                if (_currentIndex < TowerDefence.Instance.enemyWavePath.Length - 1)
                {
                    _currentIndex++;
                    DoDeltaRandom();
                }
                else
                {
                    TowerDefence.Instance.health -= enemyData.damage;
                    if (TowerDefence.Instance.health <= 0)
                    {
                        TowerDefence.Instance.health = 0;
                        TowerDefence.Instance.state = TowerDefence.States.PlayerLose;
                        TowerDefence.Instance.gameMessage.text = "GAME OVER";
                        TowerDefence.Instance.gameMessage.color = Color.red;
                        TowerDefence.Instance.gameMessage.transform.parent.gameObject.SetActive(true);
                    }

                    Finish();

                    return;
                }
            }

            transform.localPosition = new Vector3(
                Mathf.Lerp(
                    transform.localPosition.x, 
                    TowerDefence.Instance.enemyWavePath[_currentIndex].localPosition.x + _deltaRandom.x, 
                    enemyData.movingSpeed * Time.fixedDeltaTime * TowerDefence.Instance.koefficientEnemySpeed
                ),
                Mathf.Lerp(
                    transform.localPosition.y, 
                    TowerDefence.Instance.enemyWavePath[_currentIndex].localPosition.y + _deltaRandom.y, 
                    enemyData.movingSpeed * Time.fixedDeltaTime * TowerDefence.Instance.koefficientEnemySpeed
                ),
                -5
            );
        }

        void Finish()
        {
            TowerDefence.Instance.enemies.Remove(gameObject);
            DestroyImmediate(gameObject);
        }
    }
}