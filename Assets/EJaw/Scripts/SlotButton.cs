using UnityEngine;

namespace EJaw
{
    public class SlotButton : MonoBehaviour
    {
        public GameObject tower;

        public void Action()
        {
            if (null == tower) // slot is empty then we can place tower
            {
                // We need to fully reset parent by null before changing parent in order to be always on top
                TowerDefence.Instance.towerBuildingMenu.transform.parent = null;
                TowerDefence.Instance.towerBuildingMenu.transform.parent = gameObject.transform;
                TowerDefence.Instance.towerBuildingMenu.transform.localPosition = new Vector3(0, 35, -5);

                TowerDefence.Instance.towerBuildingSellMenu.gameObject.SetActive(false);
                TowerDefence.Instance.towerBuildingMenu.SetActive(true);
            }
            else // slot is not empty then we can sell tower
            {
                // We need to fully reset parent by null before changing parent in order to be always on top
                TowerDefence.Instance.towerBuildingSellMenu.transform.parent = null;
                TowerDefence.Instance.towerBuildingSellMenu.transform.parent = gameObject.transform;
                TowerDefence.Instance.towerBuildingSellMenu.transform.localPosition = new Vector3(0, -20, -5);

                TowerDefence.Instance.towerBuildingMenu.SetActive(false);
                TowerDefence.Instance.towerBuildingSellMenu.gameObject.SetActive(true);
            }
        }
    }
}