using UnityEngine;

namespace EJaw
{
    [CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower", order = 1)]
    public class TowerScriptableObject : ScriptableObject
    {
        public float buildPrice;
        public float range;
        public float shootInterval;
        public float damage;
    }
}
