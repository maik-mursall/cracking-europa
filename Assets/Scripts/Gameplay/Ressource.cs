using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "Ressource", menuName = "ScriptableObjects/Ressource", order = 1)]
    public class Ressource : ScriptableObject
    {
        public string ressourceName;
        public int amount;
        public GameObject prefab;
    }
}
