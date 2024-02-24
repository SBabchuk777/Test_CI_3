using UnityEngine;
using UnityEngine.Serialization;

namespace Tools.MaxCore.Tools.AspectPresets.Data
{
	[System.Serializable]
	public class TransformWrapper
	{
		[FormerlySerializedAs("position")] public Vector3 Position;
		[FormerlySerializedAs("rotation")] public Vector3 Rotation;
		[FormerlySerializedAs("scale")] public Vector3 Scale;

		public TransformWrapper(Vector3 position, Vector3 rotation, Vector3 scale)
		{
			Position = position;
			Rotation = rotation;
			Scale = scale;
		}

		public static implicit operator TransformWrapper(Transform transform) =>
			new TransformWrapper(transform.localPosition, transform.localEulerAngles, transform.localScale);
	}
}
