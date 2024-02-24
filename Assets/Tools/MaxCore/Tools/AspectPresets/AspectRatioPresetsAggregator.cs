using System.Collections.Generic;
using Tools.MaxCore.Tools.AspectPresets.Data;
using UnityEngine;

namespace Tools.MaxCore.Tools.AspectPresets
{
	public class AspectRatioPresetsAggregator : MonoBehaviour
	{
		[Header("Editor settings")]
		public bool ShowBordersInEditor;
		public AspectRatioType DebugAspect;
		public float CameraSize = 768f;
		public Color GizmosColor;

		[Header("Component settings")]
		public bool AutoSetup;
		public AspectsRangesData AspectsRangesData;

		private AspectRatioCalculator aspectRatioCalculator => new(AspectsRangesData);
		private IEnumerable<AspectRatioPreset> presets;
		private IEnumerable<AspectRatioPreset> Presets => 
			presets ??= GetComponentsInChildren<AspectRatioPreset>(true);

		public AspectRatioType Aspect => aspectRatioCalculator.GetAspectRatio();

		private void Start()
		{
			if (AutoSetup) 
				Setup(Aspect);
		}

		private void Setup(AspectRatioType aspectRatioType)
		{
			foreach (var preset in Presets)
				preset.Setup(AspectsRangesData, aspectRatioType);
		}

		public void Setup() => Setup(Aspect);
	}
}
