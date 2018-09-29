using System;
using System.Collections.Generic;
using System.Linq;

namespace OperableArray
{
    public class OperableArray<T>
    {
        //slow implementation implementation with dictionary, performs badly on high density data, probably
        //An Infinite array that can be operated on
        Dictionary<int, T> dict = new Dictionary<int, T>();
        Func<T> _default;

        /// <summary>
        /// Creates a new empty array with a default provider.
        /// </summary>
        /// <param name="_default">A function that returns a new instance of the default value when called.</param>
        public OperableArray(Func<T> _default) { }
        /// <summary>
        /// Creates a new array with a given preexisting dictionary of data, and a default provider.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="_default">A function that returna a new instance of the default value when called.</param>
        public OperableArray(Dictionary<int,T> values, Func<T> _default)
        {
            dict = values;
        }

        public T this[int index]
        {
            get
            {
                if (dict.ContainsKey(index))
                    return dict[index];
                return _default();
            }
            set
            {
                dict[index] = value;
            }
        }
        /// <summary>
        /// Checks if all values are equal to the default value
        /// </summary>
        /// <param name="equals">A function that calculate equality between two instances of T</param>
        /// <returns>A bool indicating if all items are equal to the default</returns>
        public bool AllDefault(Func<T, T, bool> equals)
        {
            return AllValue(equals, _default());
        }
        /// <summary>
        /// Checks is all values equal a given value T
        /// </summary>
        /// <param name="equals">A function that checks for equality</param>
        /// <param name="value">A value to check equality against</param>
        /// <returns>A bool indicating if all items are equal to the given value</returns>
        public bool AllValue(Func<T,T,bool> equals, T value)
        {
            return dict.Values.All(x => equals(x, value));
        }
        /// <summary>
        /// Projects all values into a new form, in a new array. Also transforms the default.
        /// </summary>
        /// <typeparam name="U">The new form</typeparam>
        /// <param name="transform">A transformation function</param>
        /// <returns>New array with transformed values, with a transformed default.</returns>
        public OperableArray<U> Transform<U>(Func<T,U> transform)
        {
            return new OperableArray<U>(dict.ToDictionary(x => x.Key, x => transform(x.Value)), () => transform(_default()));
        }
        /// <summary>
        /// Translates the entire array by a certain amount.
        /// </summary>
        /// <param name="amount">The amount to translate by</param>
        /// <returns>A new translated array</returns>
        public OperableArray<T> Translate(int amount)
        {
            return new OperableArray<T>(dict.ToDictionary(x => x.Key + amount, x => x.Value), _default);
        }
        /// <summary>
        /// An elementwise operation with two arrays. Also performs the elementwise operation on the defaults.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="_A"></param>
        /// <param name="_B"></param>
        /// <param name="operation"></param>
        /// <returns>New array with changes values and a transformed default.</returns>
        public static OperableArray<C> ElementwiseCombine<A,B,C>(OperableArray<A> _A, OperableArray<B> _B, Func<A,B,C> operation)
        {
            var keys = _A.dict.Keys.Select(x=>x).Concat(_B.dict.Keys.Select(x => x)).Distinct();
            return new OperableArray<C>(keys.ToDictionary(x => x, x => operation(_A[x], _B[x])), () => operation(_A._default(), _B._default()));
        }

        /// <summary>
        /// Removes any values equal to default that are saved in the dictionary, in place.
        /// </summary>
        /// <param name="equals"></param>
        public void CleanDefaultSets(Func<T,T,bool> equals)
        {
            var aredefault = dict.Keys.Where(x => equals(_default(), dict[x]));
            foreach (var key in aredefault)
            {
                dict.Remove(key);
            }
        }
    }

    /*Notes to self
     
     Make a pure and a mutable version. Mutable version will do as much as possible in place and allow value editing to aid speed. Pure version will be pure.
     Pure version only allows value types
     Cannot make pure version in C# :(
     
     */
}
