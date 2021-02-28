using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            Array<int> myArray = new Array<int>(1, 2, 3, 4, 5);
            myArray.ItemValueChanged += MyArray_ItemValueChanged;
            myArray[2] = 0;

            Console.ReadKey();
        }

        private static void MyArray_ItemValueChanged(object sender, ArrayElementChangeArgs<int> e)
        {
            Console.WriteLine("Item[{0}] = {1} change to {2}", e.Index, e.OldValue, e.NewValue);
        }
    }

    public class Array<T>
    {
        public T[] Items { get; }

        public T this[int index]
        {
            get
            {
                if (Items is null || index < 0 || index >= Items.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return Items[index];
                }
            }
            set
            {
                if (Items is null || index < 0 || index >= Items.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    T oldValue = Items[index];
                    Items[index] = value;
                    ItemValueChanged?.Invoke(this, new ArrayElementChangeArgs<T>(oldValue, value, index));
                }
            }
        }

        public event EventHandler<ArrayElementChangeArgs<T>> ItemValueChanged;

        public Array(params T[] items)
        {
            Items = items;
        }

    }
    public class ArrayElementChangeArgs<T> : EventArgs
    {
        public T OldValue { get; }
        public T NewValue { get; }
        public int Index { get; }
        public bool CancelRequested { get; set; }
        public ArrayElementChangeArgs(T oldValue, T newValue, int index)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Index = index;
        }
    }
}