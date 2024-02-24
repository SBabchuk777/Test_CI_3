using System.Collections.Generic;
using System.Linq;
using Tools.MaxCore.Tools.AspectPresets.Data;
using Unity.Collections;
using UnityEngine;

namespace Tools.MaxCore.Tools.AspectPresets
{
	public class AspectRatioPreset : MonoBehaviour
	{
		[field: SerializeField] public PresetData UiPresetsData { get; private set; }

		public bool IgnorePosition;
		public bool IgnoreRotation;
		public bool IgnoreScale;

		[SerializeField, ReadOnly] private AspectRatioType appliedAspect;
		
		private AspectsRangesData aspectsRangesData;
		
		public void Setup(AspectsRangesData aspectsRangesData ,AspectRatioType aspectRatioType)
		{
			this.aspectsRangesData = aspectsRangesData;
			
			var availablePreset = UiPresetsData.ContainsKey(aspectRatioType) 
				? aspectRatioType
				: GetClosestAspect(aspectRatioType);

			ApplyPreset(availablePreset);
		}

		public void Save(AspectRatioType aspectRatioType) => 
			UiPresetsData[aspectRatioType] = transform;

		private void ApplyPreset(AspectRatioType aspectRatioType)
		{
			var transformData = UiPresetsData[aspectRatioType];

			if (!IgnorePosition) transform.localPosition = transformData.Position;
			if (!IgnoreRotation) transform.localEulerAngles = transformData.Rotation;
			if (!IgnoreScale) transform.localScale = transformData.Scale;

			appliedAspect = aspectRatioType;
		}
		
		private AspectRatioType GetClosestAspect(AspectRatioType targetAspect)
		{
			var closestAspect = AspectRatioType.AspectDefault;
			
			if (aspectsRangesData.PhonesGroup.Contains(targetAspect))
				closestAspect = PickClosestAvailableFromTop(aspectsRangesData.PhonesGroup, targetAspect);
			else if (aspectsRangesData.TabletsGroup.Contains(targetAspect))
				closestAspect = PickClosestAvailableFromTop(aspectsRangesData.TabletsGroup, targetAspect);

			return UiPresetsData.ContainsKey(closestAspect) ? closestAspect : AspectRatioType.AspectDefault;
		}

		private AspectRatioType PickClosestAvailableFromTop(IList<AspectRatioType> aspects, AspectRatioType target)
		{
			var indexOfTarget = aspects.IndexOf(target);
			var aspectsBelowTarget = aspects.Take(indexOfTarget);

			return aspectsBelowTarget.Reverse().FirstOrDefault(aspect => UiPresetsData.ContainsKey(aspect));
		}
	}
}
