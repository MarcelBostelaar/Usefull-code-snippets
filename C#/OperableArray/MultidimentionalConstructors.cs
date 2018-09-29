using System;
using System.Collections.Generic;
using System.Text;

namespace OperableArray
{
    static class MultidimentionalConstructors
    {
        public static OperableArray<OperableArray<T>> operableArray2D<T>(Func<T> _default)
        {
            return new OperableArray<OperableArray<T>>(() => new OperableArray<T>(_default));
        }
        public static OperableArray<OperableArray<OperableArray<T>>> operableArray3D<T>(Func<T> _default)
        {
            return new OperableArray<OperableArray<OperableArray<T>>>(() => operableArray2D(_default));
        }
        public static OperableArray<OperableArray<OperableArray<OperableArray<T>>>> operableArray4D<T>(Func<T> _default)
        {
            return new OperableArray<OperableArray<OperableArray<OperableArray<T>>>>(() => operableArray3D(_default));
        }
    }
}
