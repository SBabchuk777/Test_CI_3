using System;
using Tools.MaxCore.Tools.SerializableComponent;

namespace Tools.MaxCore.Tools.AspectPresets.Data
{
	[Serializable]
	public class PresetData : SerializableDictionary<AspectRatioType, TransformWrapper> {}
}
