using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CommandPlus.Commanding
{
    /// <summary>
    /// Represents a collection of arguments.
    /// </summary>
    public class ArgumentCollection : ICollection<KeyValuePair<Type, object>>
    {
        private List<KeyValuePair<Type, object>> pairs = new List<KeyValuePair<Type, object>>();
        private bool @sealed;

        public KeyValuePair<Type, object> this[int index]
        {
            get => pairs[index];
            set
            {
                if (@sealed) throw new InvalidOperationException("This argument collection is already sealed.");
                pairs[index] = value;
            }
        }

        public int Count => pairs.Count;

        public bool IsReadOnly => @sealed;

        /// <summary>
        /// Seals this collection. Any further write attempts will be denied and this instance will become read-only.
        /// Collections with arguments must be sealed before passing it to commands.
        /// </summary>
        public void Seal()
        {
            @sealed = true;
        }

        public void Add(KeyValuePair<Type, object> item)
        {
            if (@sealed) throw new InvalidOperationException("This argument collection is already sealed.");
            pairs.Add(item);
        }

        public void Clear()
        {
            if (@sealed) throw new InvalidOperationException("This argument collection is already sealed.");
            pairs.Clear();
        }

        public bool Contains(KeyValuePair<Type, object> item)
        {
            return pairs.Contains(item);
        }

        public void CopyTo(KeyValuePair<Type, object>[] array, int arrayIndex)
        {
            pairs.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Type, object>> GetEnumerator()
        {
            return pairs.GetEnumerator();
        }

        public bool Remove(KeyValuePair<Type, object> item)
        {
            if (@sealed) throw new InvalidOperationException("This argument collection is already sealed.");
            return pairs.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
