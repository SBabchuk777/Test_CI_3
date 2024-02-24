using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Runtime.Feature.Player;
using Game.Scripts.Runtime.Feature.UIViews.LevelSelect.Data;
using Tools.MaxCore.Example.View.LevelSelect.ComponentUI;
using Tools.MaxCore.Scripts.Project.DI;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.DataHubService.Data;
using Tools.MaxCore.Scripts.Services.SceneLoaderService;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.UIViews.LevelSelect
{
    public class SelectLevelController : MonoBehaviour, IProjectInitializable
    {
        [Inject] private SceneNavigation sceneNavigation;
        [Inject] private DataHub dataHub;
        [Inject] private UIViewService uiViewService;

        private LevelGameData levelGameData;
        private SelectLevelData selectLevelData;
        private PlayerProgressData playerProgressData;

        public event Action OnRunLevel;

        public void Initialize()
        {
            levelGameData = dataHub.LevelGameData;
            selectLevelData = dataHub.LoadData<SelectLevelData>(DataType.SelectLevel);
            playerProgressData = dataHub.LoadData<PlayerProgressData>(DataType.Progress);
        }

        public void PrepareView(List<LevelButton> levelButtons)
        {
            var dataLevels = selectLevelData.Levels;

            for (var i = 0; i < dataLevels.Count; i++)
            {
                var state = dataLevels[i].LevelSelectState;
                var countLevel = dataLevels[i].CountLevel;
                var levelButton = levelButtons[i];

                levelButton.Initialize();
                switch (state)
                {
                    case LevelSelectState.Closed:
                        levelButton.SetClose(countLevel);
                        break;
                    case LevelSelectState.Active:
                        SetActiveLevel(levelButton, dataLevels, i, countLevel);
                        break;
                    case LevelSelectState.Available:
                        SetAvailable(levelButton, countLevel);
                        break;
                }
            }
        }

        public void SetCompleteLevel(int countLevel, int countStarOnLevel)
        {
            var level = selectLevelData.Levels[countLevel];

            if (countStarOnLevel > level.CountStarInLevel)
            {
                level.CountStarInLevel = countStarOnLevel;
                level.LevelSelectState = LevelSelectState.Active;

                if (++countLevel < selectLevelData.Levels.Count)
                {
                    var nextLevel = selectLevelData.Levels[countLevel];

                    if (nextLevel.LevelSelectState == LevelSelectState.Closed)
                    {
                        nextLevel.LevelSelectState = LevelSelectState.Available;
                        //playerProgressData.AddLevel();
                    }
                }

                dataHub.SaveData(DataType.SelectLevel, selectLevelData);
                dataHub.SaveData(DataType.Progress, playerProgressData);
            }
        }

        public bool TryRunNextLevel(int countLevel)
        {
            var maxCountStar = countLevel * 3;
            var currentStar = selectLevelData.Levels.Sum(l => l.CountStarInLevel);

            return currentStar >= maxCountStar && countLevel < 20;
        }

        private void SetAvailable(LevelButton levelButton, int countLevel)
        {
            levelButton.SetAvailable(countLevel);
            levelButton.SetSelectButton(() => RunLevel(countLevel));
        }

        private void SetActiveLevel(LevelButton levelButton, List<LevelInformation> dataLevels, int i, int countLevel)
        {
            levelButton.SetActive(dataLevels[i].CountStarInLevel, countLevel);
            levelButton.SetSelectButton(() => RunLevel(countLevel));
        }

        private void CheckAvailableRunLevel(int countLevel)
        {
            if (countLevel == 0)
            {
                RunLevel(countLevel);
                return;
            }

            if (TryRunNextLevel(countLevel))
            {
                RunLevel(countLevel);
            }
            else
            {
                uiViewService.Instantiate(UIViewType.CompletePrevious);
            }
        }

        private void RunLevel(int value)
        {
            LoadLevel(value);
        }

        private void LoadLevel(int value)
        {
            levelGameData.CurrentLevel = value;
            sceneNavigation.LoadLevel();

            OnRunLevel?.Invoke();
        }
    }
}