using System;
using System.Collections.Generic;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Tools.SerializableComponent;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.ResourceVaultService
{
    [Serializable]
    public class ResourceData : DataPayload
    {
        [SerializeField] private SerializableDictionary<ResourceType, int> _mapInt;
        [SerializeField] private SerializableDictionary<ResourceType, float> _mapFloat;
        
        public Dictionary<ResourceType, int> MapInt;
        public Dictionary<ResourceType, float> MapFloat;

        public override void InitializeDefault()
        {
            MapInt = new Dictionary<ResourceType, int>();
            MapFloat = new Dictionary<ResourceType, float>();
            
            foreach (var pair in _mapInt)
            {
                MapInt.Add(pair.Key, pair.Value);
            }
            
            foreach (var pair in _mapFloat)
            {
                MapFloat.Add(pair.Key, pair.Value);
            }
        }
    }
}