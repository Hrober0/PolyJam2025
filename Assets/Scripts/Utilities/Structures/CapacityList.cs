using System;
using System.Text;

namespace HCore
{
    public class CapacityList<T>
    {
        public int Capacity => _array.Length;
        public int Count { get; private set; }

        private readonly T[] _array;
        private int _pointerLast;
        private int _pointerFirst;

        public CapacityList(int capacity)
        {
            _array = new T[capacity];
            Clear();
        }

        public T Last => _array[_pointerLast];
        public T First => _array[_pointerFirst];

        /// <summary>
        /// Add item to end of list
        /// </summary>
        public void Add(T item)
        {
            _pointerLast++;
            if (_pointerLast >= _array.Length)
            {
                _pointerLast = 0;
            }
            _array[_pointerLast] = item;

            if (Count < Capacity)
            {
                Count++;
            }
            else
            {
                _pointerFirst++;
                if (_pointerFirst >= _array.Length)
                {
                    _pointerFirst = 0;
                }
            }
        }

        public void Clear()
        {
            Count = 0;
            _pointerLast = -1;
            _pointerFirst = 0;
        }

        /// <summary>
        /// Get nth item form end of list
        /// </summary>
        public T this[int indexFromEnd]
        {
            get => _array[IndexFromEndToArrIndex(indexFromEnd)];
            set => _array[IndexFromEndToArrIndex(indexFromEnd)]  = value;
        }

        private int IndexFromEndToArrIndex(int indexFromEnd)
        {
            if (indexFromEnd >= Count)
                throw new IndexOutOfRangeException();

            var index = _pointerLast - indexFromEnd;
            return index < 0 ? index + _array.Length : index;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            var f = false;
            for (int i = 0; i < Count; i++)
            {
                if (f)
                    sb.Append(", ");
                else
                    f = true;
                sb.Append(this[i]);
            }
            sb.Append("]");
            //sb.Append(_array.ElementsString());
            return sb.ToString();
        }
    }
}
