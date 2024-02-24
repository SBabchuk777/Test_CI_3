using Tools.MaxCore.Scripts.Project.Audio;
using Tools.MaxCore.Scripts.Project.DI;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Scripts.ComponentHelp
{
    [RequireComponent(typeof(Button))]
    public class ClickAudioButton : MonoBehaviour
    {
        private ProjectAudioPlayer projectAudioPlayer;
        private Button Button => gameObject.GetComponent<Button>();

        private void Awake()
        {
            if (Button == null)
                return;
            projectAudioPlayer = ProjectContext.Instance.GetDependence<ProjectAudioPlayer>();
            Button.onClick.AddListener(() => projectAudioPlayer.PlayAudioSfx(ProjectAudioType.Click));
        }
    }
}