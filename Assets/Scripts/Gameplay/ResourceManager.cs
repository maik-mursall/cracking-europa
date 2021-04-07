using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager instance;

        private readonly SortedDictionary<String, float> _resources = new SortedDictionary<string, float>();

        private void Start()
        {
            if (instance)
            {
                Destroy(gameObject);
            }

            instance = this;
        }

        public void addResource(string resourceName, float amount)
        {
            if (_resources.TryGetValue(resourceName, out float mAmount))
            {
                _resources[resourceName] = mAmount + amount;
            }
            else
            {
                _resources.Add(resourceName, amount);
            }
            
            Debug.Log($"Haha yes {_resources}");
        }
    }
}
