using UnityEngine.UI;

namespace Tools.MaxCore.Tools.Extensions
{
    public static class TextExtensions
    {
        public static void SetAlpha(this Text text, float alpha)
        {
            var color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }
}