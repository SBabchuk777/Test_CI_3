using System;
using Game.Scripts.Runtime.Feature.Player;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.DataHubService.Data;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.UiViews
{
    public class TutorialView : BaseView
    {
        [Inject] private DataHub dataHub;

        private bool isInitialize;
        public void Update()
        {
            if (Input.GetMouseButtonUp(0) && isInitialize)
            {
                dataHub.LoadData<PlayerProgressData>(DataType.Progress).IsShowTutorial = true;
                dataHub.SaveData(DataType.Progress);
                ClosePanel();
            }
        }

        protected override void Initialize()
        {
        }

        protected override void Subscribe()
        {
        }

        protected override void Open()
        {
            isInitialize = true;
        }

        protected override void Unsubscribe()
        {
        }
    }
}