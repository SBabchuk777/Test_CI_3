namespace Tools.MaxCore.Tools.AspectPresets.Data
{
    [System.Serializable]
    public class AspectData
    {
        public AspectRatioType AspectType;
        public float Width;
        public float Height;

        public float Aspect => Width / Height;
    }
}