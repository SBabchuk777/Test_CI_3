using Tools.MaxCore.Tools.SerializableComponent;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Project.Audio
{
    [CreateAssetMenu(menuName = "Game/AudioPath/" + nameof(ProjectAudioPath), fileName = "ProjectAudioPath", order = 0)]
    public class ProjectAudioPath : ScriptableObject
    {
        public SerializableDictionary<ProjectAudioType, AudioClip> ProjectAudioPathMap;
    }
}