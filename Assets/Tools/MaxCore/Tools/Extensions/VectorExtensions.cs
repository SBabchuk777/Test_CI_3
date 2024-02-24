using UnityEngine;

namespace Tools.MaxCore.Tools.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 SetXPosition(this Vector3 vector, float newX)
        {
            return new Vector3(newX, vector.y, vector.z);
        }
        public static Vector3 SetLocalXPosition(this Vector3 vector, float newX)
        {
            return new Vector3(newX, vector.y, vector.z);
        }
        
        public static Vector3 SetYPosition(this Vector3 vector, float newY)
        {
            return new Vector3(vector.x, newY, vector.z);
        }
        
        public static Vector3 SetZPosition(this Vector3 vector, float newZ)
        {
            return new Vector3(vector.x, vector.y, newZ);
        }
        
        public static Vector2 SetXPosition(this Vector2 vector, float newX)
        {
            return new Vector2(newX, vector.y);
        }
        
        public static Vector2 SetYPosition(this Vector2 vector, float newY)
        {
            return new Vector2(vector.x, newY);
        }
    }
}