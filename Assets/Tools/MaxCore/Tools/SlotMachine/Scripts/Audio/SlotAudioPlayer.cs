using DG.Tweening;
using Tools.MaxCore.Scripts.Project.DI;
using Tools.MaxCore.Scripts.Services.Audio.AudioCore;
using UnityEngine;
using AudioType = Tools.MaxCore.Scripts.Services.Audio.AudioCore.AudioType;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.Audio
{
    public class SlotAudioPlayer : MonoBehaviour
    {
        public SlotAudioPath SlotAudioPath;
        private AudioService AudioService => ProjectContext.Instance.GetDependence<AudioService>();
       
        private AudioSource slotMoveSource;

        public void OnDestroy() =>
            StopAudioMoveSlot();

        public void PlayAudioMoveSlot()
        {
            var audioClip = SlotAudioPath.SlotAudioPathMap["SlotMove"];

            slotMoveSource = AudioService.Play(new Tune(audioClip, AudioType.Music, true));

            slotMoveSource.volume = 0;
            slotMoveSource.DOFade(1, 2).Play();
        }

        public void StopAudioMoveSlot()
        {
            if (slotMoveSource != null)
                AudioService.Stop(slotMoveSource);
        }

        public void PlayAudioFinishGroup()
        {
            var audioClip = SlotAudioPath.SlotAudioPathMap["SlotStop"];

            if (audioClip == null)
            {
                return;
            }
            
            AudioService.Play(new Tune(audioClip, AudioType.Music));
            
        }

        public void PlayAudioWinSpin()
        {
            var audioClip = SlotAudioPath.SlotAudioPathMap["Win"];

            AudioService.Play(new Tune(audioClip, AudioType.Music));
        }
        public void PlayAudioLoseSpin()
        {
            var audioClip = SlotAudioPath.SlotAudioPathMap["Lose"];

            AudioService.Play(new Tune(audioClip, AudioType.Music));
        }
    }
}