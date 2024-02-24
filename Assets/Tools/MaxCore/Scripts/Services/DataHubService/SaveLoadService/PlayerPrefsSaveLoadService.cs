using Newtonsoft.Json;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.DataHubService.SaveLoadService
{
    public class PlayerPrefsSaveLoadService : ISaveLoadService<DataKey>
    {
        public void Save<TData>(DataKey dataKey, TData data)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            
            PlayerPrefs.SetString(dataKey.Key, serializedData);
        }
        
        public TData Load<TData>(DataKey dataKey)
        {
            var loadedDataString = PlayerPrefs.GetString(dataKey.Key, string.Empty);
            var deserializedData = JsonConvert.DeserializeObject<TData>(loadedDataString);
        
            return deserializedData;
        }
        
        public TData Load<TData>(DataKey dataKey, TData defaultValue)
        {
            var loadedDataString = PlayerPrefs.GetString(dataKey.Key, JsonConvert.SerializeObject(defaultValue));
            var deserializedData = JsonConvert.DeserializeObject<TData>(loadedDataString);
        
            return deserializedData;
        }
    }
}