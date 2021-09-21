using System;
using UnityEngine;

namespace Deblue.Extensions
{
    public static class TransformExtensions
    {
        public static Quaternion Tern(this Transform transform, float value)
        {
            if (value != 0f)
            {
                int tern = value < 0f ? 180 : 0;
                transform.rotation = Quaternion.Euler(0, tern, 0);
            }

            return transform.rotation;
        }
    }

    public static class ObjectExtensions
    {
        public static T Do<T>(this T obj, Action<T> action, bool when)
        {
            if (when)
                action.Invoke(obj);

            return obj;
        }

        public static T Do<T>(this T obj, Action<T> action)
        {
            action.Invoke(obj);
            return obj;
        }
    }

    public static class MathExtensions
    {
        public static float CalculatePercent(this float value, float upperLimit)
        {
            return value / upperLimit;
        }

        public static int GetClearDirection(this float direction)
        {
            var i = 0;
            if (direction != 0)
            {
                i = direction > 0 ? 1 : -1;
            }

            return i;
        }

        public static int GetClearDirection(this float direction, float threshold)
        {
            var i = 0;
            if (direction > threshold)
            {
                i = 1;
            }
            else if (direction < -threshold)
            {
                i = -1;
            }

            return i;
        }
    }
}