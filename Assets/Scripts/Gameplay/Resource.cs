using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resource", order = 1)]
    public class Resource : ScriptableObject
    {
        public string resourceName;
        public int amount;
        public GameObject prefab;
    }
}
