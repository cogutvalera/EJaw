using UnityEngine;

namespace EJaw
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 2)]
    public class EnemyScriptableObject : ScriptableObject
    {
        public float healthAmount;
        public float movingSpeed;
        public float damage;
        public int randCoinsMin = 5, randCoinsMax = 25;
    }
}
