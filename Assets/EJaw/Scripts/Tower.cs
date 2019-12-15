using UnityEngine;
using System.Collections.Generic;

namespace EJaw
{
    public class Tower : MonoBehaviour
    {
        public TowerScriptableObject towerData;

        private List<GameObject> _list;
        private GameObject _obj, _bombObj;
        private Vector3 _pos;
        private float _dist, _tmpDist;
        private float _lastTimeShoot;
        private Bomb _bomb;

        void Awake()
        {
            _list = new List<GameObject>();
        }

        public void Shoot()
        {
            _list.Clear();

            foreach(GameObject enemy in TowerDefence.Instance.enemies)
            {
                if (TowerDefence.Distance2D(enemy.transform.position, transform.position) < towerData.range)
                {
                    _list.Add(enemy);
                }
            }

            _obj = null;
            _dist = float.MaxValue;
            foreach(GameObject enemy in _list)
            {
                _pos = TowerDefence.Instance.enemyWavePath[TowerDefence.Instance.enemyWavePath.Length - 1].position;
                _tmpDist = TowerDefence.Distance2D(enemy.transform.position, _pos);
                if (_tmpDist < _dist)
                {
                    _obj = enemy;
                    _dist = _tmpDist;
                }
            }

            if (null == _obj)
            {
                return;
            }

            _bombObj = Instantiate<GameObject>(TowerDefence.Instance.bombPrefab);
            _bombObj.transform.parent = TowerDefence.Instance.bombsRoot;
            _bombObj.transform.position = new Vector3(transform.position.x, transform.position.y, _bombObj.transform.position.z);
            _bombObj.transform.localScale = Vector3.one;

            _bomb = _bombObj.GetComponent<Bomb>();
            _bomb.enemy = _obj.GetComponent<Enemy>();
            _bomb.tower = this;
        }

        void FixedUpdate()
        {
            if (TowerDefence.States.Play != TowerDefence.Instance.state)
            {
                return;
            }

            if (Time.timeSinceLevelLoad < _lastTimeShoot + towerData.shootInterval)
            {
                return;
            }

            _lastTimeShoot = Time.timeSinceLevelLoad;

            Shoot();
        }
    }
}