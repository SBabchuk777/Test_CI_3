using Tools.MaxCore.Example.View.Settings.ComponentUI;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Example.View.Settings
{
    public class SettingsView : BaseView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private SoundSetter _setter;

        [Inject] private SettingService settingService;
        protected override void Initialize()
        {
            _closeButton.onClick.AddListener(ClosePanel);
            _setter.SetDefault(settingService.SettingsData.MusicVolumeDefault, settingService.SettingsData.SoundVolumeDefault);
            _setter.SetValues(settingService.SettingsData.MusicVolumeCount, settingService.SettingsData.SoundVolumeCount);
        }

        protected override void Subscribe()
        {
            _setter.OnChangeMusic += settingService.ChangeMusic;
            _setter.OnChangeSound += settingService.ChangeSound;
        }

        protected override void Open()
        {
            _setter.Initialize();
        }

        protected override void Unsubscribe()
        {
            _closeButton.onClick.RemoveAllListeners();
            settingService.SaveData();
        }
    }
}