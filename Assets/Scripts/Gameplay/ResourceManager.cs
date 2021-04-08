using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager instance;

        private readonly Dictionary<String, ResourceUI> _resources = new Dictionary<string, ResourceUI>();

        [SerializeField] private GameObject resourceUI;
        [SerializeField] private Transform resourceUIParent;

        private void Start()
        {
            if (instance)
            {
                Destroy(gameObject);
            }

            instance = this;
        }
        
        public void AddResource(string resourceName, float amount)
        {
            if (_resources.TryGetValue(resourceName, out ResourceUI mResourceUI))
            {
                _resources[resourceName].Amount = mResourceUI.Amount + amount;
            }
            else
            {
                var createdResourceUI = Instantiate(resourceUI, resourceUIParent).GetComponent<ResourceUI>();

                createdResourceUI.SetResourceName(resourceName);
                createdResourceUI.Amount = amount;
                
                _resources.Add(resourceName, createdResourceUI);
            }
        }
    }
}
