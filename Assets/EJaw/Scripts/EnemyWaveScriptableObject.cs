using UnityEngine;

namespace EJaw
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "ScriptableObjects/EnemyWave", order = 3)]
    public class EnemyWaveScriptableObject : ScriptableObject
    {
        public float duration;
        public int enemisCount;
    }
}
