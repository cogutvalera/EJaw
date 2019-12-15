using UnityEngine;
using UnityEngine.UI;

namespace EJaw
{
    public class SellButton : MonoBehaviour
    {
        public void ActionYes()
        {
            TowerDefence.Instance.towerBuildingSellMenu.SetActive(false);

            TowerDefence.Instance.balance += transform.parent.parent.GetComponentInChildren<Tower>().towerData.buildPrice;

            DestroyImmediate(transform.parent.parent.GetComponent<SlotButton>().tower);
            transform.parent.parent.GetComponent<SlotButton>().tower = null;

            transform.parent.parent.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        public void ActionNo()
        {
            TowerDefence.Instance.towerBuildingSellMenu.SetActive(false);
        }
    }
}