using System.Collections.Generic;

namespace Utils
{
    internal static class Easing
    {
        internal static float SetEasingFunction(float time, Motion.EasingType easing)
        {
            switch (easing)
            {
                case Motion.EasingType.Linear:
                    return time;
                case Motion.EasingType.Cubic:
                    return time * time * time;
                case Motion.EasingType.EaseIn:
                    return time * time;
                case Motion.EasingType.EaseOut:
                    return time * (2 - time);
                case Motion.EasingType.EaseInOut:
                    return time < 0.5f ? 2 * time * time : -1 + (4 - 2 * time) * time;
                default:
                    return time;
            }
        }
    }

    internal class AutoIncreaseList<T> : List<T> where T : new() // where T : new() - type must have 0-param constructor
    {
        internal new T this[int index] // new: override base | this[int index]: define indexer
        {
            get
            {
                while (index >= Count)
                {
                    Add(new T());
                }

                return base[index];
            }

            set
            {
                while (index >= Count)
                {
                    Add(new T());
                }

                base[index] = value;
            }
        }
    }

    internal class StringAutoIncreaseList : List<string>
    {
        internal new string this[int index]
        {
            get
            {
                while (index >= Count)
                {
                    Add("");
                }

                return base[index];
            }

            set
            {
                while (index >= Count)
                {
                    Add("");
                }

                base[index] = value;
            }
        }
    }


}
