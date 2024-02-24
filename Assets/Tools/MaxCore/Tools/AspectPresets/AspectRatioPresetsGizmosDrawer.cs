#if UNITY_EDITOR

using System.Collections.Generic;
using Tools.MaxCore.Tools.AspectPresets.Data;
using UnityEditor;
using UnityEngine;

namespace Tools.MaxCore.Tools.AspectPresets
{
    public class AspectRatioPresetsGizmosDrawer
    {
        private static Dictionary<AspectRatioType, (float, float)> aspectsPairsMap = 
            new Dictionary<AspectRatioType, (float, float)>
            {
                [AspectRatioType.AspectDefault] = (16f, 9f),
                [AspectRatioType.Aspect3x2] = (3f, 2f),
                [AspectRatioType.Aspect4_3x3] = (4.3f, 3f),
                [AspectRatioType.Aspect4x3] = (4f, 3f),
                [AspectRatioType.Aspect5x4] = (5f, 4f),
                [AspectRatioType.Aspect16x9] = (16f, 9f),
                [AspectRatioType.Aspect16x10] = (16f, 10f),
                [AspectRatioType.Aspect19x9] = (19.5f, 9f),
                [AspectRatioType.Aspect18x9] = (18f, 9f),
                [AspectRatioType.Aspect20x9] = (20f, 9f),
                [AspectRatioType.Aspect21x9] = (21f, 9f),
                [AspectRatioType.Aspect22x9] = (22f, 9f),
            };

        [DrawGizmo(GizmoType.NonSelected | GizmoType.Active)]
        private static void DrawBorders(AspectRatioPresetsAggregator aggregator, GizmoType gizmoType)
        {
            if (!aggregator.ShowBordersInEditor)
                return;

            var position = aggregator.transform.position;
            var size = GetGizmoCubeSize(aggregator);
            
            Gizmos.color = aggregator.GizmosColor;
            Gizmos.DrawCube(position, size);
        }

        private static Vector3 GetGizmoCubeSize(AspectRatioPresetsAggregator aggregator)
        {
            var verticalSize = Application.isPlaying 
                ? Camera.main.orthographicSize * 2f
                : aggregator.CameraSize * 2f;

            var aspectsPair = GetDebugAspectsPair(aggregator.DebugAspect);
            var horizontalSize = aspectsPair.Item1 * verticalSize / aspectsPair.Item2;
            var size = new Vector3(horizontalSize, verticalSize, 1f);

            return size;
        }

        private static (float, float) GetDebugAspectsPair(AspectRatioType aspectType)
        {
            if (aspectsPairsMap.TryGetValue(aspectType, out var aspectsPair))
                return aspectsPair;

            return aspectsPairsMap[AspectRatioType.AspectDefault];
        }
    }
}
#endif