using UnityEngine;

namespace EJaw
{
    public class Bomb : MonoBehaviour
    {
        public Tower tower;
        public Enemy enemy;

        private float _distance;

        void FixedUpdate()
        {
            if (TowerDefence.States.Play != TowerDefence.Instance.state)
            {
                return;
            }

            if (null == enemy || null == enemy.transform)
            {
                DestroyImmediate(gameObject);
                return;
            }

            _distance = TowerDefence.Distance2D(transform.localPosition, enemy.transform.localPosition);

            if (_distance < TowerDefence.Instance.delta * 2)
            {
                enemy.Damage(tower.towerData.damage);
                DestroyImmediate(gameObject);

                return;
            }

            transform.localPosition = new Vector3(
                Mathf.Lerp(
                    transform.localPosition.x, 
                    enemy.transform.localPosition.x, 
                    enemy.enemyData.movingSpeed * Time.fixedDeltaTime * TowerDefence.Instance.koefficientEnemySpeed * 5
                ),
                Mathf.Lerp(
                    transform.localPosition.y, 
                    enemy.transform.localPosition.y, 
                    enemy.enemyData.movingSpeed * Time.fixedDeltaTime * TowerDefence.Instance.koefficientEnemySpeed * 5
                ),
                -5
            );
        }
    }
}