using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EJaw
{
    public class TowerButton : MonoBehaviour
    {
        public GameObject towerPrefab;

        private TextMeshProUGUI _text;

        void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Action()
        {
            TowerDefence.Instance.towerBuildingMenu.SetActive(false);

            if (TowerDefence.Instance.balance < towerPrefab.GetComponent<Tower>().towerData.buildPrice)
            {
                TowerDefence.Instance.gameMessage.transform.parent.gameObject.SetActive(true);
                TowerDefence.Instance.gameMessage.text = "not enough balance";
                Invoke("DisableGameMessage", 1.0f);
                return;
            }

            TowerDefence.Instance.balance -= towerPrefab.GetComponent<Tower>().towerData.buildPrice;

            GameObject tower;
            tower = Instantiate<GameObject>(towerPrefab);
            tower.transform.parent = transform.parent.parent;
            tower.transform.localPosition = new Vector3(0, 0, -5);
            tower.transform.localScale = new Vector3(1.5f, 1.5f, 1);

            transform.parent.parent.GetComponent<SlotButton>().tower = tower;
            transform.parent.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        void DisableGameMessage()
        {
            TowerDefence.Instance.gameMessage.transform.parent.gameObject.SetActive(false);
        }

        void Update()
        {
            _text.text = towerPrefab.GetComponent<Tower>().towerData.buildPrice.ToString() + "$";
        }
    }
}