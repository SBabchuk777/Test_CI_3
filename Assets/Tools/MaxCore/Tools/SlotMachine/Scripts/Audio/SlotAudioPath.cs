using Tools.MaxCore.Tools.SerializableComponent;
using UnityEngine;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.Audio
{
    [CreateAssetMenu(menuName = "Core/Tools/SlotMachine/Path/" + nameof(SlotAudioPath), fileName = "SlotAudioPath", order = 0)]
    public class SlotAudioPath : ScriptableObject
    {
        public SerializableDictionary<string, AudioClip> SlotAudioPathMap;
    }
}