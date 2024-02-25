using System.Collections.Generic;
using Game.Scripts.Runtime.Feature.Level.Component;
using Game.Scripts.Runtime.Feature.Level.Hoop;
using Game.Scripts.Runtime.Feature.Services.CoinAtLevelService;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level.Field
{
    public class LevelTemplate : MonoBehaviour
    {
        [SerializeField] private List<HoopHandler> _hoops;
        public List<Platform> Platforms;
        public List<HoopHandler> Hoops => _hoops;
        
        public bool IsSetTemplate;
        
        private void OnValidate()
        {
            if (IsSetTemplate)
            {
                _hoops.Clear();
                Platforms.Clear();

                _hoops.AddRange(GetComponentsInChildren<HoopHandler>());
                Platforms.AddRange(GetComponentsInChildren<Platform>());
            }
        }

        public void Initialize(CoinService coinService)
        {
            _hoops.ForEach(h=>h.Initialize(coinService));
        }
    }
}