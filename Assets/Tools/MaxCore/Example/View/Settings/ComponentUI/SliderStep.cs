using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Example.View.Settings.ComponentUI
{
    public class SliderStep : MonoBehaviour
    {
        [SerializeField] private Sprite _backSprite;
        [SerializeField] private Sprite _fillSprite;

        [SerializeField] private List<Image> _stepsSlide;

        [SerializeField] private Button _spendButton;
        [SerializeField] private Button _addButton;

        private int counter;
        private float minValue;
        private float maxValue;
        private int MaxStep => _stepsSlide.Count;

        public event Action<float> OnChangValue;

        public void Initialize(float soundValue, float minValue, float maxValue)
        {
            this.maxValue = maxValue;
            this.minValue = minValue;
            
            counter = GetCounter(soundValue);
            SetStep();

            _spendButton.onClick.AddListener(() => Add(-1));
            _addButton.onClick.AddListener(() => Add(1));
        }

        private void Add(int value)
        {
            if (TryAddSpend(counter + value))
            {
                counter += value;

                SetStep();
                SetValue();
            }
        }

        private void SetStep()
        {
            for (var i = 0; i < _stepsSlide.Count; i++)
            {
                _stepsSlide[i].sprite = i < counter ? _fillSprite : _backSprite;
            }
        }

        private bool TryAddSpend(int current)
        {
            return current >= 0 && current <= MaxStep;
        }

        private int GetCounter(float soundValue)
        {
            var proportion = Mathf.InverseLerp(minValue, maxValue, soundValue);
            var interpolatedValue = Mathf.Lerp(0, MaxStep, proportion);

            return Mathf.RoundToInt(interpolatedValue);
        }

        private void SetValue()
        {
            var proportion = Mathf.InverseLerp(0, MaxStep, counter);
            var interpolatedValue = Mathf.Lerp(minValue, maxValue, proportion);

            OnChangValue?.Invoke(interpolatedValue);
        }
    }
}