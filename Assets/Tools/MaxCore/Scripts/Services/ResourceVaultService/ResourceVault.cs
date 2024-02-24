using System;
using System.Collections.Generic;
using System.Linq;
using Tools.MaxCore.Scripts.Project.DI;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.DataHubService.Data;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.ResourceVaultService
{
    public class ResourceVault : MonoBehaviour, IProjectInitializable
    {
        private Dictionary<ResourceType, Resource<int>> resourcesMapInt;
        private Dictionary<ResourceType, Resource<float>> resourcesMapFloat;

        public event Action<ResourceType, int> OnResourceChanged;
        public event Action<ResourceType, float> OnFloatResourceChanged;
        
        [Inject] private DataHub dataHub;
        private ResourceData resourcesData;

        public void Initialize()
        {
            resourcesData = dataHub.LoadData<ResourceData>(DataType.Resource);
            InitializeResources();

            OnResourceChanged += SaveResource;
            OnFloatResourceChanged += SaveResource;
        }

        private void SaveResource(ResourceType type, float value)
        {
            resourcesData.MapFloat[type] = value;
            dataHub.SaveData(DataType.Resource);
        }

        private void SaveResource(ResourceType type, int value)
        {
            resourcesData.MapInt[type] = value;
            dataHub.SaveData(DataType.Resource);
        }


        private void InitializeResources()
        {
            var resources = resourcesData.MapInt.Select(r => new Resource<int>(r.Key, r.Value)).ToArray();
            var resourcesFloat = resourcesData.MapFloat.Select(r => new Resource<float>(r.Key, r.Value)).ToArray();

            InitializeInt(resources);
            InitializeFloat(resourcesFloat);
        }

        private void InitializeInt(Resource<int>[] resources)
        {
            resourcesMapInt = resources.ToDictionary(r => r.Type);

            SubscribeAllResourcesInt();
        }

        private void InitializeFloat(Resource<float>[] resources)
        {
            resourcesMapFloat = resources.ToDictionary(r => r.Type);

            SubscribeAllResourcesFloat();
        }
        
        public void AddResource(ResourceType type, int value)
        {
            var resource = resourcesMapInt[type];
            resource.Amount += value;
        }

        public void SpendResource(ResourceType type, int value)
        {
            var resource = resourcesMapInt[type];
            resource.Amount -= value;
        }
        
        public void SetResource(ResourceType type, int value)
        {
            var resource = resourcesMapInt[type];
            resource.Amount = value;
        }

        public bool IsEnoughResource(ResourceType type, int value)
        {
            return resourcesMapInt[type].Amount >= value;
        }

        public int GetResourceAmount(ResourceType type)
        {
            return resourcesMapInt[type].Amount;
        }
        
        public void AddResource(ResourceType type, float value)
        {
            var resource = resourcesMapFloat[type];
            resource.Amount += value;
        }

        public void SpendResource(ResourceType type, float value)
        {
            var resource = resourcesMapFloat[type];
            resource.Amount -= value;
        }

        public bool IsEnoughResource(ResourceType type, float value)
        {
            return resourcesMapFloat[type].Amount >= value;
        }

        public float GetFloatResourceAmount(ResourceType type)
        {
            return resourcesMapFloat[type].Amount;
        }

        private void SubscribeAllResourcesInt()
        {
            foreach (var resource in resourcesMapInt.Values)
            {
                resource.OnChanged += delegate(int newValue) { OnResourceChanged?.Invoke(resource.Type, newValue); };
            }
        }

        private void SubscribeAllResourcesFloat()
        {
            foreach (var resource in resourcesMapFloat.Values)
            {
                resource.OnChanged += delegate(float newValue) { OnFloatResourceChanged?.Invoke(resource.Type, newValue); };
            }
        }
    }
}