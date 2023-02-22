using System;

namespace MP.Lib.Core
{
    public class Lazy
    {
        public static T Create<T>() where T : new()
        {
            return new Lazy<T>(() => new T()).Value;
        }
    }
}
