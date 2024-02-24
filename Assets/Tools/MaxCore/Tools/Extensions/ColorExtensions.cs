using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Tools.Extensions
{
    public static class ColorExtensions
    {
        public static void SetAlpha(this Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }
        public static void SetAlpha(this SpriteRenderer sprite, float alpha)
        {
            var color = sprite.color;
            color.a = alpha;
            sprite.color = color;
        }
    }
}