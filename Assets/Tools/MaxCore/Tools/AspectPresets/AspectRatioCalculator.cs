using System;
using System.Linq;
using Tools.MaxCore.Tools.AspectPresets.Data;
using UnityEngine;

namespace Tools.MaxCore.Tools.AspectPresets
{
    public class AspectRatioCalculator
    {
        private AspectsRangesData aspectsRangesData;

        public AspectRatioCalculator(AspectsRangesData aspectsRangesData) => 
            this.aspectsRangesData = aspectsRangesData;

        public AspectRatioType GetAspectRatio()
        {
            var deviceAspect = (float)Screen.width/ Screen.height;

            var aspectType = aspectsRangesData.Aspects
                .OrderBy(x => Math.Abs(x.Aspect - deviceAspect))
                .First()
                .AspectType;

            return aspectType;
        }
    }
}